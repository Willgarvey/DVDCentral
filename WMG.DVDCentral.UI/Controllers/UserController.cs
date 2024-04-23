using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Extensions;

namespace WMG.DVDCentral.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Users";
            return View(UserManager.Load());
        }

        //private void GetBands()
        //{
        //    if (HttpContext.Session.GetObject<Band[]>("bands") != null)
        //    {
        //        bands = HttpContext.Session.GetObject<Band[]>("bands");
        //    }

        //    else
        //    {
        //        LoadBands();
        //    }
        //}

        //private User GetUser()
        //{
        //    // Retrieve the user object from session
        //    return HttpContext.Session.GetObject<User>("user");
        //}

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);

            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
            }
        }

        public IActionResult Logout()
        {
            ViewBag.Title = "Logout";
            SetUser(null);
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                bool result = UserManager.Login(user);
                SetUser(user);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());

                return RedirectToAction(nameof(Index), "Movie");
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details for User";
            return View(UserManager.LoadById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                ViewBag.Title = "Create a User";
                int result = UserManager.Insert(user);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Title = "Create";
                ViewBag.Error = ex.Message;
                return View();

            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit a User";
            return View(UserManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, User user, bool rollback = false)
        {
            try
            {
                int result = UserManager.Update(user, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }
    }
}
