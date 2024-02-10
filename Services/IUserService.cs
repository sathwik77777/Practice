using Practice_test1.Entities;

namespace Practice_test1.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        List<User> GetAllUsers(); 

        User GetUser(int userId);
        void EditUser(User user);
        void DeleteUser(int userId); 
        User ValidteUser(string userName, string password);
    }
}
