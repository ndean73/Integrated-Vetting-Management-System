using BaseLibrary.DTO;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Constant = ServerLibrary.Helpers.Constants;

namespace ServerLibrary.Repository.Implementation
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDBContext appDBContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user == null) return new GeneralResponse(false, "Model is empty");

            var checkuser = await FindUserUserByEmail(user.email);

            if (checkuser != null) return new GeneralResponse(false, "User already Registered");

            var applicationUser = await AddToDb(new ApplicationUser()
            {
                name = user.fullname,
                email = user.email,
                password = BCrypt.Net.BCrypt.HashPassword(user.password)

            }
           );

            var checkAdminRole = await appDBContext.t_SystemRoles.FirstOrDefaultAsync(_ => _.name!.Equals(Constants.Admin));

            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDb(new SystemRole { name = Constants.Admin });
                await AddToDb(new UserRole() { roleid = createAdminRole.id, userid = applicationUser.userid });
                return new GeneralResponse(true, "Account Created");

            }

            var checkUserRole = await appDBContext.t_SystemRoles.FirstOrDefaultAsync(_ => _.name!.Equals(Constants.User));
            SystemRole response = new();

            if (checkUserRole is null)
            {
                response = await AddToDb(new SystemRole { name = Constants.User });
                await AddToDb(new UserRole() { roleid = response.id, userid = applicationUser.userid });
                return new GeneralResponse(true, "Account Created");
            }
            else 
            {
                await AddToDb(new UserRole() { roleid = checkUserRole.id, userid = applicationUser.userid });
            }
            return new GeneralResponse(true,"Account Created");
        }
        public Task<LoginResponse> SignInAsync(Login user)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> FindUserUserByEmail(string Email) =>
            await appDBContext.t_Applicationuser.FirstOrDefaultAsync(_=>_.email!.ToLower()!.Equals(Email!.ToLower()));

        private async Task<T> AddToDb<T>(T model)
        {
            var result = appDBContext.Add(model);
            await appDBContext.SaveChangesAsync();
            return (T)result.Entity;
        }

    }

}

