using Microsoft.EntityFrameworkCore.Storage;
using System.Xml.Linq;
using System.Xml;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;
using System.ComponentModel;

namespace WMG.DVDCentral.BL
{
    public static class OrderItemManager
    {
        public static int Insert(int OrderId,
                                    int Quantity,
                                    int MovieId,
                                    float Cost,
                                    ref int id,
                                    bool rollback = false)
        {
            try
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = OrderId,
                    Quantity = Quantity,
                    MovieId = MovieId,
                    Cost = Cost
                };

                int results = Insert(orderItem, rollback);

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = orderItem.Id;

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = new tblOrderItem();
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(s => s.Id) + 1 : 1;
                    entity.OrderId = orderItem.OrderId;
                    entity.Quantity = 1;
                    entity.MovieId = orderItem.MovieId;
                    entity.Cost = orderItem.Cost;
      
                    orderItem.Id = entity.Id;   // IMPORTANT - BACK FILL THE ID TO MAKE THE VALUE AVAILABLE ON THE UI


                    dc.tblOrderItems.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == orderItem.Id);

                    if (entity != null)
                    {
                        entity.OrderId = orderItem.OrderId;
                        entity.Quantity = orderItem.Quantity;
                        entity.MovieId = orderItem.MovieId;
                        entity.Cost = orderItem.Cost;
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

                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == Id);

                    if (entity != null)
                    {
                        dc.tblOrderItems.Remove(entity); // Remove the row from the table
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
        public static OrderItem LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(oi => oi.Id == id);
                    if (entity != null)
                    {
                        return new OrderItem
                        {
                            Id = entity.Id,
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
        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from oi in dc.tblOrderItems
                     select new
                     {
                         oi.Id,
                         oi.OrderId,
                         oi.Quantity,
                         oi.MovieId,
                         oi.Cost
                     })
                     .ToList()
                     .ForEach(orderItem => list.Add(new OrderItem
                     {
                         Id = orderItem.Id,
                         Quantity = orderItem.Quantity,
                         MovieId = orderItem.MovieId,
                         OrderId = orderItem.OrderId,
                         Cost = (float)orderItem.Cost
                     }));;


                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<OrderItem> LoadByOrderId(int orderId)
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from oi in dc.tblOrderItems
                     where oi.OrderId == orderId
                     select new
                     {
                         oi.Id,
                         oi.OrderId,
                         oi.Quantity,
                         oi.MovieId,
                         oi.Cost
                     })
                     .ToList()
                     .ForEach(orderItem => list.Add(new OrderItem
                     {
                         Id = orderItem.Id,
                         OrderId = orderItem.OrderId,
                         Quantity = orderItem.Quantity,
                         MovieId = orderItem.MovieId,
                         Cost = (float)orderItem.Cost
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
