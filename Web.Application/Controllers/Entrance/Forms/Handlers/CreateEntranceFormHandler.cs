namespace Web.Application.Controllers.Entrance.Forms.Handlers
{
    using Domain.Entities.Entrance;
    using Domain.Exceptions;
    using Domain.Services.Building;
    using Domain.Services.Entrance;

    
    
    public class CreateEntranceFormHandler : IFormHandler<CreateEntranceForm, int>
    {
        private readonly IEntranceService _entranceService;
        private readonly IBuildingService _buildingService;

        
        
        public CreateEntranceFormHandler(
            IEntranceService entranceService,
            IBuildingService buildingService)
        {
            _entranceService = entranceService;
            _buildingService = buildingService;
        }

        
        
        public int Execute(CreateEntranceForm form)
        {
            if (!form.BuildingId.HasValue)
                throw new FormException("Building not chosen.");
            
            var building = _buildingService.GetById(form.BuildingId.Value);

            var entrance = new Entrance(building, form.Name);
            
            try
            {
                entrance = _entranceService.Add(entrance);
            }
            catch (EntityAlreadyExistsException e)
            {
                throw new FormException(e.Message);
            }

            return entrance.Id;
        }
    }
}