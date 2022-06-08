using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Business;
using WebAPI.Business.DTO;

namespace WebAPI.Controllers
{
    [RoutePrefix("category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [HttpGet]
        [Route("getall")]
        public List<CategoryDTO> GetAll()
        {
            return _categoryBusiness.GetCategories();
        }

        [HttpGet]
        [Route("get")]
        public CategoryDTO Get(int id)
        {
            return _categoryBusiness.GetCategory(id);
        }

        [HttpPost]
        [Route("create")]
        public bool Create(CategoryDTO categoryDTO)
        {
            return _categoryBusiness.CreateCategory(categoryDTO);
        }

        [HttpPost]
        [Route("create/multiple")]
        public bool CreateMultiple(List<CategoryDTO> categoryDTO)
        {
            return _categoryBusiness.CreateCategories(categoryDTO);
        }

        [HttpPut]
        [Route("update")]
        public bool Update(CategoryDTO categoryDTO)
        {
            return _categoryBusiness.UpdateCategory(categoryDTO);
        }
    }
}
