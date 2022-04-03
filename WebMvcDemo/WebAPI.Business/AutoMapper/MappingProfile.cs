using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Business.DTO;
using WebAPI.Entity;

namespace WebAPI.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            #endregion

            #region User
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            #endregion
        }
    }
}
