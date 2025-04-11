namespace Schat.Common.Configuration;

public class JwtConfig
{
    public required string Secret { get; init; }
    
    public required string Issuer { get; init; }
    
    public required string Audience { get; init; }
    
    public required int Duration { get; init; }
}