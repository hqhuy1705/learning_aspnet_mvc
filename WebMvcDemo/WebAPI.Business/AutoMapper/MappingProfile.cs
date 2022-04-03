using AutoMapper;
using WebAPI.Business.DTO;
using WebAPI.Entity;

namespace WebAPI.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<Category, CategoryDTO>().IgnoreAllNonExisting();
            CreateMap<CategoryDTO, Category>().IgnoreAllNonExisting();
            #endregion

            #region User
            CreateMap<User, UserDTO>().IgnoreAllNonExisting();
            CreateMap<UserDTO, User>().IgnoreAllNonExisting();
            #endregion
        }
    }
}
