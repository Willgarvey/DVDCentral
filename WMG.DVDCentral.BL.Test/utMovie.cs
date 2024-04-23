using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, MovieManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = MovieManager.Insert("Title", "Desc", 1,1,1,1,1,"Path", ref id, true); // True enables allow rollback
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Movie movie = new Movie
            {
                Title = "Test",
                Description = "Test",
                FormatId = 1,
                DirectorId = 1,
                RatingId = 1,
                Cost = 1,
                InStkQty = 1,
                ImagePath = "Test"

            };

            int results = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(1);
            movie.Title = "Test";
            movie.Description = "Test";
            movie.FormatId = 1;
            movie.RatingId = 1;
            movie.Cost = 1;
            movie.InStkQty = 1;
            movie.ImagePath = "Test";

            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}

