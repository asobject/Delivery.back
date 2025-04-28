using Domain.Entities;
using Domain.Interfaces.Notifications;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Commands.SendMessage;

public class SendMessageHandler(
    IChatRepository repo,
    IChatNotifier notifier) : IRequestHandler<SendMessageCommand>
{
    public async Task Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var message = new ChatMessage
        {
            UserId = request.UserId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            IsClientResponse = request.IsClientResponse
        };

        await repo.AddAsync(message);
        await notifier.NotifyNewMessage(message);
    }
}