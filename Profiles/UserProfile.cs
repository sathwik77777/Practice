using AutoMapper;
using Practice_test1.DTO;
using Practice_test1.Entities;


namespace Practice_test1.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }

    }
}
