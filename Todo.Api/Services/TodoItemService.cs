using Todo.Api.DTOs.Requests.TodoItems;
using Todo.Api.DTOs.Responses;
using Todo.Core.DTOs.Requests.TodoItems;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<Response<TodoItem?>> CreateAsync(CreateTodoItemRequest request)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await _todoItemRepository.AddAsync(todoItem);

                return new Response<TodoItem?>(todoItem, 201, "Tarefa criada com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<TodoItem?>(null, 500, $"Erro ao criar a tarefa: {ex.Message}");
            }
        }

        public async Task<Response<TodoItem?>> UpdateAsync(UpdateTodoItemRequest request)
        {
            try
            {
                var todoItem = await _todoItemRepository.GetByIdAsync(request.Id, request.UserId);

                if (todoItem is null)
                    return new Response<TodoItem?>(null, 404, "Tarefa não encontrada");

                todoItem.Title = request.Title;
                todoItem.Description = request.Description;
                todoItem.IsCompleted = request.IsCompleted;

                await _todoItemRepository.UpdateAsync(todoItem);

                return new Response<TodoItem?>(todoItem, message: "Tarefa atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<TodoItem?>(null, 500, $"Erro ao atualizar a tarefa: {ex.Message}");
            }
        }

        public async Task<Response<TodoItem?>> DeleteAsync(DeleteTodoItemRequest request)
        {
            try
            {
                var todoItem = await _todoItemRepository.GetByIdAsync(request.Id, request.UserId);

                if (todoItem is null)
                    return new Response<TodoItem?>(null, 404, "Tarefa não encontrada");

                await _todoItemRepository.DeleteAsync(todoItem);

                return new Response<TodoItem?>(todoItem, message: "Tarefa excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<TodoItem?>(null, 500, $"Erro ao excluir a tarefa: {ex.Message}");
            }
        }

        public async Task<Response<TodoItem?>> GetByIdAsync(GetTodoItemByIdRequest request)
        {
            var todoItem = await _todoItemRepository.GetByIdAsync(request.Id, request.UserId);

            return todoItem is null
                ? new Response<TodoItem?>(null, 404, "Tarefa não encontrada")
                : new Response<TodoItem?>(todoItem);
        }

        public async Task<PagedResponse<List<TodoItem>>> GetAllAsync(GetAllTodoItemsRequest request)
        {
            try
            {
                var query = _todoItemRepository.Query(request);


                var todoItems = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<TodoItem>>(
                    todoItems,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch (Exception ex)
            {
                return new PagedResponse<List<TodoItem>>(null, 500, $"Erro ao consultar as tarefas: {ex.Message}");
            }
        }
    }
}
