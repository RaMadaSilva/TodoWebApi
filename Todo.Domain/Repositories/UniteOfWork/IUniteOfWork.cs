using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories.UniteOfWork
{
    public interface IUniteOfWork
    {
        ITodoRepository TodoRepository { get; }
        void Commit();
    }
}