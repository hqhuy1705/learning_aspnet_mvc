using System;

namespace WebAPI.Business.DTO
{
    public class Order_DetailsDTO
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int TotalPrice { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
