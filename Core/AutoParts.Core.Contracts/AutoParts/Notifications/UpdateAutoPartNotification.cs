namespace AutoParts.Core.Contracts.AutoParts.Notifications
{
    using MediatR;

    using System;

    public class UpdateAutoPartNotification : INotification
    {
        public long AutoPartId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageFileName { get; set; }

        public ReadOnlyMemory<byte> ImageFileBuffer { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsAvailable { get; set; }

        public long SupplierId { get; set; }
    }
}
