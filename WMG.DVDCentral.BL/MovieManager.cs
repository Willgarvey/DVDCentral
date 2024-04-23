using Microsoft.EntityFrameworkCore.Storage;
using System.Xml.Linq;
using System.Xml;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;
using System.ComponentModel;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace WMG.DVDCentral.BL
{
    public static class MovieManager
    {
        public static int Insert(string title,
                                string description,
                                int formatId,
                                int directorId,
                                int ratingId,
                                float cost,
                                int inStkQty,
                                string imagePath,
                                ref int id,
                                bool rollback = false)
        {
            try
            {
                Movie movie = new Movie
                {
                    Title = title,
                    Description = description,
                    FormatId = formatId,
                    DirectorId = directorId,
                    RatingId = ratingId,
                    Cost = cost,
                    InStkQty = inStkQty,
                    ImagePath = imagePath
                };

                int results = Insert(movie, rollback);

                id = movie.Id;  // IMPORTANT - BACKFILL THE REFERENCE ID

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = new tblMovie();
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(m => m.Id) + 1 : 1;
                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.RatingId = movie.RatingId;
                    entity.Cost = movie.Cost;
                    entity.InStkQty = movie.InStkQty;
                    entity.ImagePath = movie.ImagePath;

                    movie.Id = entity.Id;   // IMPORTANT - BACK FILL THE ID


                    dc.tblMovies.Add(entity);
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
        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.Title = movie.Title;
                        entity.Description = movie.Description;
                        entity.FormatId = movie.FormatId;
                        entity.DirectorId = movie.DirectorId;
                        entity.RatingId = movie.RatingId;
                        entity.Cost = Math.Round(movie.Cost, 2);
                        entity.InStkQty = movie.InStkQty;
                        entity.ImagePath = movie.ImagePath;

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

                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == Id);

                    if (entity != null)
                    {
                        dc.tblMovies.Remove(entity);
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
        public static Movie LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);
                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            FormatId = entity.FormatId,
                            RatingId = entity.RatingId,
                            DirectorId = entity.DirectorId,
                            Cost = (float)entity.Cost,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath,
                            Genres = GenreManager.Load(id)
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
        public static List<Movie> Load(int? genreId = null)
        {
            try
            {
                List<Movie> list = new List<Movie>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                { // join tables for movie, movieGenres, genres, rating, format, and director
                    (from m in dc.tblMovies
                     join mg in dc.tblMovieGenres on m.Id equals mg.MovieId
                     join g in dc.tblGenres on mg.GenreId equals g.Id
                     join r in dc.tblRatings on m.RatingId equals r.Id
                     join f in dc.tblFormats on m.FormatId equals f.Id
                     join d in dc.tblDirectors on m.DirectorId equals d.Id
                     where g.Id == genreId || genreId == null
                     select new
                     {
                         m.Id,
                         m.Title,
                         m.Description,
                         m.FormatId,
                         FormatName = f.Description,
                         m.DirectorId,
                         DirectorName = d.FirstName + " " + d.LastName,
                         m.RatingId,
                         RatingName = r.Description,
                         m.Cost,
                         m.InStkQty,
                         m.ImagePath,
                         // GenreName = g.Description,
                         
                     })
                     .Distinct().ToList()
                     .ForEach(movie => list.Add(new Movie
                     {
                         Id = movie.Id,
                         Title = movie.Title,
                         Description = movie.Description,
                         FormatId = movie.FormatId,
                         FormatName = movie.FormatName,
                         DirectorId = movie.DirectorId,
                         DirectorName = movie.DirectorName,
                         RatingId = movie.RatingId,
                         RatingName = movie.RatingName,
                         Cost = (float)movie.Cost,
                         InStkQty = movie.InStkQty,
                         ImagePath = movie.ImagePath


                     }));
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Load a list of movies given a list of ids
        public static List<Movie> LoadByIds(List<int>movieIds)
        {
            List<Movie> movies = new List<Movie>();

            foreach (int movieId in movieIds)
            {
                Movie movie = LoadById(movieId);
                if (movie != null)
                {
                    movies.Add(movie);
                }
            }

            return movies;
        }
    }
}
