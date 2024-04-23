using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Models;

namespace WMG.DVDCentral.UI.Controllers
{
    public class DirectorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Directors";
            return View(DirectorManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(DirectorManager.LoadById(id));
        }


        public IActionResult Create()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Create a Director";
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Create(Director director)
        {
            try
            {
                int result = DirectorManager.Insert(director);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception)
            {

            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Edit a Director";
                return View(DirectorManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }

        [HttpPost]
        public IActionResult Edit(int id, Director director, bool rollback = false)
        {
            try
            {
                int result = DirectorManager.Update(director, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(director);
            }
        }

        public IActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Delete a Director";
                return View(DirectorManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }

        [HttpPost]
        public IActionResult Delete(int id, Director director, bool rollback = false)
        {
            try
            {
                int result = DirectorManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(director);
            }
        }

    }
}
