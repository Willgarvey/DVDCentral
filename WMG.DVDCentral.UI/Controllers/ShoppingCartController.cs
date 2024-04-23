using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.UI.Models;
using WMG.DVDCentral.UI.ViewModels;

namespace WMG.DVDCentral.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        // GET: ShoppingCartController
        public ActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();

            return View(cart);
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        private void SetShoppingCart(ShoppingCart cart)
        {
            HttpContext.Session.SetObject("cart", cart);
        }

        // Retrieve the user object from session
        private User GetUser()
        {
            if (HttpContext.Session.GetObject<User>("user") != null)
            {
                return HttpContext.Session.GetObject<User>("user");
            }
            else
            {
                return new BL.Models.User();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();
            Movie movie = cart.Items.FirstOrDefault(i => i.Id == id);
            ShoppingCartManager.Remove(cart, movie);
            HttpContext.Session.SetObject("cart", cart); // Write the new cart value into session over the old value
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            Movie movie = MovieManager.LoadById(id);

            ShoppingCartManager.Add(cart, movie);
            HttpContext.Session.SetObject("cart", cart); // Write the new cart value into session over the old value
            return RedirectToAction(nameof(Index), "Movie");
        }

        public IActionResult Checkout()
        {

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                User user = new User();
                cart = GetShoppingCart();
                user = GetUser();

                if (cart.NumberOfItems > 0)
                {
                    ShoppingCartManager.Checkout(cart, user);
                    HttpContext.Session.SetObject("cart", null); // Write the new cart value into session over the old value
                }
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpGet]
        public IActionResult AssignToCustomer()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                // Get the user from session
                User user = new User();
                user = GetUser();

                // Instantiate a new instance of the CustomerVM view model
                CustomerVM customerVM = new CustomerVM();

                // Instantiate a new instance of the Customer object
                Customer customer = new Customer();

                //Get and Put the cart into the ViewModel
                cart = GetShoppingCart();
                customerVM.Cart = cart;

                //Load ViewModel.Customers List
                customerVM.Customers = CustomerManager.Load();

                //Set the UserId in the ViewModel
                customerVM.UserId = user.Id;

                // if the UserId has any customers. set the ViewModel.CustomerId to the first one
                if (customerVM != null)
                {
                    customerVM.CustomerId = customerVM.Customers[0].Id;
                }

                // Put the ViewModel in session
                HttpContext.Session.SetObject("customerVM", customerVM);

                // Set the ViewData["ReturnUrl"] to the UriHelper.GetDisplayUrl(HttpContext.Request);
                ViewData["ReturnUrl"] = UriHelper.GetDisplayUrl(HttpContext.Request);

                // return the view with the ViewModel as the model
                return View(customerVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }

        [HttpPost]
        public IActionResult AssignToCustomer(CustomerVM customerVM)
        {
            try
            {
                // Get and assign the ViewModel.Cart
                cart = GetShoppingCart();
                customerVM.Cart = cart;

                // Add the Order like you did in the Checkout Method for Checkpoint #7
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    User user = new User();
                    user = GetUser();
                    user.CustomerId = customerVM.CustomerId;

                    if (cart.NumberOfItems > 0)
                    {
                        ShoppingCartManager.Checkout(cart, user); // Add the Order
                        // Clear the Shopping Cart
                        HttpContext.Session.SetObject("cart", null);
                    }
                    // Show the Thank you for your order screen
                    //return View();
                    return RedirectToAction("Checkout", "ShoppingCart");
                }
                else
                    return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            

        }
    }
}
