using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WMG.DVDCentral.UI.Controllers
{
    public class RatingController : Controller
    {

        #region "Pre-WebAPI"
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Ratings";
            return View(RatingManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(RatingManager.LoadById(id));
        }


        public IActionResult Create()
        {
            ViewBag.Title = "Create a Rating";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Rating rating)
        {
            try
            {
                int result = RatingManager.Insert(rating);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception)
            {

            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit a Rating";
            return View(RatingManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Rating rating, bool rollback = false)
        {
            try
            {
                int result = RatingManager.Update(rating, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(rating);
            }
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Rating";
            return View(RatingManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Rating rating, bool rollback = false)
        {
            try
            {
                int result = RatingManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(rating);
            }
        }

        #endregion

        #region "Web-API"

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7289/api/");
            return client;
        }

        public IActionResult Get()
        {
            ViewBag.Title = "List of all Programs";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Rating").Result; 

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
                 // Deserialize the JSON using Newtonsoft
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
                 // Convert JSON Data into a List of Rating Objects
            List<Rating> programs = items.ToObject<List<Rating>>();

            return View(nameof(Index), programs);
        }
        [HttpGet]
        public IActionResult GetOne(int id)
        {
            ViewBag.Title = "Rating Details";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating Rating = item.ToObject<Rating>();

            return View(nameof(Details), Rating);

        }
        [HttpGet]
        public IActionResult Insert()
        {
            ViewBag.Title = "Create a Rating";
            HttpClient client = InitializeClient();

            HttpResponseMessage response = client.GetAsync("Rating").Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result; // Same
            dynamic items = (JArray)JsonConvert.DeserializeObject(result); // Same

            Rating Rating = new Rating();

            return View(nameof(Create), Rating);

        }

        [HttpPost]
        public IActionResult Insert(Rating Rating)
        {
            try
            {
                HttpClient client = InitializeClient();

                string serializedObject = JsonConvert.SerializeObject(Rating); // convert to json
                var content = new StringContent(serializedObject); // turn json into string
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                HttpResponseMessage response = client.PostAsync("Rating", content).Result;
                return RedirectToAction(nameof(Get));


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Create), Rating);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Update a Rating";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating Rating = item.ToObject<Rating>();

            //Call the API
            response = client.GetAsync("DegreeType").Result;

            // Parse the result
            result = response.Content.ReadAsStringAsync().Result; // Same
            dynamic items = (JArray)JsonConvert.DeserializeObject(result); // Same
            

            return View(nameof(Edit), Rating);

        }

        [HttpPost]
        public IActionResult Update(int id, Rating Rating) // Changed method name and added a parameter
        {
            try
            {
                HttpClient client = InitializeClient();

                string serializedObject = JsonConvert.SerializeObject(Rating); // convert to json
                var content = new StringContent(serializedObject); // turn json into string
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                HttpResponseMessage response = client.PutAsync("Rating/" + id, content).Result; // Changed post to put
                return RedirectToAction(nameof(Get));


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Edit), Rating); // Return to edit if error
            }
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Rating/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            Rating Rating = item.ToObject<Rating>();

            return(View(nameof(Delete), Rating));
        }

        [HttpPost]
        public   IActionResult Remove(int id, Rating Rating)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Rating/" + id).Result;
                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                return (View(nameof(Delete), Rating));
            }
        }



        #endregion

    }
}
