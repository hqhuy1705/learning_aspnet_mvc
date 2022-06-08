using System;

namespace WebAPI.Business.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CategoryId { get; set; }
    }
}
