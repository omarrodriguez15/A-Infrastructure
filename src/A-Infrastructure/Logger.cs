using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace AInfrastructure
{
    public interface ILogger
    {
        void LogTrace(Func<string> msgFunc);
        void LogTrace(string msg);
        void LogDebug(Func<string> msgFunc);
        void LogDebug(string msg);
        void LogWarn(Func<string> msgFunc);
        void LogWarn(string msg);
        void LogError(Func<string> msgFunc);
        void LogError(string msg);
        void LogException(Exception ex, string msg = null);
        void LogException(Exception ex, Func<string> msgFunc);
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Warn,
        Error
    }

    public class BaseFileLogger : BaseLogger
    {
        public string FilePath { set => _filePath = value; }

        protected string _filePath;
        protected static object _locker = new object();

        public BaseFileLogger(string filePath) : base()
        {
            _filePath = filePath;
            SystemFilePath.CreateDirectory(_filePath);
        }

        protected override void Log(string msg, LogLevel level, string callee)
        {
            msg = $"{DateTime.Now} | {Thread.CurrentThread.ManagedThreadId} | {level} | {callee} | {msg}";

            Console.WriteLine(msg);

            lock(_locker)
            {
                File.AppendAllText(_filePath, $"{msg}\r\n");
            }
        }
    }

    public class BaseLogger : ILogger
    {
        /// <summary>
        /// Using the stack trace class to get the last method
        /// caller. This must be called by the inital method called.
        /// </summary>
        /// <value>The name of the callee.</value>
        protected string _calleeName => (new StackTrace())?.GetFrame(2)?.GetMethod()?.Name;

        protected virtual void Log(string msg, LogLevel level, string callee)
        {
            msg = $"{DateTime.Now} | {Thread.CurrentThread.ManagedThreadId} | {level} | {callee} | {msg}";
            Console.WriteLine(msg);
        }

        protected virtual void Log(Func<string> msgFunc, LogLevel level, string callee)
        {
            Log(msgFunc?.Invoke(), level, callee);
        }

        public virtual void LogDebug(Func<string> msgFunc)
        {
            Log(msgFunc, LogLevel.Debug, _calleeName);
        }

        public virtual void LogDebug(string msg)
        {
            Log(msg, LogLevel.Debug, _calleeName);
        }

        public virtual void LogError(Func<string> msgFunc)
        {
            Log(msgFunc, LogLevel.Error, _calleeName);
        }

        public virtual void LogError(string msg)
        {
            Log(msg, LogLevel.Error, _calleeName);
        }

        public virtual void LogException(Exception ex, string msg = null)
        {
            msg = $"{msg} \r\n Exc: {ex?.Message}";
            Log(msg, LogLevel.Error, _calleeName);
        }

        public virtual void LogException(Exception ex, Func<string> msgFunc)
        {
            var msg = $"{msgFunc?.Invoke()} \r\n Exc: {ex?.Message}";
            Log(msg, LogLevel.Error, _calleeName);
        }

        public virtual void LogTrace(Func<string> msgFunc)
        {
            Log(msgFunc, LogLevel.Trace, _calleeName);
        }

        public virtual void LogTrace(string msg)
        {
            Log(msg, LogLevel.Trace, _calleeName);
        }

        public virtual void LogWarn(Func<string> msgFunc)
        {
            Log(msgFunc, LogLevel.Warn, _calleeName);
        }

        public virtual void LogWarn(string msg)
        {
            Log(msg, LogLevel.Warn, _calleeName);
        }
    }
}
