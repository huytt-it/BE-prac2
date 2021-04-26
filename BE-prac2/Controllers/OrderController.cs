using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.MongoDbCollections;
using Services.Services;

namespace BE_prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() => _orderServices.Get();

        [HttpGet("{userName}")]
       

        public ActionResult<List<Order>> Get(string userName) => _orderServices.Get(userName);

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order inOrder)
        {
            _orderServices.CreateOrder(inOrder);
            return NoContent();
        }
    }
}
