using System.Collections.Generic;
using Data.DataAcess;
using Data.MongoDbCollections;
using MongoDB.Driver;

namespace Services.Services
{
    public interface IProductServices
    {
        List<Product> Get();
        Product Get(string id);

        void UpdateProduct(string id, Product inProduct);
        void CreateProduct(Product inProduct);
        void DeleteProduct(Product inProduct);
        void DeleteProduct(string id);

    }
    public class ProductServices:IProductServices
    {
        private readonly MongoDbContext _db;

        public ProductServices(MongoDbContext db)
        {
            _db = db;
        }

        public List<Product> Get() => _db._product.Find(p => true).ToList();
        public Product Get(string id) => _db._product.Find<Product>(p => p.Id.Equals(id)).FirstOrDefault();

        public void UpdateProduct(string id,Product inProduct)
        {
            _db._product.ReplaceOne(pd => pd.Id == id, inProduct);
        }
        public void CreateProduct(Product inProduct)
        {
            _db._product.InsertOne(inProduct);
        }
        public void DeleteProduct(Product inProduct)
        {
            _db._product.DeleteOne(pd => pd.Id == inProduct.Id);
        }
        public void DeleteProduct(string id)
        {
            _db._product.DeleteOne(pd => pd.Id == id);
        }
    }
}
