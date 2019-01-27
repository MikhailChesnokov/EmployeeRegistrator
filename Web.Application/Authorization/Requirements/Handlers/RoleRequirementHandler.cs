namespace Web.Application.Authorization.Requirements.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Authorization;
    using UserProviders;



    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement, IEnumerable<Role>>
    {
        private readonly IUserProvider<User> _userProvider;



        public RoleRequirementHandler(IUserProvider<User> userProvider)
        {
            _userProvider = userProvider;
        }



        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoleRequirement requirement,
            IEnumerable<Role> resource)
        {
            if (_userProvider.User is User user && resource.Contains(user.Role))
            {
                context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }
    }
}