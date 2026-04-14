

using BaseLibrary.DTO;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Service.Contract;
//using System.Net.Http;
using System.Net.Http.Json; // this is required for PostAsJsonAsync


namespace ClientLibrary.Service.Implementation
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthUrl = "api/authentication";
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
                        
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();

        }

        public Task<LoginResponse> RefreshTokenAsync(RefreshToken user)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);

            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occured");
            return await result.Content.ReadFromJsonAsync<LoginResponse>();

        }
    }
}
