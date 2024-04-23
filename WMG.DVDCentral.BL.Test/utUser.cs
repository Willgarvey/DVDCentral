using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoginSuccessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User { UserName = "bfoote", Password = "maple" }));
            Assert.IsTrue(UserManager.Login(new User { UserName = "wgarvey", Password = "misspiggy" }));
        }

        [TestMethod]
        public void Seed()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User { FirstName = "Test", LastName = "Test", UserName = "Test", Password = "Test123!" };
            int results = UserManager.Insert(user, true);
            Assert.AreEqual(1, results);

        }

        [TestMethod]
        public void LoadTest()
        {

        }



        [TestMethod]
        public void LoginFailureNoUserId()
        {
            try
            {
                Seed();
                Assert.IsTrue(!UserManager.Login(new User { UserName = "", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(!UserManager.Login(new User { UserName = "bfoote", Password = "birch" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureBadUserName()
        {
            try
            {
                Seed();
                Assert.IsTrue(!UserManager.Login(new User { UserName = "bfote", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsTrue(!UserManager.Login(new User { UserName = "bfoote", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
