using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.UI.ViewModels
{
    public class CustomerOrdersVM
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Customer> Customers { get; set; } = new List<Customer>();


        public CustomerOrdersVM()
        {

            Orders = OrderManager.Load();
            Users = UserManager.Load();
            Customers = CustomerManager.Load();

            foreach (Order order in Orders)
            {
                order.CalculateTotalCost();
            }
        }
    }


}
