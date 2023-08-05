using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using System.Data.SqlClient;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Add to Cart
        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.addToCart(cart, connection);
            return response;

        }

        //Add to Cart
        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.placeOrder(users, connection);
            return response;

        }

        // Order list
        [HttpPost]
        [Route("orders")]
        public Response Orders(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.Orders(users, connection);
            return response;
           
        }



    }
}
