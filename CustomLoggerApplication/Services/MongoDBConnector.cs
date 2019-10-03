using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomLoggerApplication.Services
{
    public class MongoDBConnector
    {
        public string dbName { get; set; } = null;
        public string collection { get; set; } = null;

        MongoClient connection;

        IMongoCollection<Object> _collection;

        private void Connect()
        {
            connection = new MongoClient();
        }

        private void GetDBAndCollection()
        {
            var db = connection.GetDatabase(dbName);
            _collection = db.GetCollection<Object>(collection);
        }

        public void InsertData(Object data)
        {
            Connect();
            GetDBAndCollection();
            _collection.InsertOne(data);
        }
    }
}
