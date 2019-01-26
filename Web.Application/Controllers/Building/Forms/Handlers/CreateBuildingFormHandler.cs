namespace Web.Application.Controllers.Building.Forms.Handlers
{
    using Domain.Services.Building;
    using Domain.Entities.Building;
    using Domain.Exceptions;
    
    
    
    public class CreateBuildingFormHandler : IFormHandler<CreateBuildingForm, int>
    {
        private readonly IBuildingService _buildingService;

        
        
        public CreateBuildingFormHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        
        
        public int Execute(CreateBuildingForm form)
        {
            var building = new Building(form.Address);
                        
            try
            {
                building = _buildingService.Add(building);
            }
            catch (EntityAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            return building.Id;
        }
    }
}