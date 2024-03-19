using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enum;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.CustomAttributes;
using HouseRentingSystem.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : BaseController
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(IHouseService _houseService
            , IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel query)
        {
            var model = await houseService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage
                );

            query.TotalHousesCount = model.TotalHousesCount;
            query.Houses = model.Houses;
            query.Categories = await houseService.AllCategoriesNamesAsync();
                
            return View(query);
        }

        [HttpGet]

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses;
            var userId = User.Id();
            if (await agentService.ExistsByIdAsync(userId))
            {
                var agentId = await agentService.GetAgentIdAsync(userId) ?? 0;
                myHouses = await houseService.AllHousesByAgentIdAsync(agentId);
            }
            else
            {
                myHouses = await houseService.AllHousesByUsertIdAsync(userId);
            }

            return View(myHouses);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await houseService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }
            var model = await houseService.HouseDetailsById(id);
            return View(model);
        }

        [HttpGet]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await houseService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "");
            }
            if (ModelState.IsValid == false)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            int? agentId = await agentService.GetAgentIdAsync(User.Id());

            int houseId = await houseService.CreateAsync(model, agentId ?? 0);

            return RedirectToAction(nameof(Details), new { id = houseId });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new HouseFormModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel model)
        {
            return RedirectToAction(nameof(Details), new { id = 1 });

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new HouseDetailsViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));

        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

    }
}
