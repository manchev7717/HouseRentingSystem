using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enum;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Comman;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseRentingSystem.Core.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly IRepository repository;
        public HouseService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<HouseQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housePerPages = 1)
        {
            var housesToShow = repository.AllReadOnly<Infrastructure.Data.Models.House>();

            if (category != null)
            {
                housesToShow = housesToShow
                    .Where(h => h.Category.Name == category);
            }

            if (searchTerm != null)
            {
                string normalizedSearchingTerm = searchTerm.ToLower();

                housesToShow = housesToShow
                    .Where(h => (h.Title.ToLower().Contains(normalizedSearchingTerm) ||
                                h.Address.ToLower().Contains(normalizedSearchingTerm) ||
                                h.Description.ToLower().Contains(normalizedSearchingTerm)));
            }

            housesToShow = sorting switch
            {
                HouseSorting.Price => housesToShow
                .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesToShow
                .OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.Id),

                _ => housesToShow.OrderByDescending(h => h.Id)
            };

            var houses = await housesToShow
                .Skip((currentPage - 1) * housePerPages)
                .Take(housePerPages)
                .ProjectToHouseServiceModel()
                .ToListAsync();

            var housesCount = await housesToShow.CountAsync();

            return new HouseQueryServiceModel()
            {
                Houses = houses,
                TotalHousesCount = housesCount,
            };
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => new HouseCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.House>()
                .Where(h => h.AgentId == agentId)
                .ProjectToHouseServiceModel()
                .ToListAsync();

        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUsertIdAsync(string userId)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.House>()
               .Where(h => h.RenterId == userId)
               .ProjectToHouseServiceModel()
               .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoriId)
        {
            return await repository.AllReadOnly<Category>()
                 .AnyAsync(c => c.Id == categoriId);
        }

        public async Task<int> CreateAsync(HouseFormModel model, int agentId)
        {
            Infrastructure.Data.Models.House house = new Infrastructure.Data.Models.House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.CategoryId,
                AgentId = agentId
            };

            await repository.AddAsync(house);
            await repository.SaveChangesAsync();

            return house.Id;
        }

        public async Task Delete(int houseId)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(houseId);

            if (house != null)
            {
                await repository.Remove(house);
                await repository.SaveChangesAsync();
            }

        }

        public async Task EditAsync(int houseId, HouseFormModel model)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(houseId);

            if (house != null)
            {
                house.Address = model.Address;
                house.Description = model.Description;
                house.ImageUrl = model.ImageUrl;
                house.CategoryId = model.CategoryId;
                house.PricePerMonth = model.PricePerMonth;
                house.Title = model.Title;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int Id)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.House>()
                .AnyAsync(h => h.Id == Id);
        }

        public async Task<HouseFormModel?> GetHouseFormModelByIdAsync(int id)
        {
            var house = await repository.AllReadOnly<Infrastructure.Data.Models.House>()
                .Where(h => h.Id == id)
                .Select(h => new HouseFormModel()
                {
                    Address = h.Address,
                    CategoryId = h.CategoryId,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                    PricePerMonth = h.PricePerMonth,
                })
                .FirstOrDefaultAsync();

            if (house != null)
            {
                house.Categories = await AllCategoriesAsync();
            }

            return house;
        }

        public async Task<bool> HasAgentByIdAsync(int houseId, string currentUserId)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.House>()
                .AnyAsync(h => h.Id == houseId && h.Agent.UserId == currentUserId);
        }

        public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.House>()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category.Name,
                    Agent = new Models.Agent.AgentServiceModel()
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email
                    }

                })
                .FirstAsync();
        }

        public async Task<bool> IsRented(int id)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(id);
            var result = house?.RenterId != null;
            return result;
        }

        public async Task<bool> IsRentedByUserWithId(int houseId, string UserId)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(houseId);
            if (house == null)
            {
                return false;
            }
            if (house.RenterId == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
        {
            return await repository
                .AllReadOnly<Infrastructure.Data.Models.House>()
                .OrderByDescending(x => x.Id)
                .Select(x => new HouseIndexServiceModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                })
                .Take(3)
                .ToListAsync();

        }

        public async Task Leave(int houseId)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(houseId);
            if (house != null)
            {
                house.RenterId = null;
                await repository.SaveChangesAsync();
            }

        }

        public async Task Rent(int houseId, string userId)
        {
            var house = await repository.GetByIdAsync<Infrastructure.Data.Models.House>(houseId);

            if (house != null)
            {
                house.RenterId = userId;
                await repository.SaveChangesAsync();
            }
        }
    }
}
