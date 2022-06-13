using System.Collections.Generic;
using WebAPI.Business.DTO;

namespace WebAPI.Business
{
    public interface ICategoryBusiness
    {
        List<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        bool CreateCategory(CategoryDTO category);
        bool CreateCategories(List<CategoryDTO> categories);
        bool UpdateCategory(CategoryDTO category);
        bool DeleteCategory(int id);
    }
}
