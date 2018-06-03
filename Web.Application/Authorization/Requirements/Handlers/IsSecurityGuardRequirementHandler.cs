namespace Web.Application.Authorization.Requirements.Handlers
{
    using System.Threading.Tasks;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Authorization;
    using UserProviders;



    public class IsSecurityGuardRequirementHandler : AuthorizationHandler<IsSecurityGuardRequirement>
    {
        private readonly IUserProvider<User> _userProvider;



        public IsSecurityGuardRequirementHandler(IUserProvider<User> userProvider)
        {
            _userProvider = userProvider;
        }



        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsSecurityGuardRequirement requirement)
        {
            if (_userProvider.User?.Role is Roles.SecurityGuard)
            {
                context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }
    }
}