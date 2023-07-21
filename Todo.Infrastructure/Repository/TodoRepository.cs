using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Context;

namespace Todo.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
            => _context = context;

        public void Create(TodoItem todo)
        {
            _context.Add(todo);
        }

        public IEnumerable<TodoItem> GetAll(string user)
            => _context.TodoItems.AsNoTracking().Where(TodoQuery.GetAll(user));

        public IEnumerable<TodoItem> GetAllDone(string user)
            => _context.TodoItems.AsNoTracking().Where(TodoQuery.GetAllDone(user));

        public IEnumerable<TodoItem> GetAllUndone(string user)
            => _context
                .TodoItems
                .AsNoTracking()
                .Where(TodoQuery.GetAllUndone(user));


        public IEnumerable<TodoItem> GetAllTodo(string user)
            => _context
                    .TodoItems
                    .AsNoTracking()
                    .Where(x => x.User == user);

        public TodoItem GetById(Guid id, string user) => _context
                    .TodoItems
                    .Where(x => x.Id == id &&
                    x.User == user)
                    .FirstOrDefault()!;

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
            => _context
                .TodoItems
                .AsNoTracking()
                .Where(TodoQuery.GetByPeriodo(user, date, done));

        public void Update(TodoItem todo)
            => _context.Entry(todo).State = EntityState.Modified;
    }
}