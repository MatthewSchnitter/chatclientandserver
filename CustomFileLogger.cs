using Microsoft.Extensions.Logging;

/// <summary> 
/// Author:    Griffin Shannon and Matthew Schnitter
/// Date:      4/11/2022 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500, Griffin Shannon, and Matthew Schnitter - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Matthew Schnitter, and I, Griffin Shannon, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// Contains the logger which logs to a file 
///  
/// </summary>

namespace FileLogger
{
    /// <summary>
    /// File logger for the GUI
    /// </summary>
    public class CustomFileLogger : ILogger
    {
        private readonly string _fileName;
        private readonly string _categoryName;

        /// <summary>
        /// Creates new file logger
        /// </summary>
        /// <param name="categoryName"></param>
        public CustomFileLogger(string categoryName)
        {
            _categoryName = categoryName;
            _fileName = "Log_CS3500_Assignment7";
        }

        /// <summary>
        /// Scope determines which areas of code/variables to trace.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if log is enabled
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs data to a file
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"> level to log </param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string result = DateTime.Now.ToString() + $": {logLevel.ToString()} :" + formatter(state, exception) + Environment.NewLine;
            File.AppendAllText(_fileName, result);
        }
    }
}