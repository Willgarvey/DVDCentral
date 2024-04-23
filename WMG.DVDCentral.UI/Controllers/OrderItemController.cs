using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.UI.Models;

namespace WMG.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of all Order Items";
            return View(OrderItemManager.Load());
        }

        [HttpGet]
        public IActionResult Remove(int id) // Remove orderItem where orderItem ID = ID and movieID = movieID
        {
             if (Authenticate.IsAuthenticated(HttpContext))
              {
            try
            {
                // Load the order by the id
                Order order = OrderManager.LoadById(id);
                // Remove the OrderItem with the orderItem id provided
                OrderItem orderItem = order.OrderItems.FirstOrDefault(order.OrderItems.FirstOrDefault(oi => oi.Id == id));
                // Load the updated Order
                OrderItemManager.Delete(orderItem.Id);
                return RedirectToAction("Details", "Order", new {id = order.Id}); // Change to no page load
                }

                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
           else
               return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }
    }
}
