using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class TodoQuery
    {
        public static Expression<Func<TodoItem, bool>> GetAll(string user)
        {
            return x => x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetAllDone(string user)
        {
            return x => x.User == user && x.Done == true;
        }

        public static Expression<Func<TodoItem, bool>> GetAllUndone(string user)
        {
            return x => x.User == user && x.Done == false;
        }
        public static Expression<Func<TodoItem, bool>> GetByPeriodo(string user, DateTime date, bool done)
        {
            return x =>
                x.User == user &&
                x.Date.Date == date.Date &&
                x.Done == done;
        }
    }
}