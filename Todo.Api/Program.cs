using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Services;
using Todo.Core.Interfaces;
using Todo.Core.Repositories;
using Todo.Infrastruture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar o DbContext (supondo que voc� esteja usando o Entity Framework Core)
builder.Services.AddDbContext<TodoDataContext>();

// 2. Registrar os reposit�rios e servi�os no cont�iner de depend�ncia
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();

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