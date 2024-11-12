using Serilog;
using Serilog.Sinks.MSSqlServer;
using Todo.Api.Data;
using Todo.Api.Repositories;
using Todo.Api.Services;
using Todo.Core.Interfaces;
using Todo.Core.Repositories;
using Todo.Infrastruture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura��o b�sica do Serilog para salvar logs no banco de dados
Log.Logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(
        connectionString: TodoDataContext.connString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogEntries",
            AutoCreateSqlTable = false // Certifique-se de que a tabela "LogEntries" j� exista no banco de dados
        }
    )
    .CreateLogger();

builder.Host.UseSerilog();

// Definir Serilog como o provedor de logging da aplica��o
builder.Services.AddSingleton(Log.Logger);

// 1. Registrar o DbContext
builder.Services.AddDbContext<TodoDataContext>();

// 2. Registrar os reposit�rios e servi�os no cont�iner de depend�ncia
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// 3. Adicionar os controladores da aplica��o
builder.Services.AddControllers();

// Configura��o do Swagger
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
