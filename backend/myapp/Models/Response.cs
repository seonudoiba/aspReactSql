namespace myapp.Models
{
    public class Response
    {
        public int Statuscode { get; set; }
        public string StatusMessage { get; set; }
        public List<Users> ListUsers { get; set; }
        public Users user { get; set; }
        public List<Medicines>  ListMedicines { get; set; }
        public Medicines medicine { get; set; }
        public List<Cart> ListCart { get; set; }
        public Cart cart { get; set; }
        public List<Orders> Orders { get; set; }
        public Orders order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderItem orderItem { get; set; }

    }
}
