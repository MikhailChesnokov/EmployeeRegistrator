namespace Web.Application.Controllers.Entrance.Forms.Handlers
{
    using Domain.Services.Entrance;

    
    
    public class DeleteEntranceFormHandler : IFormHandler<DeleteEntranceForm>
    {
        private readonly IEntranceService _entranceService;

        
        
        public DeleteEntranceFormHandler(IEntranceService entranceService)
        {
            _entranceService = entranceService;
        }

        
        
        public void Execute(DeleteEntranceForm form)
        {
            if (!form.Id.HasValue)
                throw new FormException("Entrance not chosen.");

            var entrance = _entranceService.GetById(form.Id.Value);
            
            _entranceService.Delete(entrance);
        }
    }
}