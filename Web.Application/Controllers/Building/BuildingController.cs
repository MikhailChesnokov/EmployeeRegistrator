namespace Web.Application.Controllers.Building
{
    using Forms;
    using ViewModels;
    using System.Collections.Generic;
    using Domain.Entities.User;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using Domain.Services.Building;
    using Microsoft.AspNetCore.Authorization;


    
    [Authorize]
    public sealed class BuildingController : FormControllerBase
    {
        private readonly IBuildingService _buildingService;
        private readonly IMapper _mapper;
        
        
        
        public BuildingController(
            IFormHandlerFactory formHandlerFactory,
            IAuthorizationService authorizationService,
            IBuildingService buildingService,
            IMapper mapper)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _buildingService = buildingService;
            _mapper = mapper;
        }
        
        
        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();
            
            
            var buildings = _buildingService.AllActive();

            var buildingsViewModels = _mapper.Map<IEnumerable<BuildingViewModel>>(buildings);

            return View(buildingsViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();

            
            var building = _buildingService.GetById(id);

            if (building is null)
                return NotFound();

            var buildingViewModel = _mapper.Map<BuildingViewModel>(building);
            
            return View(buildingViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return View(new CreateBuildingForm());
        }

        [HttpPost]
        public IActionResult Create(CreateBuildingForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                (int id) => this.RedirectToAction(c => c.View(id)),
                () => View(form));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            var department = _buildingService.GetById(id);

            if (department is null)
                return NotFound();

            var form = _mapper.Map<EditBuildingForm>(department);

            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(EditBuildingForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();

            
            return Form(
                form,
                () => this.RedirectToAction(x => x.View(form.Id.Value)),
                () => View(form));
        }
        
        [HttpPost]
        public IActionResult Delete(DeleteBuildingForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                () => this.RedirectToAction(x => x.List()),
                () => this.RedirectToAction(x => x.List()));
        }
    }
}