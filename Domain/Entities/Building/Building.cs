namespace Domain.Entities.Building
{
    using System;
    using System.Collections.Generic;
    using Entrance;


    public class Building : IRemovableEntity
    {
        [Obsolete("Only for reflection", true)]
        public Building() { }

        public Building(string address)
        {
            ChangeAddress(address);
        }
        
        
        
        public int Id { get; protected set; }

        public string Address { get; protected set; }
        
        public DateTime? DeletedAtUtc { get; protected set; }
        
        public IList<Entrance> Entrances { get; protected set; }



        protected internal void ChangeAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(address));

            address = address.Trim();

            Address = address;
        }

        public void AddEntrance(Entrance entrance)
        {
            if (entrance == null) throw new ArgumentNullException(nameof(entrance));
            
            Entrances.Add(entrance);
        }

        public void RemoveEntrance(Entrance entrance)
        {
            if (entrance == null) throw new ArgumentNullException(nameof(entrance));

            Entrances.Remove(entrance);
        }

        public bool IsDeleted()
        {
            return DeletedAtUtc.HasValue;
        }

        public void Delete()
        {
            DeletedAtUtc = DateTime.UtcNow;
        }
    }
}