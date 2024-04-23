using Microsoft.EntityFrameworkCore.Storage;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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
            Assert.AreEqual(3, dc.tblDirectors.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblDirector entity = new tblDirector();
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.Id = -99;

            // Add the entity to the databasse
            dc.tblDirectors.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblDirector - Use the first one.
            tblDirector entity = dc.tblDirectors.FirstOrDefault();

            //Change property values
            entity.FirstName = "ChangeTest";
            entity.LastName = "ChangeTest";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblDirector WHERE Id = 4
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblDirectors.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
