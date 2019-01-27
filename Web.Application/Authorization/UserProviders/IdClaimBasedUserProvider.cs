namespace Web.Application.Authorization.UserProviders
{
    using System;
    using System.Security.Claims;
    using Domain.Entities;
    using Domain.Services.User;
    using Microsoft.AspNetCore.Http;
    using ClaimTypes = Claims.ClaimTypes;



    public class IdClaimBasedUserProvider<TUser> : UserProviderBase<TUser>
        where TUser : class, IEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private TUser _user;



        public IdClaimBasedUserProvider(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }



        protected override TUser GetUser()
        {
            if (_user != null)
                return _user;

            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated != true)
                return default;

            Claim idClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Id);

            if (idClaim is null)
                return default;

            if (!int.TryParse(idClaim.Value, out int id))
                return default;

            _user = _userService.GetById(id) as TUser;

            return _user;
        }
    }
}