namespace Schat.Application.DTO.Signin;

public record SigninRequest(
    string Email,
    string Password);