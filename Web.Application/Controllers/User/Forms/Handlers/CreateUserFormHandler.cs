namespace Web.Application.Controllers.User.Forms.Handlers
{
    using Domain.Services.User;



    public class CreateUserFormHandler : IFormHandler<CreateUserForm>
    {
        private readonly IUserService _userService;



        public CreateUserFormHandler(
            IUserService userService)
        {
            _userService = userService;
        }



        public void Execute(CreateUserForm form)
        {
            throw new System.NotImplementedException();
        }
    }
}