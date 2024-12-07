using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infra.repository.generics;
using Infra.repository;
using dominio.Interfaces.Generics;
using dominio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using webApi.Token;
using Entities.Context;
using Domain.Interfaces;
using System.Text;
using Entities;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers e views (para API, é mais comum usar apenas Controllers)
builder.Services.AddControllers();

// Adiciona suporte à geração de documentação de API com Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de CORS para permitir acesso de diferentes origens
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar a string de conexão com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adiciona o DbContext ao container de injeção de dependência
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(connectionString));

// Configura o Identity para usar a entidade Usuario personalizada
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ContextBase>()
    .AddDefaultTokenProviders();

// Configuração de autenticação com JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

// Adiciona os repositórios genéricos e específicos
builder.Services.AddScoped(typeof(InterfaceGeneric<>), typeof(RepositorioGeneric<>));
builder.Services.AddScoped<InterfaceAdocao, RepositorioAdocao>();
builder.Services.AddScoped<InterfaceAnuncioAnimal, RepositorioAnuncioAnimal>();
builder.Services.AddScoped<InterfaceConversa, RepositorioConversa>();
builder.Services.AddScoped<InterfaceMensagem, RepositorioMensagem>();
builder.Services.AddScoped<InterfaceMidia, RepositorioMidia>();
builder.Services.AddScoped<InterfaceUsuariosConversa, RepositorioUsuariosConversa>();

// Adiciona serviços adicionais (exemplo)

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita o middleware de CORS
app.UseCors("AllowAll");

// Habilita o middleware de redirecionamento de HTTP para HTTPS
app.UseHttpsRedirection();

// Habilita a autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Mapeia os controllers para roteamento
app.MapControllers();

// Inicia a aplicação
app.Run();
