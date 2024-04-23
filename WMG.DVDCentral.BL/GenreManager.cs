using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class GenreManager
    {
        public static int Insert(string description, ref int id, bool rollback = false)
        {
            try
            {
                Genre genre = new Genre
                {
                    Description = description
                };

                int results = Insert(genre, rollback);

                id = genre.Id;    // IMPORTANT - BACKFILL THE REFERENCE ID

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entity = new tblGenre();
                    entity.Id = dc.tblGenres.Any() ? dc.tblGenres.Max(g => g.Id) + 1 : 1;
                    entity.Description = genre.Description;


                    genre.Id = entity.Id; // Backfill the ID so it is available in the UI

                    dc.tblGenres.Add(entity);
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
        public static int Update(Genre genre, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entity = dc.tblGenres.FirstOrDefault(g => g.Id == genre.Id);

                    if (entity != null)
                    {
                        entity.Description = genre.Description;
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

                    tblGenre entity = dc.tblGenres.FirstOrDefault(g => g.Id == Id);

                    if (entity != null)
                    {
                        dc.tblGenres.Remove(entity);
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
        public static Genre LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblGenre entity = dc.tblGenres.FirstOrDefault(g => g.Id == id);
                    if (entity != null)
                    {
                        return new Genre
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
        public static List<Genre> Load()
        {
            try
            {
                List<Genre> list = new List<Genre>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from g in dc.tblGenres
                     select new
                     {
                         g.Id,
                         g.Description
                     })
                     .ToList()
                     .ForEach(genre => list.Add(new Genre
                     {
                         Id = genre.Id,
                         Description = genre.Description
                     }));


                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Genre> Load(int movieId)
        {
            try
            {
                List<Genre> list = new List<Genre>();

                using (ProgDecEntities dc = new ProgDecEntities())
                {
                    (from mg in dc.tblMovieGenres
                     join g in dc.tblGenres on mg.GenreId equals g.Id
                     where mg.MovieId == movieId
                     select new
                     {
                         mg.Id,
                         mg.GenreId,
                         g.Description,
                         mg.MovieId,
                     })
                     .ToList()
                     .ForEach(genre => list.Add(new Genre
                     {
                         Id = genre.GenreId,
                         Description = genre.Description
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
