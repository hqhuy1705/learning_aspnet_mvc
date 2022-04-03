using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Business.DTO;
using WebAPI.Entity;
using WebAPI.Repository;

namespace WebAPI.Business
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryBusiness(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategories()
        {
            try
            {
                var categories = _categoryRepository.GetAll().ToList();

                return _mapper.Map<List<CategoryDTO>>(categories);
            }
            catch
            {
                return new List<CategoryDTO>();
            }
        }

        public CategoryDTO GetCategory(int id)
        {
            try
            {
                var category = _categoryRepository.GetById(id);

                return _mapper.Map<CategoryDTO>(category);
            }
            catch
            {
                return null;
            }
        }

        public bool CreateCategory(CategoryDTO category)
        {
            try
            {
                category.CreatedDate = DateTime.Now;
                category.UpdatedDate = DateTime.Now;
                var entity = _mapper.Map<Category>(category);
                _categoryRepository.Insert(entity);

                return _categoryRepository.Save();
            }
            catch
            {
                return false;
            }
        }

        public bool CreateCategories(List<CategoryDTO> categories)
        {
            try
            {
                var entities = _mapper.Map<List<Category>>(categories);
                _categoryRepository.Inserts(entities);

                return _categoryRepository.Save();
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCategory(CategoryDTO category)
        {
            try
            {
                category.UpdatedDate = DateTime.Now;
                var entity = _mapper.Map<Category>(category);
                _categoryRepository.Update(entity);

                return _categoryRepository.Save();
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(CategoryDTO category)
        {
            try
            {
                var entity = _mapper.Map<Category>(category);
                _categoryRepository.Delete(entity);

                return _categoryRepository.Save();
            }
            catch
            {
                return false;
            }
        }
    }
}
