using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Interfaces;
using Todo.Domain.ICommands;
using Todo.Domain.Repositories.UniteOfWork;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTitleCommand>,
        IHandler<MarkAsDoneCommand>,
        IHandler<MarkAsUnDoneCommand>
    {
        private readonly IUniteOfWork _uow;

        public TodoHandler(IUniteOfWork uow)
        {
            _uow = uow;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            //pega todas as notificações Fail Fast Validate
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "ops Dados Invalido", command.Notifications);

            //Criar o TodoItem
            var todo = new TodoItem(command.Title, command.Date, command.User);

            //Salvar no banco, 
            _uow.TodoRepository.Create(todo);
            _uow.Commit();

            //retornar o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(UpdateTitleCommand command)
        {
            //Fail Fast Validate
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "ops Dados Invalido", command.Notifications);

            //pegar o Todo do banco de dados, 
            var todo = _uow.TodoRepository.GetById(command.Id, command.User);

            //Alterar o title
            todo.UpdateTitle(command.Title);

            //Salva no banco de dados 
            _uow.TodoRepository.Update(todo);
            _uow.Commit();

            //retorna o resultado
            return new GenericCommandResult(true, "Titulo actualizado", todo);
        }

        public ICommandResult Handle(MarkAsDoneCommand command)
        {
            //Fail Fast Validade
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops Dados Invalidos", command.Notifications);

            //pega O TodoItem do banco de dados
            var todo = _uow.TodoRepository.GetById(command.Id, command.User);

            // Caso a tarefa não exista;
            if (todo is null)
                return new GenericCommandResult(false, "Não existe a tarefa informada", null);

            //alterar o estado da tarefa
            todo.MarkAsDone();

            //Salvar no banco de dados 
            _uow.TodoRepository.Update(todo);
            _uow.Commit();

            //retornar o resultado
            return new GenericCommandResult(true, "Tarefa Marcada como feito", todo);

        }

        public ICommandResult Handle(MarkAsUnDoneCommand command)
        {
            //Fail Fast Validade
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops Dados Invalidos", command.Notifications);

            //pega O TodoItem do banco de dados
            var todo = _uow.TodoRepository.GetById(command.Id, command.User);

            // Caso a tarefa não exista;
            if (todo is null)
                return new GenericCommandResult(false, "Não existe a tarefa informada", null);

            //alterar o estado da tarefa
            todo.MarkAsUnDone();

            //Salvar no banco de dados 
            _uow.TodoRepository.Update(todo);
            _uow.Commit();

            //retornar o resultado
            return new GenericCommandResult(true, "Tarefa Marcada como não feito", todo);
        }
    }
}