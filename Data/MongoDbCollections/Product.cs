using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.MongoDbCollections
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }
        [BsonElement("catelogy")]
        public string Catelogy { get; set; }
        [BsonElement("dateCreate")]
        public string DateCreate { get; set; }
        [BsonElement("img")]
        public string Img { get; set; }

    }
}
