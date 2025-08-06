# Gestor de Bibliotecas Comunitárias

Este repositório contém uma implementação de um **sistema de gestão de bibliotecas comunitárias** desenvolvido em C# sobre .NET 8.0.  O objetivo é demonstrar os conceitos de **Modelagem de Domínio Rico**, **CQRS** e **mediador (MediatR)** em um cenário de cadastro e empréstimo de livros.  O código foi estruturado em camadas claramente definidas (Domínio, Aplicação, Infraestrutura e API) para promover separação de responsabilidades, testabilidade e extensibilidade.

## 🧠 Visão geral da arquitetura

### Camada de Domínio

Contém as entidades de negócio `Livro` e `Emprestimo` juntamente com regras de validação e comportamentos:

- **Livro**: possui `Id`, `Titulo`, `Autor`, `AnoPublicacao` e `QuantidadeDisponivel`.  A classe expõe métodos para diminuir e aumentar a quantidade em estoque, lançando exceção quando não há exemplares disponíveis.
- **Emprestimo**: armazena informações de empréstimos (`LivroId`, datas de empréstimo e devolução e `Status`).  O método `DevolverLivro` marca o empréstimo como devolvido e atualiza a data de devolução.

### Camada de Aplicação

Responsável por orquestrar a interação entre o domínio e as camadas externas.  Contém **comandos**, **consultas**, **manipuladores** e **modelos de visualização**:

- **Comandos**: definem operações de escrita, como criar livro (`CreateLivroRequest`), atualizar livro, criar empréstimo (`CreateEmprestimoRequest`) e devolver empréstimo (`ReturnEmprestimoRequest`).
- **Consultas**: definem operações de leitura; por exemplo `GetAllLivrosQuery`, `GetLivroByIdQuery`, `GetAllEmprestimosQuery` e `GetEmprestimoByIdQuery` retornam modelos de visualização desacoplados das entidades.
- **Manipuladores** (`Handlers`): executam a lógica para cada comando ou consulta através do MediatR.  O manipulador de criação de empréstimo reduz o estoque do livro, cadastra o empréstimo e comita a transação.  O manipulador de devolução incrementa o estoque e atualiza o status do empréstimo.
- **ViewModels**: classes simples usadas como DTOs na camada de consulta.

### Camada de Infraestrutura

Implanta os repositórios de acesso a dados.  A versão atual utiliza Entity Framework Core com SQL Server para gravação e um repositório base para leitura.  Estão definidas as interfaces `ILivroRepository`, `IEmprestimoRepository` e `IUnitOfWork` e suas implementações em `Repositories`.

### Camada de API

Exibe uma API RESTful com controladores ASP.NET Core:

- `api/livros`: permite criar livros (`POST`), atualizar (`PUT`), consultar todos os livros (`GET`) e consultar por id (`GET /{id}`).
- `api/emprestimos`: permite criar empréstimos (`POST`), atualizar (não suportado), devolver (`PUT /{id}/devolver`), listar (`GET`) e consultar empréstimo por id (`GET /{id}`).

O MediatR é utilizado para desacoplar os controladores da lógica de negócio.  Todas as dependências são registradas no container de DI em `Program.cs`.

## 🚀 Como executar o projeto

1. **Pré‑requisitos:**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.
   - Um servidor SQL Server (ou ajuste o `appsettings.json` para apontar para outro banco suportado).  Para testes locais pode‑se utilizar a edição Express ou um banco em memória.

2. **Clonar o repositório:**

   ```bash
   git clone https://github.com/angelo-marques/GestorBibliotecasComunitarias.git
   cd GestorBibliotecasComunitarias/GestorBibliotecaComunitaria
   ```

3. **Restaurar as dependências:**

   ```bash
   dotnet restore
   ```

4. **Atualizar a base de dados:**

   Para gerar as tabelas no banco SQL Server configure a `DefaultConnection` em `GestorBiblioteca.API/appsettings.json` e execute as migrações:

   ```bash
   dotnet ef database update --project GestorBiblioteca.API
   ```

5. **Executar a API:**

   ```bash
   dotnet run --project GestorBiblioteca.API
   ```

   A API estará acessível em `https://localhost:5001` (HTTPS) e `http://localhost:5000` (HTTP).  A interface do Swagger é habilitada em ambiente de desenvolvimento para facilitar os testes.

## ✅ Executando os testes e cobertura

Os testes unitários, de integração e de estresse encontram‑se no projeto `GestorBiblioteca.Tests`.  Para executá‑los, use o comando:

```bash
dotnet test --settings coverlet.runsettings --collect:"XPlat Code Coverage"
```

O arquivo `coverlet.runsettings` já está configurado para gerar relatórios de cobertura nos formatos **OpenCover**, **JSON** e **Cobertura** na pasta `TestResults`.  Você pode importar esses relatórios em ferramentas como o Visual Studio, Azure DevOps ou Cobertura.

## 📄 Melhorias implementadas

Esta solução inclui diversas melhorias em relação ao esboço inicial:

1. **Fluxo do mediador:** os manipuladores de comandos agora não utilizam `.Result` (evitando deadlocks) e trabalham de forma assíncrona.  O handler de criação de empréstimos reduz o estoque do livro, cria o empréstimo e comita em uma única transação.  Um novo handler (`ReturnEmprestimoHandler`) foi adicionado para processar devoluções, incrementando o estoque e atualizando o status.
2. **Consultas:** criadas queries simples (`GetAll*` e `GetById*`) que retornam modelos de visualização desacoplados das entidades e suportam filtragem por título/autor.
3. **Repositórios fakes e testes:** adicionados repositórios em memória e uma bateria de testes unitários cobrindo regras de domínio, handlers, queries, integração com a API e testes de estresse.
4. **Cobertura de código:** fornecido arquivo `coverlet.runsettings` para coleta de cobertura via Coverlet.
5. **Documentação:** este README explica a arquitetura, instruções de execução, testes e cobertura.

## 📝 Considerações finais

Este projeto demonstra como estruturar uma aplicação .NET utilizando práticas de DDD, CQRS e testes automatizados.  Para um ambiente de produção seria recomendável adicionar:

- Autenticação e autorização para controlar o acesso aos endpoints.
- Persistência em banco NoSQL para a parte de leitura (queries) e mecanismo de sincronização baseado em eventos.
- Tratamento de erros mais robusto e logging estruturado.

Contribuições e sugestões são bem‑vindas!