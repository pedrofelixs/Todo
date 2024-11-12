using Serilog;
using Serilog.Sinks.MSSqlServer;
using Todo.Api.Data;
using Todo.Api.Repositories;
using Todo.Api.Services;
using Todo.Core.Interfaces;
using Todo.Core.Repositories;
using Todo.Infrastruture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração básica do Serilog para salvar logs no banco de dados
Log.Logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(
        connectionString: TodoDataContext.connString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogEntries",
            AutoCreateSqlTable = false // Certifique-se de que a tabela "LogEntries" já exista no banco de dados
        }
    )
    .CreateLogger();

builder.Host.UseSerilog();

// Definir Serilog como o provedor de logging da aplicação
builder.Services.AddSingleton(Log.Logger);

// 1. Registrar o DbContext
builder.Services.AddDbContext<TodoDataContext>();

// 2. Registrar os repositórios e serviços no contêiner de dependência
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// 3. Adicionar os controladores da aplicação
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para Swagger (apenas em desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
