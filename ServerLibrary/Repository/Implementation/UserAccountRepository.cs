using BaseLibrary.DTO;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repository.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Constant = ServerLibrary.Helpers.Constants;

namespace ServerLibrary.Repository.Implementation
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDBContext appDBContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user == null) return new GeneralResponse(false, "Model is empty");

            var checkuser = await FindUserByEmail(user.email);

            if (checkuser != null) return new GeneralResponse(false, "User already Registered");

            var applicationUser = await AddToDb(new ApplicationUser()
            {
                name = user.Fullname,
                email = user.email,
                password = BCrypt.Net.BCrypt.HashPassword(user.password)

            }
           );

            var checkAdminRole = await appDBContext.SystemRoles.FirstOrDefaultAsync(_ => _.name!.Equals(Constants.Admin));

            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDb(new SystemRole { name = Constants.Admin });
                await AddToDb(new UserRole() { roleid = createAdminRole.id, userid = applicationUser.userid });
                return new GeneralResponse(true, "Account Created");

            }

            var checkUserRole = await appDBContext.SystemRoles.FirstOrDefaultAsync(_ => _.name!.Equals(Constants.User));
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
        public async Task<LoginResponse> SignInAsync(Login user)
         {
           
            if (user == null) return new LoginResponse(false, "Model is empty");

            var appuser = await FindUserByEmail(user.email);

            if (appuser is null) return new LoginResponse(false, "User Not Found");

            var crptpass = BCrypt.Net.BCrypt.HashPassword(user.password);

            if (!BCrypt.Net.BCrypt.Verify(user.password, appuser.password))
                  return new LoginResponse(false,"Email/Password not valid");


            // var getUserRole = await appDBContext.UserRoles.FirstOrDefaultAsync(_ => _.userid== appuser.userid);
            var getUserRole = await FindUserRole(appuser.userid);
            if (getUserRole is null) return new LoginResponse(false, "User Role Not Found");

            // var getRoleName = await appDBContext.SystemRoles.FirstOrDefaultAsync(_ => _.id == getUserRole.roleid);
            var getRoleName = await FindRoleName(getUserRole.roleid);
            if (getRoleName is null) return new LoginResponse(false, "User Role Not Found");

            string jwtToken = GenerateToken(appuser, getRoleName!.name!);
            string refreshToken= GenerateRefreshToken();

            // Save the Refresh token to the database
            var findUser = await appDBContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == appuser.userid);

            if (findUser is not null)
            {
                findUser!.Token = refreshToken;
                await appDBContext.SaveChangesAsync();
            }
            else
            {
                await AddToDb(new RefreshTokenInfo() { Token = refreshToken, UserId = appuser.userid });

            }

            return new LoginResponse(true, "Login Successfully", jwtToken,refreshToken);



        }

        private string GenerateToken(ApplicationUser user, string role) 
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.userid.ToString()),
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, role!),
            };

            var token = new JwtSecurityToken(
            issuer: config.Value.Issuer,
            audience: config.Value.Audience,
            claims: userClaims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private async Task<UserRole> FindUserRole(int userId) => await appDBContext.UserRoles.FirstOrDefaultAsync(_ => _.userid == userId);

        private async Task<SystemRole> FindRoleName(int roleId) => await appDBContext.SystemRoles.FirstOrDefaultAsync(_ => _.id == roleId);

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        
        

        private async Task<ApplicationUser> FindUserByEmail(string Email) =>
            await appDBContext.Applicationusers.FirstOrDefaultAsync(_=>_.email!.ToLower()!.Equals(Email!.ToLower()));

        private async Task<T> AddToDb<T>(T model)
        {
            var result = appDBContext.Add(model);
            await appDBContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        public async Task<LoginResponse> RefreshTokenAsync(BaseLibrary.DTO.RefreshToken token)
        {
            if ( token is null) return new LoginResponse(false, "Model is empty");
            var findToken = await appDBContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
            if (findToken is null) return new LoginResponse(false, "Refresh token is required");

            //get user details
            var user = await appDBContext.Applicationusers.FirstOrDefaultAsync(_ => _.userid == findToken.UserId);
            if (user is null) return new LoginResponse(false, "Refresh token coud not be generated because user was not found");

            var userRole = await FindUserRole(user.userid);
            var roleName = await FindRoleName(userRole.roleid);
            string jwtToken = GenerateToken(user, roleName.name);
            string refreshToken = GenerateRefreshToken();


            var updateRefreshToken = await appDBContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == user.userid);
            if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");

            updateRefreshToken.Token = refreshToken;
            await appDBContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed successfully", jwtToken, refreshToken);
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await appDBContext.Applicationusers.ToListAsync();
        }

    }

}

