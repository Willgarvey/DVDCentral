using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.UI.Models;
using WMG.DVDCentral.UI.ViewModels;

namespace WMG.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "List of all Movies";
            return View("IndexCard", MovieManager.Load());
        }
        [HttpGet]
        public IActionResult IndexCard()
        {
            ViewBag.Title = "List of all Movies";
            return View("IndexCard", MovieManager.Load());
        }
        [HttpGet]
        public IActionResult Browse(int id)
        {
            var results = MovieManager.LoadById(id);
            ViewBag.Title = "List of " + results.Description + " Movies";
            return View(nameof(Index), MovieManager.Load(id)); // Use the Movie Index View
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details of a Movie";
            return View(MovieManager.LoadById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {

                MovieVM movieVM = new MovieVM();
                ViewBag.Title = "Create a Movie";
                return View(movieVM);


        }
        [HttpPost]
        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                //Insert the movie
                int result = MovieManager.Insert(movieVM.Movie);

                IEnumerable<int> newGenreIds = new List<int>();

                if(movieVM.GenresIds != null)
                    newGenreIds = movieVM.GenresIds;

                //Insert each entry for movie genre selections
                newGenreIds.ToList().ForEach(a => MovieGenreManager.Insert( movieVM.Movie.Id , a));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create a Movie";
                ViewBag.Error = ex.Message;
                throw;
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            MovieVM movieVM = new MovieVM(id); // Uses the MovieVM constructor
            ViewBag.Title = "Edit " + movieVM.Movie.Title;
            HttpContext.Session.SetObject("genreids", movieVM.GenresIds);
            return View(movieVM);

        }
        [HttpPost]
        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                movieVM.Movie.Cost = (float)Math.Round(movieVM.Movie.Cost, 2);
                int result = MovieManager.Update(movieVM.Movie, rollback); // the rollback it apart of the Insert manager because of the tests we wrote

                IEnumerable<int> newGenreIds = new List<int>();

                if (movieVM.GenresIds != null)
                    newGenreIds = movieVM.GenresIds;

                IEnumerable<int> oldGenreIds = new List<int>();
                oldGenreIds = GetObject();

                IEnumerable<int> deletes = oldGenreIds.Except(newGenreIds);
                IEnumerable<int> adds = newGenreIds.Except(oldGenreIds);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(movieVM.Movie.Id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Insert(movieVM.Movie.Id, a));

               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit " + movieVM.Movie.Title; ;
                ViewBag.Error = ex.Message;
                return View(movieVM);
            }
            
        }
        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("genreids") != null)
            {
                return HttpContext.Session.GetObject<IEnumerable<int>>("genreids");
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ViewBag.Title = "Delete a Movie";
                return View(MovieManager.LoadById(id));
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

        }
        [HttpPost]
        public IActionResult Delete(int id,Movie movie, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movie);
            }
        }
    }
}
