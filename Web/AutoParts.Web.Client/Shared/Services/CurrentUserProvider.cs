namespace AutoParts.Web.Client.Shared.Services
{
    using Protos;

    using System;
    using System.Collections.Generic;

    using Models;

    public class CurrentUserProvider
    {
        public CurrentUserInfoModel CurrentUserInfo { get; private set; } = null;

        public bool IsLoggedIn => CurrentUserInfo != null;

        public bool LoadingUserInfo { get; private set; } = true;

        private List<Action> actions = new List<Action>();

        public void AddEventListenerOnLoadedUserInfo(Action action)
        {
            actions.Add(action);
        }

        public void SetUserInfoLoading(bool isLoading)
        {
            LoadingUserInfo = isLoading;

            if (!isLoading)
            {
                actions.ForEach(action => action());
            }
        }

        public void SetCurrentUserInfo(CurrentUserInfoModel currentUserInfo)
        {
            CurrentUserInfo = currentUserInfo;

            SetUserInfoLoading(false);
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

            SetUserInfoLoading(false);
        }
    }
}
