﻿namespace AutoParts.Core.Contracts.CarBrands.Notifications
{
    using MediatR;

    using System;

    public class UpdateCarBrandNotification : INotification
    {
        public long CarBrandId { get; set; }

        public string Name { get; set; }

        public string ImageFileName { get; set; }

        public ReadOnlyMemory<byte> ImageFileBuffer { get; set; }
    }
}
