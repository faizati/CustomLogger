using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomLoggerApplication.Services
{
    public class MongoDBLogger : ILogger
    {
        private readonly string _name;
        private readonly MongoDBLoggerConfiguration _config;
        private readonly MongoDBConnector _conn = new MongoDBConnector();


        public MongoDBLogger(string name, MongoDBLoggerConfiguration config)
        {
            _name = name;
            _config = config;
            _conn.dbName = "test";
            _conn.collection = "log";
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
              
                Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
                this.SaveToMongoDB(logLevel, formatter(state, exception).ToString());
            }
        }

        private void SaveToMongoDB(LogLevel logLevel, string message)
        {
            LogData logData = new LogData();
            logData.type = logLevel.ToString();
            logData.message =message;
            _conn.InsertData(logData);
        }
    }
}
