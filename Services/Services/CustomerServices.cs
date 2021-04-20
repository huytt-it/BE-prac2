using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BE_prac2.Helpers;
using Data.DataAcess;
using Data.MongoDbCollections;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;


namespace Services.Services
{
    public interface ICustomerServices
    {
        Customer Authenticate(string username, string password);
        IEnumerable<Customer> GetAll();
        void CreateCustomer(Customer inCustomer);

    }

    public class CustomerServices:ICustomerServices
    {
        private readonly MongoDbContext _db;
        private readonly AppSettings _appSettings;
        public CustomerServices(IOptions<AppSettings> appSettings,MongoDbContext db)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public Customer Authenticate(string username, string password)
        {
            var customer = _db._customer.Find(cus => cus.UserName.Equals(username) && cus.Password.Equals(password)).FirstOrDefault();
           
            // return null if user not found
            if (customer == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,customer.Id.ToString()),
                    new Claim(ClaimTypes.Role, customer.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            customer.Token = tokenHandler.WriteToken(token);

            return customer;
        }

        public IEnumerable<Customer> GetAll() => _db._customer.Find(cus => true).ToList();

        public void CreateCustomer(Customer inCustomer)
        {
            _db._customer.InsertOne(inCustomer);
        }
        
    }
}
