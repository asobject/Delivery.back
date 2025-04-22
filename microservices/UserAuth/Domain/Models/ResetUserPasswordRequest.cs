

namespace Domain.Models;

public record ResetUserPasswordRequest(string Email, string Token, string NewPassword);
