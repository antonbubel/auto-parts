namespace AutoParts.Core.Contracts.Files.Requests
{
    using MediatR;

    using System;

    public class SaveFileRequest : IRequest<string>
    {
        public string FileName { get; set; }

        public ReadOnlyMemory<byte> Buffer { get; set; }
    }
}
