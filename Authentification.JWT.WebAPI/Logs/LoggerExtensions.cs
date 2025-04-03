namespace Authentification.JWT.WebAPI.Logs
{
    public static class LoggerExtensions
    {
        public static void LogInfo(this ILogger logger, string message, params object[] args)
        {
            logger.LogInformation($"[INFO] {message}", args);
        }

        public static void LogErr(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.LogError(exception, $"[ERROR] {message}", args);
        }


    }
}
