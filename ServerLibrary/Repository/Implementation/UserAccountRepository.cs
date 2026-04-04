using BaseLibrary.DTO;
using BaseLibrary.Responses;
using ServerLibrary.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLibrary.Repository.Implementation
{
    public class UserAccountRepository : IUserAccount
    {
        public Task<GeneralResponse> CreateAsync(Register user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }
    }
}
