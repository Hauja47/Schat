namespace Schat.Application.DTO;

public record SigninRequest(
    string Email,
    string Password);