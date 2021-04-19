namespace VeraDemoNet.Helper
{
    public static class LoggerExtensions
    {
        public const int SafeStringLength = 255;

        public static void SafeInfo(this log4net.ILog logger, string text)
        {
            var safeString = text.Replace("\r", " ").Replace("\n", " ");
            if(safeString.Length > SafeStringLength)
            {
                safeString = safeString.Substring(0, 255);
            }            
            logger.Info(safeString);
        }
    }
}