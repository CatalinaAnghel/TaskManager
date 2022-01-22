namespace TaskManager.ApplicationLogic.Services.Abstractions
{
    public interface ILoggerManager
    {
        public void LogDebug(string message);
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
    }
}
