using Microsoft.EntityFrameworkCore.Storage;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
            Assert.AreEqual(3, dc.tblCustomers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblCustomer entity = new tblCustomer();
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.UserId = 99;
            entity.Address = "Test";
            entity.City = "Test";
            entity.State = "WI";
            entity.Zip = "23456";
            entity.Phone = "2345678900";
            entity.ImagePath = ".\\Test.png";


            // Add the entity to the databasse
            dc.tblCustomers.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblCustomer - Use the first one.
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            //Change property values
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.UserId = 99;
            entity.Address = "Test";
            entity.City = "Test";
            entity.State = "WI";
            entity.Zip = "23456";
            entity.Phone = "2345678900";
            entity.ImagePath = ".\\Test.png";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblCustomer WHERE Id = 4
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblCustomers.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
