namespace Web.Application.Controllers.Entrance.ViewModels
{
    using Building.ViewModels;

    
    
    public class EntranceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BuildingViewModel Building { get; set; }
    }
}