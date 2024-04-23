using Microsoft.AspNetCore.Mvc;

namespace WMG.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Formats";
            return View(FormatManager.Load());
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(FormatManager.LoadById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create a Format";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Format format)
        {
            try
            {
                int result = FormatManager.Insert(format);
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
            ViewBag.Title = "Edit a Format";
            return View(FormatManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Update(format, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(format);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Format";
            return View(FormatManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Delete(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(format);
            }
        }

    }
}
