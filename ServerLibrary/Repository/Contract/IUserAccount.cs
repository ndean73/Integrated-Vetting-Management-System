using BaseLibrary.DTO;
using BaseLibrary.Entities;
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
        Task<LoginResponse> RefreshTokenAsync(RefreshToken user);
        Task<List<ApplicationUser>> GetUsersAsync();



    }
}
