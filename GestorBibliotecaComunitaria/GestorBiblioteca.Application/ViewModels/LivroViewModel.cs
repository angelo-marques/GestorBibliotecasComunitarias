namespace GestorBiblioteca.Application.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeCadastrada { get; set; }
    }
}