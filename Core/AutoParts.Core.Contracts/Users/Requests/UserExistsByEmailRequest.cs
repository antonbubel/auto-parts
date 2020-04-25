namespace AutoParts.Core.Contracts.Users.Requests
{
    using MediatR;

    public class UserExistsByEmailRequest : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
