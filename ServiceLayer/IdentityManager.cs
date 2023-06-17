using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class IdentityManager
    {
        IdentityContext context;
        UserManager<User> userManager;

        public IdentityManager(IdentityContext context, UserManager<User> usermanager)
        {
            this.context = context;
            this.userManager = usermanager; 
        }

        #region Seeding Data with this Project

        public async Task SeedDataAsync(string adminPass, string adminEmail)
        {
            await context.SeedDataAsync(adminPass, adminEmail);
        }

        #endregion

        #region CRUD
        public async Task CreateUserAsync(User user)
        {
           
            await context.CreateUserAsync(user, Role.User);
        }
        public async Task CreateAdminAsync(User user)
        {

            await context.CreateUserAsync(user, Role.Admin);
        }

        public async Task<ClaimsPrincipal> LogInUserAsync(string egn, string password)
        {
            return await context.LogInUserAsync(egn, password);
        }

        public async Task<ClaimsPrincipal> LogOutUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await context.LogOutUserAsync(claimsPrincipal);
        }

        public async Task<User> ReadUserAsync(string key, bool useNavigationalProperties = false, bool photoes = false)
        {
            return await context.ReadUserAsync(key, useNavigationalProperties, photoes);
        }
        public async Task<User> ReadUserEGNAsync(string egn, bool useNavigationalProperties = false, bool photoes = false)
        {
            return await context.ReadUserEGNAsync(egn, useNavigationalProperties, photoes);
        }

        public async Task<List<User>> ReadAllUsersAsync(bool useNavigationalProperties = false, bool photoes = false)
        {
            List<User> users = await context.ReadAllUsersAsync(useNavigationalProperties, photoes);

            return users;
        }

        public async Task UpdateUserAsync(User user, bool useNavigationalProperties = false, bool photoes = false)
        {
            await context.UpdateUserAsync(user, useNavigationalProperties, photoes);
        }

        public async Task DeleteUserAsync(string id)
        {
            await context.DeleteUserAsync(id);
        }

        public async Task DeleteUserByEGNAsync(string egn)
        {
            await context.DeleteUserByEGNAsync(egn);
        }
        public async Task ChangePassword(string userid, string password)
        {
            await context.ChangePassword(userid, password);
        }
        #endregion

        #region Find Methods    

        public bool Exists(string key)
        {
            return context.ExistsAsync(key).Result;
        }
        public bool ExistsEGN(string egn)
        {
            return context.ExistByEGNAsync(egn).Result;
        }

        /*   public async Task<User> FindUserByEGNAsync(string name, bool useNavigationalProperties = false, bool photoes = false)
        {
            return await context.FindUserByEGNAsync(name, useNavigationalProperties, photoes);
        }*/

        /* public UpdateUserViewModel GetUpdateUserViewModel(ReadUserViewModel readUserViewModel, bool useNavigationalProperties = false)
         {
             return Transformer.GetUpdateUserViewModel(readUserViewModel, useNavigationalProperties);
         }*/
        #endregion
    }
}
