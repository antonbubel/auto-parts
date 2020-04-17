namespace AutoParts.Core.Contracts.CarModels.Notifications
{
    using MediatR;

    using System;

    public class UpdateCarModelNotification : INotification
    {
        public long CarModelId { get; set; }

        public string Name { get; set; }

        public string ImageFileName { get; set; }

        public ReadOnlyMemory<byte> ImageFileBuffer { get; set; }
    }
}
