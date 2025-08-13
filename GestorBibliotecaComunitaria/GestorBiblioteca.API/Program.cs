using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Handlers.Emprestimo;
using GestorBiblioteca.Application.Handlers.Livro;
using GestorBiblioteca.Application.Queries.Emprestimos;
using GestorBiblioteca.Application.Queries.Livros;
using GestorBiblioteca.Application.ViewModels;
using GestorBiblioteca.Infrastructure.Events;
using GestorBiblioteca.Infrastructure.Interfaces;
using GestorBiblioteca.Infrastructure.Persistence;
using GestorBiblioteca.Infrastructure.Repositories;
using GestorBiblioteca.Infrastructure.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers(options => {
            options.RespectBrowserAcceptHeader = true;
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        }).AddJsonOptions(x => {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; x.JsonSerializerOptions.PropertyNamingPolicy = null; 
        });
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestor Biblioteca API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
        builder.Services.AddScoped(typeof(IRequestHandler<CreateEmprestimoRequest, GenericCommandResponse>), typeof(CreateEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<UpdateEmprestimoRequest, GenericCommandResponse>), typeof(UpdateEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<DeleteEmprestimoRequest, GenericCommandResponse>), typeof(DeleteEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<ReturnEmprestimoRequest, GenericCommandResponse>), typeof(ReturnEmprestimoHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<CreateLivroRequest, GenericCommandResponse>), typeof(CreateLivroHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<UpdateLivroRequest, GenericCommandResponse>), typeof(UpdateLivroHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<DeleteLivroRequest, GenericCommandResponse>), typeof(DeleteLivroHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<GetAllEmprestimosQuery, ResultViewModel<List<EmprestimoViewModel>>>), typeof(GetAllEmprestimosQueryHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<GetEmprestimoByIdQuery, ResultViewModel<EmprestimoViewModel>>), typeof(GetEmprestimoByIdQueryHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<GetAllLivrosQuery, ResultViewModel<List<LivroViewModel>>>), typeof(GetAllLivrosQueryHandler));
        builder.Services.AddScoped(typeof(IRequestHandler<GetLivroByIdQuery, ResultViewModel<LivroViewModel>>), typeof(GetLivroByIdQueryHandler));
        builder.Services.AddTransient<IEmprestimoRepository, EmprestimoRepository>();
        builder.Services.AddTransient<ILivroRepository, LivroRepository>();
        builder.Services.AddDbContext<GestorBibliotecaDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("GestorBiblioteca.API")
            )
        );
        builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoConnectionSettings"));
        builder.Services.AddSingleton<IBaseMongoContext, BaseMongoDbContext>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ILivroMongoRepository, LivroMongoEventsRepository>();
        builder.Services.AddScoped<IEmprestimoMongoRepository, EmprestimoMongoEventsRepository>();

        builder.Services.AddHttpClient();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<GestorBibliotecaDbContext>();
            db.Database.MigrateAsync();
        }
        // Configure the HTTP request pipeline.
        app.UseCors("AllowSpecificOrigin");
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}