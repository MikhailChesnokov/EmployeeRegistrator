namespace Domain.Entities.Employee
{
    using System;
    using Department;
    using Exceptions;



    public class Employee : IRemovableEntity
    {
        [Obsolete("Only for reflection", true)]
        public Employee() { }

        public Employee(
            string firstName,
            string surname,
            string patronymic,
            bool isWorkplacePresenceRequired,
            string personnelNumber,
            Department department)
        {
            SetFirstName(firstName);
            SetSurname(surname);
            SetPatronymic(patronymic);
            SetWorkplacePresenceRequirement(isWorkplacePresenceRequired);
            SetPersonnelNumber(personnelNumber);
            SetDepartment(department);
        }



        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        public string Patronymic { get; protected set; }

        public string Fio => $"{Surname} {FirstName} {Patronymic}";

        public bool WorkplacePresenceRequired { get; protected set; }

        public string PersonnelNumber { get; protected set; }

        public Department Department { get; protected set; }

        public DateTime? DeletedAtUtc { get; set; }

        public int Id { get; protected set; }



        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));

            firstName = firstName.Trim();
            
            FirstName = firstName;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(surname));

            Surname = surname.Trim();
            
            Surname = surname;
        }

        public void SetPatronymic(string patronymic)
        {
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(patronymic));

            patronymic = patronymic.Trim();
            
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

        public void SetDepartment(Department department)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        protected internal void Delete()
        {
            if (DeletedAtUtc.HasValue)
            {
                throw new EmployeeAlreadyRemovedException($"Сотрудник \"{Fio}\" уже был удален ранее.");
            }

            DeletedAtUtc = DateTime.Today;
        }

        public bool IsDeleted()
        {
            return DeletedAtUtc.HasValue;
        }
    }
}