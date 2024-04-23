using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.BL;
using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        [HttpGet] // Need to add the HttpGet here unlike other controllers
        public IEnumerable<Rating> Get()
        {
            return RatingManager.Load();
        }

        [HttpGet("{id}")] // Grabs the additional information that is the id
        public Rating Get(int id)
        {
            return RatingManager.LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rating rating)
        {
            try
            {
                int results = RatingManager.Insert(rating);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Rating rating)
        {
            try
            {
                int results = RatingManager.Update(rating);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) // You will not see this is it's set to private
        {
            try
            {
                int results = RatingManager.Delete(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
