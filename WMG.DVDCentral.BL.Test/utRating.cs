using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, RatingManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = RatingManager.Insert("Test", ref id, true); // True enables allow rollback
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Rating genre = new Rating
            {
                Description = "Test"
            };

            int results = RatingManager.Insert(genre, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Rating genre = RatingManager.LoadById(3);
            genre.Description = "Test";
            int results = RatingManager.Update(genre, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = RatingManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}

