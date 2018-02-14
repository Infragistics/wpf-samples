
using System.Collections.Generic;
namespace IgOutlook.Modules.Mail
{
    public class FolderParameters
    {
        public const string FolderKey = "Folder";

        public const string Inbox = "Inbox";
        public const string Drafts = "Drafts";
        public const string Sent = "Sent";
        public const string Deleted = "Deleted";

        public static Dictionary<string, string> LocalizedFolderNames = new Dictionary<string, string>
        { 
            {FolderParameters.Inbox, Resources.ResourceStrings.MailGroup_Inbox_Text},
            {FolderParameters.Drafts, Resources.ResourceStrings.MailGroup_Drafts_Text},
            {FolderParameters.Sent, Resources.ResourceStrings.MailGroup_SentItems_Text},
            {FolderParameters.Deleted, Resources.ResourceStrings.MailGroup_DeletedItems_Text}
        };
    }

    public class MessageParameters
    {
        public const string MessageModeKey = "MessageMode";

        public const string New = "New";
        public const string Reply = "Reply";
        public const string ReplyAll = "ReplyAll";
        public const string Forward = "Forward";
        public const string View = "View";


        public const string MessageId = "MessageId";

    }
}
