using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.ICommands;

namespace Todo.Domain.Commands;
public abstract class MarkCommand : Notifiable, ICommand
{
    public MarkCommand()
    {
    }
    public MarkCommand(Guid id, string user)
    {
        Id = id;
        User = user;
    }

    public Guid Id { get; set; }
    public string User { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(User, 6, "User", "Verificar o Utilizador"));
    }
}
