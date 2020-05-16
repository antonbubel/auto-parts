namespace AutoParts.Data.EF.ContextMappings
{
    using System.ComponentModel;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Model.Entities;
    using OrderStatusEnum = Model.Enums.OrderStatus;

    using Utilities.Common.Extensions;

    public class OrderStatusMap : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.Property(orderStatus => orderStatus.Id)
                .ValueGeneratedNever();

            builder.Property(orderStatus => orderStatus.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new OrderStatus {
                    Id = OrderStatusEnum.Pending,
                    Name = OrderStatusEnum.Pending.GetAttribute<OrderStatusEnum, DescriptionAttribute>().Description
                },
                new OrderStatus
                {
                    Id = OrderStatusEnum.Processing,
                    Name = OrderStatusEnum.Processing.GetAttribute<OrderStatusEnum, DescriptionAttribute>().Description
                },
                new OrderStatus
                {
                    Id = OrderStatusEnum.OnHold,
                    Name = OrderStatusEnum.OnHold.GetAttribute<OrderStatusEnum, DescriptionAttribute>().Description
                },
                new OrderStatus
                {
                    Id = OrderStatusEnum.Canceled,
                    Name = OrderStatusEnum.Canceled.GetAttribute<OrderStatusEnum, DescriptionAttribute>().Description
                },
                new OrderStatus
                {
                    Id = OrderStatusEnum.Completed,
                    Name = OrderStatusEnum.Completed.GetAttribute<OrderStatusEnum, DescriptionAttribute>().Description
                }
            );
        }
    }
}
