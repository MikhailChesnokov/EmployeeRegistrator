namespace Web.Application.Controllers.User.Forms.Handlers
{
    using Authorization.UserProviders;
    using Domain.Entities.User;
    using Domain.Services.User;

    public class DeleteUserFormHandler : IFormHandler<DeleteUserForm>
    {
        private readonly IUserService _userService;
        private readonly IUserProvider<User> _userProvider;

        
        
        public DeleteUserFormHandler(IUserService userService, IUserProvider<User> userProvider)
        {
            _userService = userService;
            _userProvider = userProvider;
        }

        
        
        public void Execute(DeleteUserForm form)
        {
            var user = _userService.GetById(form.Id);
            
            if (user is SecurityGuard securityGuard && securityGuard.Entrance != null)
                throw new FormException($"Невозможно удалить охранника, так как он связан со входом '{securityGuard.Entrance.CompleteName}'.");
            
            if (_userProvider.User.Id == user.Id)
                throw new FormException("Невозможно удалить текущего пользователя.");
            
            _userService.Delete(user);
        }
    }
}