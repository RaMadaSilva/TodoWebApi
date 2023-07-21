using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;
using Todo.Domain.Repositories.UniteOfWork;
using Todo.Infrastructure.Context;
using Todo.Infrastructure.Repository;
using Todo.Infrastructure.Repository.UniteOfWork;

var builder = WebApplication.CreateBuilder(args);
var conections = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(conections));
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddTransient<IUniteOfWork, UniteOfWork>();
builder.Services.AddTransient<TodoHandler, TodoHandler>();


var app = builder.Build();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/todo", ([FromServices] UniteOfWork uow) =>
{
    var result = uow.TodoRepository.GetAll("raulsilva");

    if (result.Count() == 0)
        return Results.NotFound("Não existem tarefas");
    return Results.Ok(result);

});

app.MapPost("/todo", ([FromBody] CreateTodoCommand command, [FromServices] TodoHandler handler) =>
{
    var result = (GenericCommandResult)handler.Handle(command);
    if (!result.Sucesses)
        return Results.BadRequest("tarefa não criada");

    return Results.Created("/todo/ result.Id", result);
});

//Update Title
app.MapPut("/todo", ([FromServices] TodoHandler handle, [FromBody] UpdateTitleCommand command) =>
{
    var result = (GenericCommandResult)handle.Handle(command);

    if (!result.Sucesses)
        return Results.BadRequest("Não foi possivel actualizar o titulo");
    return Results.Ok(result);
});

//Marks Done
app.MapPut("/todo/done", ([FromServices] TodoHandler handler, [FromBody] MarkAsDoneCommand command)=>{

    var result = (GenericCommandResult)handler.Handle(command);
    
        if (!result.Sucesses)
        return Results.BadRequest("Não foi possivel actualizar o Estado da Tarefa");
    return Results.Ok(result);
}); 

//Marks Undone
app.MapPut("/todo/undone", ([FromServices] TodoHandler handler, [FromBody] MarkAsUnDoneCommand command)=>{

    var result = (GenericCommandResult)handler.Handle(command);
    
        if (!result.Sucesses)
        return Results.BadRequest("Não foi possivel actualizar o Estado da Tarefa");
    return Results.Ok(result);
}); 
app.Run();
