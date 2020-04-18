namespace AutoParts.Web.Client.Shared.Models
{
    using Protos;

    public class CurrentUserInfoModel
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }
    }
}
