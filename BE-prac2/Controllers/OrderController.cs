using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.MongoDbCollections;
using Microsoft.AspNetCore.SignalR;
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
            inOrder.Status = "Unconfimred";
            _orderServices.CreateOrder(inOrder);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Order> ConfirmOrder(string id, Order inOrder)
        {
            var order = _orderServices.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            inOrder.Status = "Confirmed";

            _orderServices.UpdateOrder(id, inOrder);
            return inOrder;
        }



    }
}
