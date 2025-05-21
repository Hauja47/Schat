namespace Schat.Application.DTO.Register;

// public record RegisterRequest(
//     string Email,
//     string Password,
//     string UserName,
//     string FullName);
    
public record RegisterRequest(
    string Email,
    string Password,
    string Username,
    string FullName);