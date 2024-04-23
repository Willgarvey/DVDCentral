using System.Net;
using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = CustomerManager.Insert("Test", "Test", 99, "Test", "Test", "WI", "23455", "1234567890", "./path", ref id, true); // True enables allow rollback
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Customer customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Test",
                City = "Test",
                State = "WI",
                Zip = "23455",
                Phone = "1234567890",
                ImagePath = ".\\path"
            };

            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(2);
            customer.FirstName = "Test";
            customer.LastName = "Test";;
            customer.Address = "Test";
            customer.City = "Test";
            customer.State = "WI";
            customer.Zip = "23455";
            customer.Phone = "1234567890";
            customer.ImagePath = ".\\path";

            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}

