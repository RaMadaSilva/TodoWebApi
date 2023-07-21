using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.ICommands;

namespace Todo.Domain.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult()
        {
        }
        public GenericCommandResult(bool sucesses, string message, object data)
        {
            Sucesses = sucesses;
            Message = message;
            Data = data;
        }

        public bool Sucesses { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}