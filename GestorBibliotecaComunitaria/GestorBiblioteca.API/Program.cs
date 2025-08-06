using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Handlers.Emprestimo;
using GestorBiblioteca.Application.Handlers.Livro;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBiblioteca.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        builder.Services.AddScoped(typeof(IRequestHandler<CreateEmprestimoRequest, GenericCommandResponse>), typeof(CreateEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<UpdateEmprestimoRequest, GenericCommandResponse>), typeof(UpdateEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<DeleteEmprestimoRequest, GenericCommandResponse>), typeof(DeleteEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<ReturnEmprestimoRequest, GenericCommandResponse>), typeof(ReturnEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<CreateLivroRequest, GenericCommandResponse>), typeof(CreateLivroHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<UpdateLivroRequest, GenericCommandResponse>), typeof(UpdateLivroHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<DeleteLivroRequest, GenericCommandResponse>), typeof(DeleteLivroHandler));


        builder.Services.AddTransient<IEmprestimoRepository, EmprestimoRepository>();
        builder.Services.AddTransient<ILivroRepository, LivroRepository>();
        builder.Services.AddDbContext<GestorBibliotecaDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("GestorBiblioteca.API")
            )
        );
        builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoConnection")));

        builder.Services.AddHttpClient();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}