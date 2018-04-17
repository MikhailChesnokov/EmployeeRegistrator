namespace Domain.Entities
{
    using System;



    public class Employee : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Employee() { }

        public Employee(
            string firstName,
            string surname,
            string patronymic,
            bool isWorkplacePresenceRequired,
            string personnelNumber)
        {
            SetFirstName(firstName);
            SetSurname(surname);
            SetPatronymic(patronymic);
            SetWorkplacePresenceRequirement(isWorkplacePresenceRequired);
            SetPersonnelNumber(personnelNumber);
        }



        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        public string Patronymic { get; protected set; }

        public string Fio => $"{Surname} {FirstName} {Patronymic}";

        public bool WorkplacePresenceRequired { get; protected set; }

        public string PersonnelNumber { get; protected set; }

        public int Id { get; protected set; }



        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));

            FirstName = firstName;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(surname));

            Surname = surname;
        }

        public void SetPatronymic(string patronymic)
        {
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(patronymic));

            Patronymic = patronymic;
        }

        public void SetWorkplacePresenceRequirement(bool isWorkplacePresenceRequired)
        {
            WorkplacePresenceRequired = isWorkplacePresenceRequired;
        }

        public void SetPersonnelNumber(string personnelNumber)
        {
            if (string.IsNullOrWhiteSpace(personnelNumber))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(personnelNumber));

            PersonnelNumber = personnelNumber;
        }
    }
}