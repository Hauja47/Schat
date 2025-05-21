using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Schat.Application.Exception;
using Schat.Common.Constants;

namespace Schat.Infrastructure.Factory;

public class EmailVerificationLinkFactory(
    IHttpContextAccessor httpContextAccessor, 
    LinkGenerator linkGenerator)
{
    public string Create(string emailVerificationToken, string userEmail)
    {
        // string? verificationLink = linkGenerator.GetUriByName(
        //     httpContextAccessor.HttpContext!, 
        //     EndpointName.VerifyEmail,
        //     new
        //     {
        //         token = emailVerificationToken
        //     });
        
        string? verificationLink = linkGenerator.GetUriByName(
            httpContextAccessor.HttpContext!,
            endpointName: EndpointName.VerifyEmail,
            values: new
            {
                confirmationToken = emailVerificationToken,
                userEmail
            });
        
        return  verificationLink ?? throw new NotCreatedException("Email verification could not be created.");
    }
}