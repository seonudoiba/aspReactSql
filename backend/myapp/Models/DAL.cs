using System.Data.SqlClient;
using System.Data;

namespace myapp.Models
{
    public class DAL
    {
        //Register 
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "User registration failed";
            }

            return response;
        }
       //Login
        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("sp_login", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Email", users.Email.ToLower());
            adapter.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0) {
                // Accessing the first row
                DataRow firstRow = dt.Rows[0];

                // Now, you can access the values of the columns from the first row
                user.Id = Convert.ToInt32(firstRow["Id"]);
                user.FirstName = Convert.ToString(firstRow["FirstName"]);
                user.LastName = Convert.ToString(firstRow["LastName"]);
                user.Email = Convert.ToString(firstRow["Email"]);
                user.Type = Convert.ToString(firstRow["Type"]);

                response.Statuscode = 200;
                response.StatusMessage = "User is Valid";
                response.user = user;
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "User is invalid";
                response.user = null;
            }
            return response;
        }

        //View User
        public Response viewUser(Users users, SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("p_viewUser", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ID", users.Id);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count > 0)
            {
                // Accessing the first row
                DataRow firstRow = dt.Rows[0];

                // Now, you can access the values of the columns from the first row
                user.Id = Convert.ToInt32(firstRow["Id"]);
                user.FirstName = Convert.ToString(firstRow["FirstName"]);
                user.LastName = Convert.ToString(firstRow["LastName"]);
                user.Email = Convert.ToString(firstRow["Email"]);
                user.Type = Convert.ToString(firstRow["Type"]);
                user.Fund = Convert.ToDecimal(firstRow["Fund"]);
                user.CreatedOn = Convert.ToDateTime(firstRow["CreatedOn"]);
                user.Password = Convert.ToString(firstRow["Password"]);

                response.Statuscode = 200;
                response.StatusMessage = "User exist";
                response.user = user;
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "User does not exist";
                response.user = null;
            }
            return response;
        }

        //Update Profile
        public Response UpdateProfile(Users users, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {

                response.Statuscode = 200;
                response.StatusMessage = "Record Updated";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "Cannot Update Profile";
            }
            return response;
        }

        //Add to Cart
        public Response addToCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {

                response.Statuscode = 200;
                response.StatusMessage = "Item added";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "Item Could not be added";
            }
            return response;
        }

        //Place order
        public Response placeOrder(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", users.Id);
            
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {

                response.Statuscode = 200;
                response.StatusMessage = "Order has been Placed sucessfully";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "Order could not be placed";
            }
            return response;
        }

        //User Order List
        public Response Orders(Users users, SqlConnection connection)
        {
            Response response = new Response();
            List<Orders> Orders = new List<Orders>();
            SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
            da.SelectCommand.Parameters.AddWithValue("@Id", users.Id);

            DataTable dt = new DataTable();
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                // Accessing the first row
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders order = new Orders();
                    order.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    Orders.Add(order);
                }
                if (Orders.Count > 0)
                {
                    response.Statuscode = 200;
                    response.StatusMessage = "Order Details Fetched";
                    response.Orders = listOrder;
                }
                else
                {
                    response.Statuscode = 100;
                    response.StatusMessage = "Order Details not Available";
                    response.Orders = null;
                }
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessage = "Order Details not Available";
                response.Orders = null;
            }
            return response;

        }
    }

        
    }