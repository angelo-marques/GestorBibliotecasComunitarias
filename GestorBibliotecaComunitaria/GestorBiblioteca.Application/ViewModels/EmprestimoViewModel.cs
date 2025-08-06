using System;

namespace GestorBiblioteca.Application.ViewModels
{
    
    public class EmprestimoViewModel
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public string TituloLivro { get; set; } = string.Empty;
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}