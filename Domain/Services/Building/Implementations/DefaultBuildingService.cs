namespace Domain.Services.Building.Implementations
{
    using System;
    using System.Linq;
    using Exceptions;
    using System.Collections.Generic;
    using Repository;
    using Domain.Entities.Building;
    
    
    
    public class DefaultBuildingService : IBuildingService
    {
        private readonly IRepository<Building> _buildingsRepository;

        
        
        public DefaultBuildingService(IRepository<Building> buildingsRepository)
        {
            _buildingsRepository = buildingsRepository;
        }

        

        public Building Add(Building building)
        {
            CheckForSameAddress(building, building.Address);
            
            _buildingsRepository.Add(building);

            return _buildingsRepository.AllActive().Single(x => x.Address == building.Address);
        }

        public void Update(Building building)
        {
            CheckForSameAddress(building, building.Address);
            
            _buildingsRepository.Update(building);
        }

        public Building GetById(int id)
        {
            return _buildingsRepository.FindById(id);
        }

        public void Delete(int id)
        {
            var building = _buildingsRepository.FindById(id);
            
            if (building == null)
                throw new ArgumentException("Building not found.");
            
            building.Delete();

            Update(building);
        }

        public IEnumerable<Building> All()
        {
            return _buildingsRepository.All();
        }

        public IEnumerable<Building> AllActive()
        {
            return _buildingsRepository.AllActive();
        }

        public void ChangeAddress(Building building, string address)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));
            
            CheckForSameAddress(building, address);
            
            building.ChangeAddress(address);
            
            Update(building);
        }


        private void CheckForSameAddress(Building building, string address)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));

            if (_buildingsRepository.AllActive().Any(x => x.Id != building.Id && string.Equals(x.Address, address, StringComparison.InvariantCultureIgnoreCase)))
                throw new EntityAlreadyExistsException($"Здание с адресом \"{address}\" уже существует.");
        }
    }
}