using System;
using System.Collections.Generic;
using System.Text;
using Data.MongoDbCollections;
using MongoDB.Driver;

namespace Data.DataAcess
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;
        private IMongoClient _mongoClient;

        public MongoDbContext(IMongoClient client, string databaseName)
        {
            _db = client.GetDatabase(databaseName);
            _mongoClient = client;
        }
        public IClientSessionHandle StartSession()
        {
            var session = _mongoClient.StartSession();
            return session;
        }

        public IMongoCollection<Product> _product => _db.GetCollection<Product>("product");
        public IMongoCollection<Customer> _customer => _db.GetCollection<Customer>("customer");

    }
}
