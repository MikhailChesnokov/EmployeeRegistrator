namespace Domain.Services.Entrance.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Entrance;
    using Exceptions;
    using Repository;


    public class DefaultEntranceService : IEntranceService
    {
        private readonly IRepository<Entrance> _entranceRepository;


        public DefaultEntranceService(IRepository<Entrance> entranceRepository)
        {
            _entranceRepository = entranceRepository;
        }


        public Entrance Add(Entrance entrance)
        {
            CheckForSameName(entrance, entrance.Name);

            _entranceRepository.Add(entrance);

            return _entranceRepository
                .AllActiveInclude(x => x.Building)
                .Single(x =>
                    x.Building.Id == entrance.Building.Id &&
                    x.Name.Equals(entrance.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Update(Entrance building)
        {
            CheckForSameName(building, building.Name);

            _entranceRepository.Update(building);
        }

        public Entrance GetById(int id)
        {
            return _entranceRepository.FindById(id);
        }

        public void Delete(Entrance entrance)
        {
            entrance.Delete();

            Update(entrance);
        }

        public IEnumerable<Entrance> All()
        {
            return _entranceRepository.AllInclude(x => x.Building);
        }

        public IEnumerable<Entrance> AllActive()
        {
            return _entranceRepository.AllActiveInclude(x => x.Building);
        }

        public void Rename(Entrance entrance, string name)
        {
            if (entrance == null) throw new ArgumentNullException(nameof(entrance));

            CheckForSameName(entrance, name);

            entrance.Rename(name);

            Update(entrance);
        }

        private void CheckForSameName(Entrance entrance, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (_entranceRepository
                .AllActiveInclude(x => x.Building)
                .Any(x =>
                    x.Id != entrance.Id &&
                    x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Building.Id == entrance.Building.Id))
            {
                throw new EntityAlreadyExistsException($"Вход с именем '{name}' в этом здании уже существует.");
            }
        }
    }
}