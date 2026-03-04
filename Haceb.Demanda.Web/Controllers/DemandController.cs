using AutoMapper;
using Haceb.Demand.Services.Dto;
using Haceb.Demand.Services.IPort;
using Haceb.Demanda.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace Haceb.Demanda.Web.Controllers
{
    public class DemandController(IDemandServices _demandServices, IMapper _mapper) : Controller
    {
        // GET: DemandController
        public async Task<ActionResult> Index()
        {
            var lista = await _demandServices.GetListDemand();
            var list = _mapper.Map<List<DemandIndexViewModel>>(lista);
            return View(list);
        }

        // GET: DemandController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            var response = await _demandServices.GetIdDemand(Id);
            var model = _mapper.Map<DemandDetailViewModel>(response);
            return View(model);
        }

        // GET Create
        public async Task<IActionResult> Create()
        {
            var model = new DemandCreateViewModel();
            await LoadSelectLists(model);
            return View(model);
        }

        // POST Create
        [HttpPost]
        public async Task<IActionResult> Create(DemandCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists(model);
                return View(model);
            }

            var mpaDemand = _mapper.Map<DemandRequestDto>(model);
            var response = await _demandServices.PostCreateDemand(mpaDemand);


            if (!response)
            {
                ModelState.AddModelError("", "Error al crear la demanda");
                await LoadSelectLists(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET
        public async Task<IActionResult> Progress(int Id)
        {
            var response = await _demandServices.GetIdDemand(Id);
            var model = _mapper.Map<DemandProgressViewModel>(response);

            await LoadProgressSelectLists(model);
            return View(model);
        }

        // POST Create
        [HttpPost]
        public async Task<IActionResult> Progress(DemandProgressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadProgressSelectLists(model);
                return View(model);
            }

            var mapDemand = _mapper.Map<DemandUpdateRequestDto>(model);
            var response = await _demandServices.PutProgressDemand(mapDemand);

            if (!response)
            {
                ModelState.AddModelError("", "Error al actualizar la demanda");
                await LoadProgressSelectLists(model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        private async Task LoadSelectLists(DemandCreateViewModel model)
        {
            var lookup = await _demandServices.GetLookup();
            model.Ratings = lookup.FirstOrDefault(x => x.Type == "Rating")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            model.Types = lookup.FirstOrDefault(x => x.Type == "DemandType")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            var Users = await _demandServices.GetListUser();
            model.Users = Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Username
            }).ToList();
        }

        private async Task LoadProgressSelectLists(DemandProgressViewModel model)
        {
            var lookup = await _demandServices.GetLookup();
            model.Ratings = lookup.FirstOrDefault(x => x.Type == "Rating")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            model.Types = lookup.FirstOrDefault(x => x.Type == "DemandType")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            model.Priorities = lookup.FirstOrDefault(x => x.Type == "Prioritize")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            model.Status = lookup.FirstOrDefault(x => x.Type == "Status")?.Items.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Description
            }).ToList();

            var Users = await _demandServices.GetListUser();
            model.Users = Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Username
            }).ToList();
        }
    }
}
