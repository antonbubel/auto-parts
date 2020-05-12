namespace AutoParts.Core.Contracts.Manufacturer.Notifications
{
    using MediatR;

    using System;

    public class UpdateManufacturerNotification : INotification
    {
        public long ManufacturerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long CountryId { get; set; }

        public string ImageFileName { get; set; }

        public ReadOnlyMemory<byte> ImageFileBuffer { get; set; }
    }
}
