using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;
using System.Text;
using WMG.DVDCentral.BL.Models;
using WMG.DVDCentral.PL;

namespace WMG.DVDCentral.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot login in with these credentials. Your IP address has been logged.")
        {

        }

        public LoginFailureException(string message) : base(message)
        {

        }
    }

    public static class UserManager
    {
        public static string GetHash(string password)
        {
            using (var hasher = SHA1.Create())
            {   // Use maple as Brian's password
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }

        public static int DeleteAll()
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    dc.tblUsers.RemoveRange(dc.tblUsers.ToList());
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblUser entity = new tblUser();

                    entity.Id = dc.tblUsers.Any() ? dc.tblUsers.Max(s => s.Id) + 1 : 1;
                    entity.FirstName = user.FirstName;
                    entity.LastName = user.LastName;
                    entity.UserName = user.UserName;
                    entity.Password = GetHash(user.Password);


                    // IMPORTANT - BACK FILL THE ID TO MAKE THE VALUE AVAILABLE ON THE UI
                    user.Id = entity.Id;

                    dc.tblUsers.Add(entity);
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

        public static int Update(User user, bool rollback = false)
        {
            try
            {
                int result = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    //Get the row that we are trying to update
                    tblUser entity = dc.tblUsers.FirstOrDefault(user => user.Id == user.Id);

                    if (entity != null)
                    {
                        entity.FirstName = user.FirstName;
                        entity.LastName = user.LastName;
                        entity.UserName = user.UserName;
                        entity.Password = GetHash(user.Password);
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

        public static bool Login(User user)
        {   // Include custom error handling before connection
            try
            {
                if (!string.IsNullOrEmpty(user.UserName))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (DVDCentralEntities dc = new DVDCentralEntities())
                        {
                            tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.UserName == user.UserName);
                            if (tblUser != null)
                            {
                                if (tblUser.Password == GetHash(user.Password))
                                {
                                    // Login successful
                                    user.Id = tblUser.Id;
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                }
                            }
                            else
                            {
                                throw new LoginFailureException("UserId was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new LoginFailureException("Password was not set.");
                    }
                }
                else
                {
                    throw new LoginFailureException("UserId was not set.");
                }
            }
            catch (LoginFailureException)
            {
                throw;
            }
            catch (Exception)
            {

                throw; // Automatically returns 0
            }
        }

        public static void Seed()
        {
            using (DVDCentralEntities dc = new DVDCentralEntities())
            {
                if (!dc.tblUsers.Any())
                {
                    User user = new User
                    {
                        UserName = "wgarvey",
                        FirstName = "Will",
                        LastName = "Garvey",
                        Password = "missing"
                    };
                    Insert(user);

                    user = new User // Brian's login
                    {
                        UserName = "bfoote",
                        FirstName = "Brian",
                        LastName = "Foote",
                        Password = "maple"
                    };
                    Insert(user);
                }
            }
        }

        public static List<User> Load()
        {
            try
            {
                List<User> list = new List<User>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from u in dc.tblUsers
                     select new
                     {
                         u.Id,
                         u.UserName,
                         u.FirstName,
                         u.LastName,
                         u.Password
                     })
                     .ToList()
                     .ForEach(user => list.Add(new User
                     {
                         Id = user.Id,
                         UserName = user.UserName,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         Password = user.Password,

                     }));


                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static User LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblUser entity = dc.tblUsers.FirstOrDefault(d => d.Id == id);
                    if (entity != null)
                    {
                        return new User
                        {
                            Id = entity.Id,
                            UserName = entity.UserName,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Password = entity.Password
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
    }
}
