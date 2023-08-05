using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapp.Models;
using System.Data.SqlClient;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //Registration
        [HttpPost]
        [Route("registration")]
        public Response register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
           response = dal.register(users, connection);
            return response;
            
        }
        
        //Login
        [HttpPost]
        [Route("login")]
        public Response login(Users users) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.login(users, connection);
            return response;
        }

        //ViewUser
        [HttpPost]
        [Route("user")]
        public Response user(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.viewUser(users, connection);
            return response;
        }

        //Update Profile
        [HttpPost]
        [Route("updateProfile")]
        public Response UpdateProfile(Users users)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = new Response();
            response = dal.UpdateProfile(users, connection);
            return response;

        }
    }
}
