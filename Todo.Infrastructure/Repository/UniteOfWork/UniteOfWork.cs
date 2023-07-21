using Todo.Domain.Repositories;
using Todo.Domain.Repositories.UniteOfWork;
using Todo.Infrastructure.Context;

namespace Todo.Infrastructure.Repository.UniteOfWork
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly TodoDbContext _context;
        private ITodoRepository todoRepository;

        public UniteOfWork(TodoDbContext context)
        {
            _context = context;
        }

        public ITodoRepository TodoRepository { get { return todoRepository ?? new TodoRepository(_context); } }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}