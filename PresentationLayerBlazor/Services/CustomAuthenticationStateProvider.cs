using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

// https://github.com/JamesNK/Newtonsoft.Json/issues/1713 (!)

namespace BlazorServer.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsIdentity userClaimsIdentity;
        private readonly ProtectedSessionStorage sessionStorage;
        private JsonSerializerSettings jsonSerializerSettings;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage storage)
        {
            sessionStorage = storage;

            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            jsonSerializerSettings.MaxDepth = 32;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string jsonClaimsIdentity = (await sessionStorage.GetAsync<string>("claimsIdentity")).Value;
            
            if (jsonClaimsIdentity is null)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            userClaimsIdentity = JsonConvert.DeserializeObject<ClaimsIdentity>(jsonClaimsIdentity, new ClaimConverter());

            userClaimsIdentity = new ClaimsIdentity(userClaimsIdentity.Claims, "IsAuthenticatedIssueSolved");

            return new AuthenticationState(new ClaimsPrincipal(userClaimsIdentity));
        }

        public new async Task NotifyAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            try
            {
                string jsonClaimsIdentity = JsonConvert.SerializeObject(task.Result.User.Identity, jsonSerializerSettings);
                await sessionStorage.SetAsync("claimsIdentity", jsonClaimsIdentity);
                base.NotifyAuthenticationStateChanged(Task.FromResult(await GetAuthenticationStateAsync()));
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<ClaimsPrincipal> GetUserPrincipal(bool notificationChangedEventInvoked = false)
        {
            if (!notificationChangedEventInvoked)
            {
                await GetAuthenticationStateAsync();
            }
            
            return new ClaimsPrincipal(userClaimsIdentity);
        }

        public async Task<IIdentity> GetUserIdentity(bool notificationChangedEventInvoked = false)
        {
            if (!notificationChangedEventInvoked)
            {
                await GetAuthenticationStateAsync();
            }
            
            return userClaimsIdentity;
        }

        public async Task<bool> IsAuthenticated(bool notificationChangedEventInvoked = false)
        {
            if (!notificationChangedEventInvoked)
            {
                await GetAuthenticationStateAsync();
            }

            return userClaimsIdentity.IsAuthenticated;
        }

        public async Task RemoveClaimFromSessionStorageAsync()
        {
            await sessionStorage.DeleteAsync("claimsIdentity");
            userClaimsIdentity = new ClaimsIdentity();
        }

    }
}
