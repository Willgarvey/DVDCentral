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
    public class utGenre
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
            Assert.AreEqual(3, dc.tblGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblGenre entity = new tblGenre();
            entity.Description = "Hugo";
            entity.Id = -99;

            // Add the entity to the databasse
            dc.tblGenres.Add(entity);

            // Commit the changes without resaving the entire table
            int result = dc.SaveChanges();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            // Select * FROM tblGenre - Use the first one.
            tblGenre entity = dc.tblGenres.FirstOrDefault();

            //Change property values
            entity.Description = "Hugo";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblGenre WHERE Id = 4
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 2).FirstOrDefault();

            dc.tblGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadById()
        {
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 1).FirstOrDefault();
            Assert.AreEqual(entity.Id, 1);
        }
    }
}
