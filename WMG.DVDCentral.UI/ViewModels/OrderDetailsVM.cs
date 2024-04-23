namespace WMG.DVDCentral.UI.ViewModels
{
    public class OrderDetailsVM
    {
        public Order Order {  get; set; }
        public List<Movie> Movies { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }

        public OrderDetailsVM(int id)
        {
            Order order = OrderManager.LoadById(id);
            // Select the movieIds from each order item in the order
            List<int> movieIds = order.OrderItems.Select(oi => oi.MovieId).ToList();
            List<Movie> movies = MovieManager.LoadByIds(movieIds);
            User user = UserManager.LoadById(order.UserId);
            Customer customer = CustomerManager.LoadById(order.CustomerId);
            // Assign the Models to the ViewModel
            Order = order;
            Movies = movies;
            User = user;
            Customer = customer;
            Order.CalculateTotalCost();

        }


    }


}
