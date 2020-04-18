namespace AutoParts.Web.Client.Shared.Services
{
    using Protos;

    using Models;

    public class CurrentUserProvider
    {
        public CurrentUserInfoModel CurrentUserInfo { get; private set; } = null;

        public bool IsLoggedIn => CurrentUserInfo != null;

        public bool LoadingUserInfo { get; private set; } = true;

        public void SetUserInfoLoading(bool isLoading)
        {
            LoadingUserInfo = isLoading;
        }

        public void SetCurrentUserInfo(CurrentUserInfoModel currentUserInfo)
        {
            CurrentUserInfo = currentUserInfo;

            LoadingUserInfo = false;
        }

        public void SetCurrentUserInfoFromResponse(GetCurrentUserInfoResponse response)
        {
            CurrentUserInfo = new CurrentUserInfoModel
            {
                Id = response.Id,
                Email = response.Email,
                FirstName = response.FirstName,
                LastName = response.LastName,
                UserType = response.UserType
            };

            LoadingUserInfo = false;
        }
    }
}
