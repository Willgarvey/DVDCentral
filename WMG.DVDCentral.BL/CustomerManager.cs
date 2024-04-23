using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(string firstname,
                                    string lastname,
                                    int userId,
                                    string address,
                                    string city,
                                    string state,
                                    string zip,
                                    string phone,
                                    string imagePath,
                                    ref int id,
                                    bool rollback = false)
        {
            try
            {
                Customer customer = new Customer
                {
                    FirstName = firstname,
                    LastName = lastname,
                    UserId = id,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip,
                    Phone = phone,
                    ImagePath = "",
                };

                int results = Insert(customer, rollback);

                // IMPORTANT - BACKFILL THE REFERENCE ID
                id = customer.Id;

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer entity = new tblCustomer();
                    entity.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(c => c.Id) + 1 : 1;
                    entity.FirstName = customer.FirstName;
                    entity.LastName = customer.LastName;
                    entity.Address = customer.Address;
                    entity.City = customer.City;
                    entity.State = customer.State;
                    entity.Zip = customer.Zip;
                    entity.Phone = customer.Phone;
                    entity.ImagePath = "";
                    entity.UserId = entity.Id;

                    // IMPORTANT - BACK FILL THE ID TO MAKE THE VALUE AVAILABLE ON THE UI
                    customer.Id = entity.Id;

                    dc.tblCustomers.Add(entity);
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

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    //Get the row that we are trying to update
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(c => c.Id == customer.Id);

                    if (entity != null)
                    {
                        entity.FirstName = customer.FirstName;
                        entity.LastName = customer.LastName;
                        entity.UserId = customer.UserId;
                        entity.Address = customer.Address;
                        entity.City = customer.City;
                        entity.State = customer.State;
                        entity.Zip = customer.Zip;
                        entity.Phone = customer.Phone;
                        entity.ImagePath = "";
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

                    //Get the row that we are trying to update
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(c => c.Id == Id);

                    if (entity != null)
                    {
                        dc.tblCustomers.Remove(entity); // Remove the row fro mthe table
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
        public static Customer LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    var entity = dc.tblCustomers.FirstOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        return new Customer
                        {
                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            UserId = entity.UserId,
                            Address = entity.Address,
                            City = entity.City,
                            State = entity.State,
                            Zip = entity.Zip,
                            Phone = entity.Phone,
                            ImagePath = entity.ImagePath,

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
        public static List<Customer> Load()
        {
            try
            {
                List<Customer> list = new List<Customer>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from c in dc.tblCustomers
                     select new
                     {
                         c.Id,
                         c.FirstName,
                         c.LastName,
                         c.UserId,
                         c.Address,
                         c.City,
                         c.State,
                         c.Zip,
                         c.Phone,
                         c.ImagePath
                     })
                     .ToList()
                     .ForEach(customer => list.Add(new Customer
                     {
                         Id = customer.Id,
                         FirstName = customer.FirstName,
                         LastName = customer.LastName,
                         Address = customer.Address,
                         UserId = customer.UserId,
                         City = customer.City,
                         State = customer.State,
                         Zip = customer.Zip,
                         Phone = customer.Phone,
                         ImagePath = customer.ImagePath
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
