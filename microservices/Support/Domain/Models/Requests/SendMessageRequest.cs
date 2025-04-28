

namespace Domain.Models.Requests;

public record SendMessageRequest(string Text,bool IsClientResponse=true);