namespace Domain.Services.Entrance
{
    using System.Collections.Generic;
    using Entities.Building;
    using Entities.Entrance;

    
    
    public interface IEntranceService : IDomainService
    {
        Entrance Add(Entrance entrance);

        void Update(Entrance entrance, Building building);

        Entrance GetById(int id);

        void Delete(Entrance entrance);
        
        IEnumerable<Entrance> All();

        IEnumerable<Entrance> AllActive();

        
        void Rename(Entrance entrance, string name);
    }
}