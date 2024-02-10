using AutoMapper;
using Practice_test1.Entities;
using Practice_test1.DTO;

namespace Practice_test1.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
        }
    }
}
