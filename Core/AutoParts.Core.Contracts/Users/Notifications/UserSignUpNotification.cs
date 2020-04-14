namespace AutoParts.Core.Contracts.Users.Notifications
{
    using MediatR;

    public class UserSignUpNotification : INotification
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}
