using Practice_test1.Entities;
using Practice_test1.Database;
using Microsoft.EntityFrameworkCore;

namespace Practice_test1.Services
{
    public class UserService : IUserService
    {
        private readonly Mycontext Context;
        public UserService(Mycontext context)
        {
            Context = context;
        }
        public void CreateUser(User user)
        {
            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            User user = Context.Users.Find(userId);
            if (user != null)
            {
                Context.Users.Remove(user);
                Context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {

            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var result = Context.Users.ToList();
            return result;

        }

        public User GetUser(int userId)
        {
            return Context.Users.Find(userId);
        }

        public User ValidteUser(string userName, string password)
        {
            return Context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
        }
    }
}
