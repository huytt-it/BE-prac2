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
        List<Order> Get(string userName);
        Order GetById(string id);
        Order CreateOrder(Order order);
        Order UpdateOrder(string id, Order order);

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

        public List<Order> Get(string userName)
            => _db._order.Find(or => or.UserName == userName).ToList();

        public Order GetById(string id)
            => _db._order.Find(or => or.Id == id).FirstOrDefault();

        public Order UpdateOrder(string id, Order order)
        {
            _db._order.ReplaceOne(or => or.Id == id, order);
            return order;
        }

    }
}
