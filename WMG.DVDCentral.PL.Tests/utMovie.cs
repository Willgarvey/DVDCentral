using Microsoft.EntityFrameworkCore.Storage;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
    {
        protected DVDCentralEntities dc; //modular or class level scope
        protected IDbContextTransaction transaction; //modular or class level scope


        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }


        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblMovies.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblMovie entity = new tblMovie();
            entity.Title = "Test";
            entity.Description = "Test";
            entity.FormatId = 1;
            entity.DirectorId = 1;
            entity.DirectorId = 1;
            entity.RatingId = 1;
            entity.Cost = 1;
            entity.InStkQty = 1;
            entity.ImagePath = "Test.png";
            entity.Id = -99;

            // Add the entity to the databasse
            dc.tblMovies.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblMovie - Use the first one.
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            //Change property values
            entity.Title = "Test";
            entity.Description = "Test";
            entity.FormatId = 1;
            entity.DirectorId = 1;
            entity.RatingId = 1;
            entity.Cost = 1;
            entity.InStkQty = 1;
            entity.ImagePath = "Test";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblMovie WHERE Id = 4
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblMovies.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
