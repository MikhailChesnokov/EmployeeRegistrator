namespace Web.Application.Authorization.Requirements.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities.Employee;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Authorization;
    using UserProviders;



    public class IdEqualsRequirementHandler : AuthorizationHandler<IdEqualsRequirement, Employee>
    {
        private readonly IUserProvider<User> _userProvider;



        public IdEqualsRequirementHandler(IUserProvider<User> userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }



        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IdEqualsRequirement requirement,
            Employee resource)
        {
            if (resource.Id == _userProvider.User?.Id)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}