using Microsoft.EntityFrameworkCore.Storage;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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
            Assert.AreEqual(3, dc.tblOrderItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblOrderItem entity = new tblOrderItem();
            entity.OrderId = 99;
            entity.Quantity = 99;
            entity.MovieId = 99;
            entity.Cost = 99.99;

            // Add the entity to the databasse
            dc.tblOrderItems.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblOrderItem - Use the first one.
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            //Change property values
            entity.OrderId = 99;
            entity.Quantity = 99;
            entity.MovieId = 99;
            entity.Cost = 99.99;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblOrderItem WHERE Id = 4
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblOrderItems.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
