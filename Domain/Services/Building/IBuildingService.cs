namespace Domain.Services.Building
{
    using System.Collections.Generic;
    using Domain.Entities.Building;
    
    
    
    public interface IBuildingService : IDomainService
    {
        Building Add(Building building);

        void Update(Building building);

        Building GetById(int id);

        void Delete(int id);
        
        IEnumerable<Building> All();

        IEnumerable<Building> AllActive();



        void ChangeAddress(Building building, string address);
    }
}