
namespace IgOutlook.Business.Mail
{
    [System.Flags]
    public enum MailFlags
    {
        None = 0,
        Seen = 1,
        Answered = 2,
        Flagged = 4,
        Deleted = 8,
        Draft = 16,
        Forwarded = 32,
    }
}
