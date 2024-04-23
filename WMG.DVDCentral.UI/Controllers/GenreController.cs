using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Models;

namespace WMG.DVDCentral.UI.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of all Genres";
            return View(GenreManager.Load());
        }

        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details of a Genre";
            return View(GenreManager.LoadById(id));
        }


        public IActionResult Create() 
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Create a Genre";
                return View();
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            try
            {
                int result = GenreManager.Insert(genre);
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
                ViewBag.Title = "Edit a Genre";
                return View(GenreManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }

        [HttpPost]
        public IActionResult Edit(int id, Genre genre, bool rollback = false)
        {
            try
            {
                int result = GenreManager.Update(genre, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(genre);
            }
        }

        public IActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Delete a Genre";
                return View(GenreManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }

        [HttpPost]
        public IActionResult Delete(int id, Genre genre, bool rollback = false)
        {
            try
            {
                int result = GenreManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(genre);
            }
        }

    }
}
