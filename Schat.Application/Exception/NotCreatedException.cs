using System;

namespace Schat.Application.Exception;

public class NotCreatedException : System.Exception
{
    public NotCreatedException() {}
    
    public NotCreatedException(string message) : base(message) {}
        
    public NotCreatedException(string message, System.Exception innerException) : base(message, innerException) {}
}