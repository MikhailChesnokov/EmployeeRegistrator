namespace Web.Application.Controllers.Entrance
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Building;
    using Domain.Entities.Department;
    using Domain.Entities.User;
    using Domain.Services.Building;
    using Domain.Services.Entrance;
    using Forms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels;


    [Authorize]
    public class EntranceController : FormControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEntranceService _entranceService;
        private readonly IBuildingService _buildingService;
        
        
        
        public EntranceController(
            IFormHandlerFactory formHandlerFactory,
            IAuthorizationService authorizationService,
            IEntranceService entranceService,
            IMapper mapper, IBuildingService buildingService)
            : base(
                formHandlerFactory,
                authorizationService)
        {
            _entranceService = entranceService;
            _mapper = mapper;
            _buildingService = buildingService;
        }
        
        
        
        [HttpGet]
        public IActionResult List()
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();
            
            
            var entrances = _entranceService.AllActive();

            var entrancesViewModels = _mapper.Map<IEnumerable<EntranceViewModel>>(entrances);

            return View(entrancesViewModels);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            if (!RoleIs(Roles.Administrator, Roles.Manager, Roles.SecurityGuard))
                return Forbid();

            
            var entrance = _entranceService.GetById(id);

            if (entrance is null)
                return NotFound();

            var entranceViewModel = _mapper.Map<EntranceViewModel>(entrance);
            
            return View(entranceViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return View(new CreateEntranceForm
            {
                Buildings = GetBuildingsItems(_buildingService.AllActive())
            });
        }

        [HttpPost]
        public IActionResult Create(CreateEntranceForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                (int id) => this.RedirectToAction(c => c.View(id)),
                () =>
                {
                    form.Buildings = GetBuildingsItems(_buildingService.AllActive(), form.BuildingId);
                    
                    return View(form);
                });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            var entrance = _entranceService.GetById(id);

            if (entrance is null)
                return NotFound();

            var form = _mapper.Map<EditEntranceForm>(entrance);

            form.Buildings = GetBuildingsItems(_buildingService.AllActive());
            
            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(EditEntranceForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();

            
            return Form(
                form,
                () => this.RedirectToAction(x => x.View(form.Id.Value)),
                () =>
                {
                    form.Buildings = GetBuildingsItems(_buildingService.AllActive(), form.BuildingId);
                    
                    return View(form);
                });
        }
        
        [HttpPost]
        public IActionResult Delete(DeleteEntranceForm form)
        {
            if (!RoleIs(Roles.Administrator))
                return Forbid();
            
            
            return Form(
                form,
                () => this.RedirectToAction(x => x.List()),
                () => this.RedirectToAction(x => x.List()));
        }
        
        private IEnumerable<SelectListItem> GetBuildingsItems(IEnumerable<Building> buildings, int? selectedId = null)
        {
            return buildings.Select(x => new SelectListItem
            {
                Text = x.Address,
                Value = x.Id.ToString(),
                Selected = selectedId != null && selectedId == x.Id,
                Disabled = false,
                Group = default
            });
        }
    }
}