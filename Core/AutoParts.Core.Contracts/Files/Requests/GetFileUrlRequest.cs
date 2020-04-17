namespace AutoParts.Core.Contracts.Files.Requests
{
    using MediatR;

    public class GetFileUrlRequest : IRequest<string>
    {
        public string FileName { get; set; }
    }
}
