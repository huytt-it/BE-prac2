using System;
using System.Collections.Generic;
using System.Text;
using Data.DataAcess;
using Data.MongoDbCollections;
using MongoDB.Driver;

namespace Services.Services
{
    public interface IOrderServices
    {
        List<Order> Get();
        Order CreateOrder(Order order);
    }

    public class OrderServices : IOrderServices

    {
        private readonly MongoDbContext _db;

        public OrderServices(MongoDbContext db)
        {
            _db = db;
        }

        public List<Order> Get() => _db._order.Find<Order>(or => true).ToList();

        public Order CreateOrder(Order order)
        {
            _db._order.InsertOne(order);
            return order;
        }





    }
}
