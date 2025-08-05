using GestorBiblioteca.Application.Commands.Interfaces;

namespace GestorBiblioteca.Application.Handlers.Interfarces
{
    public interface IHandler<in T> where T : ICommand
    {
        ICommandResponse Handle(T command);
    }
}
