namespace WMG.DVDCentral.UI.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public int UserId { get; set; }
        public ShoppingCart Cart { get; set; } = new ShoppingCart();    

        public CustomerVM()
        {
            Customers = CustomerManager.Load();
            CustomerId = Customers[0].Id;
        }


    }
}
