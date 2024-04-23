using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class OrderManager
    {
        public static int Insert(int customerId,
                                    DateTime orderDate,
                                    DateTime shipDate,
                                    int userId,
                                    ref int id,
                                    bool rollback = false)
        {
            try
            {
                Order order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = orderDate,
                    ShipDate = shipDate,
                    UserId = userId
                };

                int results = Insert(order, rollback);

                id = order.Id;  // IMPORTANT - BACKFILL THE REFERENCE ID

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = new tblOrder();
                    entity.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(o => o.Id) + 1 : 1;
                    entity.CustomerId = order.CustomerId;
                    entity.OrderDate = order.OrderDate;
                    entity.ShipDate = order.ShipDate;
                    entity.UserId = order.UserId;

                    order.Id = entity.Id;  // Backfill the Id as a reference

                    dc.tblOrders.Add(entity);

                    foreach (OrderItem orderItem in order.OrderItems) // 3d
                    {
                        orderItem.OrderId = order.Id; // Add the OrderId to each Order Item for storage in the database
                        
                        results += OrderItemManager.Insert(orderItem, rollback);

                    }

                    results += dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = dc.tblOrders.FirstOrDefault(o => o.Id == order.Id);

                    if (entity != null)
                    {
                        entity.Id = order.Id;
                        entity.CustomerId = order.CustomerId;
                        entity.OrderDate = order.OrderDate;
                        entity.ShipDate = order.ShipDate;
                        entity.UserId = order.UserId;
                        result = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }

                    if (rollback) transaction.Rollback();
                }
                return result;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public static int Delete(int Id, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = dc.tblOrders.FirstOrDefault(o => o.Id == Id);

                    if (entity != null)
                    {
                        dc.tblOrders.Remove(entity); // Remove the rom from the table
                        result = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }

                    if (rollback) transaction.Rollback();
                }
                return result;
            }

            catch (Exception)
            {
                throw;
            }
        }
        public static Order LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    var entity = dc.tblOrders.FirstOrDefault(o => o.Id == id);
                    if (entity != null)
                    {
                        return new Order
                        {
                            Id = entity.Id,
                            OrderDate = entity.OrderDate,
                            ShipDate = entity.ShipDate,
                            CustomerId = entity.CustomerId,
                            OrderItems = OrderItemManager.LoadByOrderId(id),
                            UserId = entity.UserId,
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<Order> Load(int? CustomerId = null )
        {
            try
            {
                List<Order> list = new List<Order>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from o in dc.tblOrders
                     where CustomerId == o.Id || CustomerId == null
                     select new
                     {
                         o.Id,
                         o.CustomerId,
                         o.OrderDate,
                         o.ShipDate,
                         o.UserId,
                         
                     })
                     .ToList()
                     .ForEach(order => list.Add(new Order
                     {
                         Id = order.Id,
                         CustomerId = order.CustomerId,
                         OrderDate = order.OrderDate,
                         ShipDate = order.ShipDate,
                         UserId = order.UserId,
                         OrderItems = OrderItemManager.LoadByOrderId(order.Id),
                     }));


                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
