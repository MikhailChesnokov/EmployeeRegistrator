namespace Web.Application.Controllers.Employee
{
    using System;
    using Forms;
    using Microsoft.AspNetCore.Mvc;



    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        public IActionResult View(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEmployeeForm());
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeForm form)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeForm form)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}