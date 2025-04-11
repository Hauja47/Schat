namespace Schat.Domain.Enum;

public enum MessageStatus
{
    Error = -1,
    Sending = 1,
    Sent = 2,
    Seen = 3,
    Deleted = 4
}