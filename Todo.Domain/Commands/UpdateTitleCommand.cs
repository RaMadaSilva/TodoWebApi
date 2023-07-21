using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.ICommands;

namespace Todo.Domain.Commands
{
    public class UpdateTitleCommand : Notifiable, ICommand
    {
        public UpdateTitleCommand()
        {
        }

        public UpdateTitleCommand(Guid id, string title, string user)
        {
            Id = id;
            Title = title;
            User = user;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract()
                .Requires()
                .HasMinLen(Title, 3, "Title", "Por favor Descreva Melhor o titulo da Tarefa")
                .HasMinLen(User, 6, "User", "Usuario Invalido"));
        }
    }
}