namespace Web.Application.Controllers.Employee.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Fio { get; set; }

        public string PersonnelNumber { get; set; }

        public bool WorkplacePresenceRequired { get; set; }
    }
}