namespace Web.Application.Controllers.Entrance.Forms.Handlers
{
    using Domain.Exceptions;
    using Domain.Services.Building;
    using Domain.Services.Entrance;

    
    
    public class EditEntranceFormHandler : IFormHandler<EditEntranceForm>
    {
        private readonly IEntranceService _entranceService;
        private readonly IBuildingService _buildingService;

        
        
        public EditEntranceFormHandler(
            IBuildingService buildingService,
            IEntranceService entranceService)
        {
            _buildingService = buildingService;
            _entranceService = entranceService;
        }


        public void Execute(EditEntranceForm form)
        {
            if (!form.BuildingId.HasValue)
                throw new FormException("Building not chosen.");
            
            if (!form.Id.HasValue)
                throw new FormException("Entrance not chosen.");
            
            var building = _buildingService.GetById(form.BuildingId.Value);

            var entrance = _entranceService.GetById(form.Id.Value);
            
            try
            {   
                _entranceService.Rename(entrance, form.Name);
            }
            catch (EntityAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }
            
            _entranceService.Update(entrance, building);
        }
    }
}