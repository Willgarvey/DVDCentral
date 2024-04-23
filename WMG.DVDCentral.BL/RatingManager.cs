using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class RatingManager
    {
        public static int Insert(string description,
                                 ref int id,
                                 bool rollback = false)
        {
            try
            {
                Rating rating = new Rating
                {
                    Description = description
                };

                int results = Insert(rating, rollback);

                id = rating.Id; // Backfill the Id as a reference

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entity = new tblRating();
                    entity.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(s => s.Id) + 1 : 1;
                    entity.Description = rating.Description;

                    rating.Id = entity.Id; // Backfill the Id as a reference

                    dc.tblRatings.Add(entity);
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

        public static int Update(Rating rating, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entity = dc.tblRatings.FirstOrDefault(s => s.Id == rating.Id);

                    if (entity != null)
                    {
                        entity.Description = rating.Description;
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

                    tblRating entity = dc.tblRatings.FirstOrDefault(s => s.Id == Id);

                    if (entity != null)
                    {
                        dc.tblRatings.Remove(entity); // Remove the row from the table
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
        public static Rating LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblRating entity = dc.tblRatings.FirstOrDefault(s => s.Id == id);
                    if (entity != null)
                    {
                        return new Rating
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
        public static List<Rating> Load()
        {
            try
            {
                List<Rating> list = new List<Rating>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from s in dc.tblRatings
                     select new
                     {
                         s.Id,
                         s.Description
                     })
                     .ToList()
                     .ForEach(rating => list.Add(new Rating
                     {
                         Id = rating.Id,
                         Description = rating.Description
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
