using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMG.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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
            Assert.AreEqual(5, dc.tblMovies.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblMovie entity = new tblMovie();
            entity.FirstName = "Hugo";
            entity.LastName = "Weaving";
            entity.MovieId = "345556777";
            entity.Id = -99;

            // Add the entity to the databasse
            dc.tblMovies.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblMovie - Use the first one.
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            //Change property values
            entity.FirstName = "Hugo";
            entity.LastName = "Weaving";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblMovie WHERE Id = 4
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 4).FirstOrDefault();

            dc.tblMovies.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblDeclaration entity = dc.tblDeclarations.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
