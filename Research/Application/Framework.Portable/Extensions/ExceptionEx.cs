
namespace System
{
    public static class ExceptionEx
    {
        public static string ToString(this Exception error, bool showType = true)
        {
            var message = "";
            if (showType)
            {
                message += error.GetType().Name + "\n";
                message += "-------------------------------------------------\n";
            }
            message += error.Message + "\n"; // + "\n" + error.StackTrace;
            if (error.InnerException == null)
            {
                message += error.StackTrace + "\n";
                message += "-------------------------------------------------\n";
            }
            else
            {
                message += error.InnerException.ToString(showType);
            }
            return message;
        }
    }
}
