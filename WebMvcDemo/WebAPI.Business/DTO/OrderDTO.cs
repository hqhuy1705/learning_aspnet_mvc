using System;

namespace WebAPI.Business.DTO
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
