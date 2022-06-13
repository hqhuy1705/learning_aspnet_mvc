using System;
using System.Collections.Generic;
using WebAPI.Entity;

namespace WebAPI.Test.TestHelper
{
    public class DataInitializer
    {
        public static List<Category> GetAllCategories()
        {
            var categories = new List<Category>
                            {
                                new Category()
                                {
                                    DisplayName = "Laptop",
                                    CreatedDate = DateTime.Now,
                                },
                                new Category() 
                                {
                                    DisplayName = "Mobile",
                                    CreatedDate = DateTime.Now,
                                },
                                new Category() 
                                {
                                    DisplayName = "HardDrive",
                                    CreatedDate = DateTime.Now,
                                },
                                new Category() 
                                {
                                    DisplayName = "IPhone",
                                    CreatedDate = DateTime.Now,
                                },
                                new Category() 
                                {
                                    DisplayName = "IPad",
                                    CreatedDate = DateTime.Now,
                                }
                            };
            return categories;
        }
    }
}
