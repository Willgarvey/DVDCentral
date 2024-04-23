using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = OrderManager.Insert(1, DateTime.Now, DateTime.Now, 1, ref id, true); // True enables allow rollback
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
           
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now,
                UserId = 99


            };

            int results = OrderManager.Insert(order, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(1);
            order.CustomerId = 1;
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.Now;
            order.UserId = 1;

            int results = OrderManager.Update(order, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertOrderItemsTest()
        {
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = DateTime.Now,
                UserId = 99,
                ShipDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 88,
                        MovieId = 1,
                        Cost = 9.99F,
                        Quantity = 9
                    },
                    new OrderItem
                    {
                        Id = 99,
                        MovieId = 2,
                        Cost = 8.88F,
                        Quantity = 2
                    }
                }
            };
            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(order.OrderItems[1].OrderId, order.Id);
            Assert.AreEqual(3, result);
        }
        [TestMethod]
        public void LoadByIdTest()
        {
            int id = OrderManager.Load().LastOrDefault().Id;
            Order order = OrderManager.LoadById(id);
            Assert.AreEqual(id, order.Id);
            Assert.IsTrue(order.OrderItems.Count > 0);
        }
        [TestMethod]
        public void LoadByIdCustomerIdTest()
        {
            int customerId = OrderManager.Load().FirstOrDefault().CustomerId;
            Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
        }
        [TestMethod]
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            // Assert.IsTrue(OrderItemManager.LoadByOrderId(orderId).Count > 0);
        }
    }
}

