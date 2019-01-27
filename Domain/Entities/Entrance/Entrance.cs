namespace Domain.Entities.Entrance
{
    using System;
    using System.Collections.Generic;
    using Building;
    using Registration;
    using User;


    public class Entrance : IRemovableEntity
    {
        [Obsolete("Only for reflection", true)]
        public Entrance() { }

        public Entrance(Building building, string name)
        {
            Rename(name);
            ChangeBuilding(building);
        }
    
        
        
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public DateTime? DeletedAtUtc { get; protected set; }

        public Building Building { get; protected set; }

        public User SecurityGuard { get; protected set; }

        public IList<Registration> Registrations { get; protected set; }

        public string CompleteName  => $"{Building?.Address} ({Name})";



        protected internal void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            name = name.Trim();

            Name = name;
        }
        
        protected internal void ChangeBuilding(Building building)
        {
            Building = building ?? throw new ArgumentNullException(nameof(building));
        }

        public void ChangeSecurityGuard(SecurityGuard securityGuard)
        {
            SecurityGuard = securityGuard ?? throw new ArgumentNullException(nameof(securityGuard));
        }
        
        public void AddRegistration(Registration registration)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));
            
            Registrations.Add(registration);
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