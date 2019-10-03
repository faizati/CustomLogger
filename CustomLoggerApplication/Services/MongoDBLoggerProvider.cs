using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomLoggerApplication.Services
{
    public class MongoDBLoggerProvider : ILoggerProvider
    {
        private readonly MongoDBLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, MongoDBLogger> _loggers = new ConcurrentDictionary<string, MongoDBLogger>();

        public MongoDBLoggerProvider(MongoDBLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new MongoDBLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
