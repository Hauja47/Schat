namespace Schat.Application.DTO;

public record RegisterRequest(
    string Email,
    string Password,
    string UserName,
    string FullName);