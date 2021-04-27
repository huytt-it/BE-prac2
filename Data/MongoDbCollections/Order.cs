using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.MongoDbCollections
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public List<Cart> Cart { get; set; }
        public string Status { get; set; }
    }
}
