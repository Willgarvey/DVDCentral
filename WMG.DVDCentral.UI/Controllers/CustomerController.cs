using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Models;

namespace WMG.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Customers";
            return View(CustomerManager.Load());
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(CustomerManager.LoadById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Create a Customer";
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                int result = CustomerManager.Insert(customer);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());

                return RedirectToAction(nameof(Index), "Customer");
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
                ViewBag.Title = "Edit a Customer";
                return View(CustomerManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Edit(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Update(customer, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Delete a Customer";
                return View(CustomerManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Delete(int id, Customer customer, bool rollback = false)
        {
            try
            {
                int result = CustomerManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(customer);
            }
        }
    }
}
