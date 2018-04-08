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
            string patronymic)
        {
            SetFirstName(firstName);
            SetSurname(surname);
            SetPatronymic(patronymic);
        }



        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        public string Patronymic { get; protected set; }

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
    }
}