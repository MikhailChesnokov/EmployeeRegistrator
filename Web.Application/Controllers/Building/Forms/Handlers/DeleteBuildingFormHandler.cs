namespace Web.Application.Controllers.Building.Forms.Handlers
{
    using Domain.Services.Building;

    
    public class DeleteBuildingFormHandler : IFormHandler<DeleteBuildingForm>
    {
        private readonly IBuildingService _buildingService;

        
        
        public DeleteBuildingFormHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        
        
        public void Execute(DeleteBuildingForm form)
        {
            if (!form.Id.HasValue)
                throw new FormException("Building id required.");

            _buildingService.Delete(form.Id.Value);
        }
    }
}