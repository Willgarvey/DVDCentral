using Microsoft.EntityFrameworkCore.Storage;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        public static int Insert(int movieId, int genreId, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre tblMovieGenre = new tblMovieGenre();
                    tblMovieGenre.MovieId = movieId;
                    tblMovieGenre.GenreId = genreId;
                    tblMovieGenre.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(sa => sa.Id) + 1 : 1;

                    dc.tblMovieGenres.Add(tblMovieGenre);
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

        public static int Delete(int movieId, int genreId, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre? tblMovieGenre = dc.tblMovieGenres
                                                            .FirstOrDefault(mg => mg.MovieId == movieId
                                                            && mg.GenreId == genreId);
                    if (tblMovieGenre != null)
                    {
                        dc.tblMovieGenres.Remove(tblMovieGenre);
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }                
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
