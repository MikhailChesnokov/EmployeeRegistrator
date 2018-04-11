﻿namespace Web.Application.Controllers.Registration.Profiles
{
    using AutoMapper;
    using Domain.Entities;
    using ViewModels;



    internal class EmployeeRegistrationProfile : Profile
    {
        public EmployeeRegistrationProfile()
        {
            CreateMap<Registration, EmployeeRegistrationViewModel>();
        }
    }
}