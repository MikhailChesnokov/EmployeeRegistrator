namespace Web.Application.Services.RegistrationsViewModel
{
    using System.Collections.Generic;
    using Controllers.Registration.Forms;
    using Controllers.Registration.ViewModels;



    public interface IRegistrationsViewModelService
    {
        RegistrationsViewModel ToRegistrationsViewModel(
            IEnumerable<RegistrationViewModel> registrations,
            ReportFilterForm filterForm);
    }
}