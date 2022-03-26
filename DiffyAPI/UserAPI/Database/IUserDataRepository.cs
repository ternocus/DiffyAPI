﻿using DiffyAPI.Database.Model;
using DiffyAPI.UserAPI.Controllers.Model;
using DiffyAPI.UserAPI.Core.Model;
using DiffyAPI.UserAPI.Database.Model;

namespace DiffyAPI.UserAPI.Database
{
    public interface IUserDataRepository
    {
        public Task<IEnumerable<UserData>> GetUserListData();
        public Task<UserData> GetUserData(string username);
        public Task UploadUserData(UploadUser registerCredential);
    }
}
