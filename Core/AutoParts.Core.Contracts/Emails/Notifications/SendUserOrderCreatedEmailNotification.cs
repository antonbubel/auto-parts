﻿namespace AutoParts.Core.Contracts.Emails.Notifications
{
    using MediatR;

    public class SendUserOrderCreatedEmailNotification : INotification
    {
        public string ToEmail { get; set; }

        public string ToName { get; set; }
    }
}
