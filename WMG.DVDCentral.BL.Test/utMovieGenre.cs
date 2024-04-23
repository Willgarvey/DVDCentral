using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre
    {
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Genre genre = new Genre
            {
                Description = "Test"
            };

            int results = MovieGenreManager.Insert(1, 1, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadById(3);
            genre.Description = "Test";
            int results = GenreManager.Update(genre, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieGenreManager.Delete(3,3, true);
            Assert.AreEqual(1, results);
        }
    }
}

