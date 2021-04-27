using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.MongoDbCollections;
using Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson.Serialization.IdGenerators;
using Services.Services;

namespace BE_prac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerServices _customerServices;

        public CustomersController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var customer = _customerServices.Authenticate(model.Username, model.Password);
            if (customer == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(customer);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _customerServices.GetAll();
            return Ok(users);
        }


        [HttpPost]
        public ActionResult CreateCustomer(Customer inCustomer)
        {
            var customer = _customerServices.GetUserName(inCustomer.UserName);
            if (customer != null)
            {
                return BadRequest(new { message = "User already exists" });
            }
            _customerServices.CreateCustomer(inCustomer);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteCustomer(string id)
        {
            var product = _customerServices.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            _customerServices.DeleteCustomer(id);
            return NoContent();
        }
    }
}
