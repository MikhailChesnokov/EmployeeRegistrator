namespace Domain.Infrastructure.Authentication
{
    using System.Security.Claims;
    using Entities;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;



    public class IdClaimBasedCookieAuthenticationService<TUser> : IAuthenticationService<TUser>
        where TUser : IEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _scheme;




        public IdClaimBasedCookieAuthenticationService(
            IHttpContextAccessor httpContextAccessor,
            string scheme)
        {
            _httpContextAccessor = httpContextAccessor;
            _scheme = scheme;
        }



        public async void SignIn(TUser user)
        {
            Claim[] claims =
            {
                new Claim(ClaimTypes.Id, user.Id.ToString(), ClaimValueTypes.Integer64)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookie");

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync(_scheme, principal, new AuthenticationProperties
            {
                IsPersistent = true
            });
        }

        public async void SignOut()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(_scheme);
        }
    }



    public static class ClaimTypes
    {
        public const string Id = "Id";
    }
}