using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetAll()
        {
            var categories = _categoryBusiness.GetCategories();

            if (categories.Any())
                return Request.CreateResponse(HttpStatusCode.OK, categories);

            throw new HttpResponseException(new HttpResponseMessage
            {
                ReasonPhrase = "404 - Not found",
                StatusCode = HttpStatusCode.NotFound
            });
        }

        [HttpGet]
        [Route("get")]
        public HttpResponseMessage Get(int id)
        {
            var category = _categoryBusiness.GetCategory(id);

            if (category != null)
                return Request.CreateResponse(HttpStatusCode.OK, category);

            throw new HttpResponseException(new HttpResponseMessage
            {
                ReasonPhrase = "404 - Not found",
                StatusCode = HttpStatusCode.NotFound
            });
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

        [HttpDelete]
        [Route("delete")]
        public bool Delete(int id)
        {
            return _categoryBusiness.DeleteCategory(id);
        }
    }
}
