namespace AutoParts.Data.Model.Entities
{
    using System.Collections.Generic;

    using Base;

    using OrderStatusEnum = Enums.OrderStatus;

    public class OrderStatus : LookupEntity<OrderStatusEnum>
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}
