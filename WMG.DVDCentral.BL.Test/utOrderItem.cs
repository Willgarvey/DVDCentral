using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = OrderItemManager.Insert(1,1,1,1, ref id, true); // True enables allow rollback
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            OrderItem orderItem = new OrderItem
            {
                Quantity = 1,
                OrderId = 1,
                MovieId = 1,
                Cost = 1


            };

            int results = OrderItemManager.Insert(orderItem, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadById(1);
            orderItem.Quantity = 1;
            orderItem.OrderId = 1;
            orderItem.MovieId = 1;
            orderItem.Cost = 1;

            int results = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderItemManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }
}

