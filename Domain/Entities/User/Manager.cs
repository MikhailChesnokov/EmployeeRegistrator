namespace Domain.Entities.User
{
    using System;
    using Department;


    public sealed class Manager : User
    {
        [Obsolete("Only for reflection", true)]
        public Manager() { }

        public Manager(
            string login,
            string password,
            Department department,
            string email)
            : base(
                login,
                password,
                Role.Manager,
                email)
        {
            ChangeDepartment(department);
        }


        public Department Department { get; set; }



        public void ChangeDepartment(Department department)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }
    }
}