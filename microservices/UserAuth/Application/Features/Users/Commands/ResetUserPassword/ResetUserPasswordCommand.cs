

using MediatR;

namespace Application.Features.Users.Commands.ResetUserPassword;

public record ResetUserPasswordCommand(string Email, string Token, string NewPassword) : IRequest<ResetUserPasswordResponse>;