using Microsoft.EntityFrameworkCore.Storage;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
            Assert.AreEqual(3, dc.tblOrders.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblOrder entity = new tblOrder();
            entity.CustomerId = 99;
            entity.OrderDate = DateTime.Now;
            entity.ShipDate = DateTime.Now;
            entity.UserId = 99;


            // Add the entity to the databasse
            dc.tblOrders.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblOrder - Use the first one.
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            //Change property values
            entity.CustomerId = 99;
            entity.OrderDate = DateTime.Now;
            entity.ShipDate = DateTime.Now;
            entity.UserId = 99;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblOrder WHERE Id = 4
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblOrders.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
