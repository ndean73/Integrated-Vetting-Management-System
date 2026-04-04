using BaseLibrary.DTO;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLibrary.Repository.Contract
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SignInAsync(Login user);

    }
}
