using AutoMapper;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using WebAPI.Business;
using WebAPI.Business.DTO;
using WebAPI.Controllers;
using WebAPI.Entity;
using WebAPI.Repository;
using WebAPI.Test.TestHelper;

namespace WebAPI.Test.APIControllerTest
{
    [TestFixture]
    public class CategoryTest
    {
        #region Variables
        private IDbFactory _dbFactory;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICategoryBusiness _categoryBusiness;
        private List<Category> _categories;
        private GenericRepository<Category> _categoryRepository;
        private HttpClient _client;
        private HttpResponseMessage _response;
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _categories = SetUpCategories();

            _dbFactory = new DbFactory();
            _categoryRepository = SetUpCategoryRepository();

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.CategoryRepository).Returns(_categoryRepository);
            _unitOfWork = unitOfWork.Object;

            _mapper = AutoMapperConfig.GetMapperConfig();

            _categoryBusiness = new CategoryBusiness(_unitOfWork, _mapper);

            _client = new HttpClient { BaseAddress = new Uri(Constants.ServiceBaseURL) };
            _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
        }

        #endregion

        #region Setup

        /// <summary>
        /// Setup dummy repository
        /// </summary>
        /// <returns></returns>
        private GenericRepository<Category> SetUpCategoryRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<Category>>(MockBehavior.Default, _dbFactory);

            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_categories.AsQueryable());

            mockRepo.Setup(p => p.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Category>(
                             id => _categories.Find(p => p.Id.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Category>())))
                .Callback(new Action<Category>(newCategory =>
                {
                    dynamic maxCategoryId = _categories.Last().Id;
                    dynamic nextCategoryId = maxCategoryId + 1;
                    newCategory.Id = nextCategoryId;
                    _categories.Add(newCategory);
                }));

            mockRepo.Setup(p => p.Update(It.IsAny<Category>()))
                .Callback(new Action<Category>(category =>
                {
                    var oldCategory = _categories.Find(a => a.Id == category.Id);
                    oldCategory = category;
                }));

            mockRepo.Setup(p => p.Delete(It.IsAny<Category>()))
                .Callback(new Action<Category>(category =>
                {
                    var categoryRemove =
                        _categories.Find(a => a.Id == category.Id);

                    if (categoryRemove != null)
                        _categories.Remove(categoryRemove);
                }));

            mockRepo.Setup(p => p.Delete(It.IsAny<object>()))
                .Callback(new Action<object>(categoryId =>
                {
                    var categoryRemove =
                        _categories.Find(a => a.Id == (int)categoryId);

                    if (categoryRemove != null)
                        _categories.Remove(categoryRemove);
                }));

            // Return mock implementation object
            return mockRepo.Object;
        }

        /// <summary>
        /// Set up categories
        /// </summary>
        /// <returns></returns>
        private static List<Category> SetUpCategories()
        {
            var categoryId = new int();
            var categories = DataInitializer.GetAllCategories();
            foreach (Category category in categories)
                category.Id = ++categoryId;
            return categories;
        }

        #endregion

        #region Unit Tests
        
        /// <summary>
        /// Get all categories test
        /// </summary>
        [Test]
        public void GetAllCategoryTest()
        {
            var categoryController = new CategoryController(_categoryBusiness)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{Constants.ServiceBaseURL}{Constants.CategoryPrefix}/getall")
                }
            };

            categoryController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            _response = categoryController.GetAll();

            var responseResult = JsonConvert.DeserializeObject<List<Category>>(_response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(responseResult.Any(), true);

            var comparer = new CategoryComparer();
            CollectionAssert.AreEqual(
                responseResult.OrderBy(category => category, comparer),
                _categories.OrderBy(category => category, comparer), comparer);
        }

        /// <summary>
        /// Get category by Id test
        /// </summary>
        [TestCase(1,"Laptop")]
        [TestCase(2,"Mobile")]
        [TestCase(3, "HardDrive")]
        public void GetCategoryByIdTest(int id, string name)
        {
            var categoryController = new CategoryController(_categoryBusiness)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{Constants.ServiceBaseURL}{Constants.CategoryPrefix}/get?id={id}")
                }
            };

            categoryController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            _response = categoryController.Get(id);

            var responseResult = JsonConvert.DeserializeObject<Category>(_response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
            AssertObjects.PropertyValuesAreEquals(responseResult,
                            _categories.Find(a => a.DisplayName.Contains(name)));
        }

        /// <summary>
        /// Create category test
        /// </summary>
        [Test]
        public void CreateCategoryTest()
        {
            var categoryController = new CategoryController(_categoryBusiness)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{Constants.ServiceBaseURL}{Constants.CategoryPrefix}/create")
                }
            };

            categoryController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var createDate = DateTime.Now;
            var updatedDate = DateTime.Now;
            var newCategory = new CategoryDTO
            {
                DisplayName = "Android Phone",
                CreatedDate = createDate,
                UpdatedDate = updatedDate
            };

            var maxCategoryIdBeforeAdd = _categories.Max(a => a.Id);
            newCategory.Id = maxCategoryIdBeforeAdd + 1;
            categoryController.Create(newCategory);

            var addedCategory = new Category
            { 
                Id = newCategory.Id, 
                DisplayName = newCategory.DisplayName,
                CreatedDate = createDate,
                UpdatedDate = updatedDate
            };

            AssertObjects.PropertyValuesAreEquals(addedCategory, _categories.Last());
            Assert.That(maxCategoryIdBeforeAdd + 1, Is.EqualTo(_categories.Last().Id));
        }

        /// <summary>
        /// Update category test
        /// </summary>
        [Test]
        public void UpdateCategoryTest()
        {
            var categoryController = new CategoryController(_categoryBusiness)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{Constants.ServiceBaseURL}{Constants.CategoryPrefix}/update")
                }
            };

            categoryController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var firstCategory = _categories.First();
            firstCategory.DisplayName = "Laptop updated";
            var updatedCategory = new CategoryDTO() { DisplayName = firstCategory.DisplayName, Id = firstCategory.Id };
            categoryController.Update(updatedCategory);

            Assert.That(firstCategory.Id, Is.EqualTo(1)); // hasn't changed
            Assert.That(firstCategory.DisplayName, Is.EqualTo("Laptop updated")); // hasn't changed
        }

        /// <summary>
        /// Delete category by Id test
        /// </summary>
        [Test]
        public void DeleteCategoryTest()
        {
            var categoryController = new CategoryController(_categoryBusiness)
            {
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{Constants.ServiceBaseURL}{Constants.CategoryPrefix}/delete?id=5")
                }
            };

            categoryController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            int maxID = _categories.Max(a => a.Id); // Before removal
            var lastCategory = _categories.Last();

            // Remove last category
            categoryController.Delete(lastCategory.Id);
            Assert.That(maxID, Is.GreaterThan(_categories.Max(a => a.Id))); // Max id reduced by 1
        }
        
        #endregion

        #region Tear Down

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TearDown]
        public void DisposeTest()
        {
            _categories = null;
            _dbFactory = null;
            _categoryRepository = null;
            _unitOfWork = null;
            _mapper = null;
            _categoryBusiness = null;
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }

        #endregion
    }
}
