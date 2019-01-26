namespace Web.Application.Controllers.Building.Forms.Handlers
{
    using System;
    using Domain.Services.Building;
    
    
    
    public class EditBuildingFormHandler : IFormHandler<EditBuildingForm>
    {
        private readonly IBuildingService _buildingService;

        
        
        public EditBuildingFormHandler(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        
        
        public void Execute(EditBuildingForm form)
        {
            if (!form.Id.HasValue)
                throw new Exception("Department Id required.");
            
            var building = _buildingService.GetById(form.Id.Value);
            
            _buildingService.ChangeAddress(building, form.Address);
        }
    }
}