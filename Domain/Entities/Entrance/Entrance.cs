namespace Domain.Entities.Entrance
{
    using System;
    using Building;

    

    public class Entrance : IRemovableEntity
    {
        [Obsolete("Only for reflection", true)]
        public Entrance() { }

        public Entrance(Building building, string name)
        {
            Rename(name);
            SetBuilding(building);
        }
    
        
        
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public DateTime? DeletedAtUtc { get; protected set; }

        public Building Building { get; protected set; }



        protected internal void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            name = name.Trim();

            Name = name;
        }
        
        protected void SetBuilding(Building building)
        {
            Building = building ?? throw new ArgumentNullException(nameof(building));
        }
        
        public bool IsDeleted()
        {
            return DeletedAtUtc.HasValue;
        }

        protected internal void Delete()
        {
            DeletedAtUtc = DateTime.UtcNow;
        }
    }
}