using BaseLibrary.DTO;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Service.Contract
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SignInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken user);

        Task<List<ApplicationUser>> GetUsersAsync();
    }
}
