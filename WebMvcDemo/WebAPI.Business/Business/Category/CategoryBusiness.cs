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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryBusiness(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategories()
        {
            try
            {
                var categories = _unitOfWork.CategoryRepository.GetAll().ToList();

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
                var category = _unitOfWork.CategoryRepository.GetById(id);

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
                var entity = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepository.Insert(entity);

                return _unitOfWork.Save();
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
                _unitOfWork.CategoryRepository.Inserts(entities);

                return _unitOfWork.Save();
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
                _unitOfWork.CategoryRepository.Update(entity);

                return _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                _unitOfWork.CategoryRepository.Delete(id);

                return _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }
    }
}
