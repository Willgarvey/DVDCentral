using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
    {
        protected DVDCentralEntities dc; //modular or class level scope
        protected IDbContextTransaction transaction; //modular or class level scope


        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }


        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblMovieGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblMovieGenre entity = new tblMovieGenre();
            entity.MovieId = 99;
            entity.GenreId = 99;


            // Add the entity to the databasse
            dc.tblMovieGenres.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblMovieGenre - Use the first one.
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            //Change property values
            entity.MovieId = 99;
            entity.GenreId = 99; ;

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblMovieGenre WHERE Id = 4
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblMovieGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
