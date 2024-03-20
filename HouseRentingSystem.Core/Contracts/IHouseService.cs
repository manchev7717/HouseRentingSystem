using HouseRentingSystem.Core.Enum;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoriId);

        Task<int> CreateAsync(HouseFormModel model, int agentId);

        Task<HouseQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housePerPages = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);
        Task<IEnumerable<HouseServiceModel>> AllHousesByUsertIdAsync(string userId);

        Task<bool> ExistsAsync(int Id);

        Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);

        Task EditAsync(int houseId, HouseFormModel model);

        Task<bool> HasAgentByIdAsync(int houseId, string currentUserId);

        Task<HouseFormModel?> GetHouseFormModelByIdAsync(int id);

        Task Delete(int houseId);

        Task<bool> IsRented(int id);

        Task<bool> IsRentedByUserWithId(int houseId, string UserId);

        Task Rent(int houseId, string userId);

        Task Leave(int houseId);
    }
}
