using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Models;
using WMG.DVDCentral.UI.ViewModels;

namespace WMG.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Orders";
            CustomerOrdersVM customerOrdersVM = new CustomerOrdersVM();
            return View(customerOrdersVM);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details - Order #" + id;
            OrderDetailsVM orderDetailsVM = new OrderDetailsVM(id);
            return View(orderDetailsVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Create an Order";
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Edit Order #" + id;
                OrderDetailsVM orderDetailsVM = new OrderDetailsVM(id);
                return View(orderDetailsVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Edit(int id, Order order, bool rollback = false) 
        {
            try
            {
                int result = OrderManager.Update(order, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(order);
            }       
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                OrderDetailsVM orderDetailsVM = new OrderDetailsVM(id);
                return View(orderDetailsVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Delete(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(order);
            }
        }
    }
}
