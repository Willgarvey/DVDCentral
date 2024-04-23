using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class FormatManager
    {
        public static int Insert(string description, ref int id, bool rollback = false)
        {
            try
            {
                Format format = new Format
                {
                    Description = description
                };

                int results = Insert(format, rollback);

                id = format.Id; // Backfill to reference Id

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Format format, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat entity = new tblFormat();
                    entity.Id = dc.tblFormats.Any() ? dc.tblFormats.Max(f => f.Id) + 1 : 1;
                    entity.Description = format.Description;

                    format.Id = entity.Id;  // Backfill to reference Id

                    dc.tblFormats.Add(entity);
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
        public static int Update(Format format, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == format.Id);

                    if (entity != null)
                    {
                        entity.Description = format.Description;
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

                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == Id);

                    if (entity != null)
                    {
                        dc.tblFormats.Remove(entity); // Remove the row from the table
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
        public static Format LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblFormat entity = dc.tblFormats.FirstOrDefault(f => f.Id == id);
                    if (entity != null)
                    {
                        return new Format
                        {
                            Id = entity.Id,
                            Description = entity.Description
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
        public static List<Format> Load()
        {
            try
            {
                List<Format> list = new List<Format>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from f in dc.tblFormats
                     select new
                     {
                         f.Id,
                         f.Description
                     })
                     .ToList()
                     .ForEach(format => list.Add(new Format
                     {
                         Id = format.Id,
                         Description = format.Description
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
