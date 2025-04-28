

using MediatR;

namespace Application.Features.Commands.SendMessage;

public record SendMessageCommand(string UserId,
    string Text,
    bool IsClientResponse = true) :IRequest;