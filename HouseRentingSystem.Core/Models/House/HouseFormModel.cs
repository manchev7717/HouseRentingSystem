using HouseRentingSystem.Core.Constants;
using HouseRentingSystem.Infrastructure.Data.Constance;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseFormModel
    {
        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [StringLength(ValidationConstance.HouseTitleMaxLength,
            MinimumLength = ValidationConstance.HouseTitleMinLength,
            ErrorMessage = MessageConstance.StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [StringLength(ValidationConstance.HouseAddressMaxLength,
            MinimumLength = ValidationConstance.HouseAddressMinLength,
            ErrorMessage = MessageConstance.StringLengthErrorMessage)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [StringLength(ValidationConstance.HouseDescriptionMaxLength,
            MinimumLength = ValidationConstance.HouseDescriptionMinLength,
            ErrorMessage = MessageConstance.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [Range(ValidationConstance.HousePricePerMonthMinValue,
            ValidationConstance.HousePricePerMonthMaxValue,ErrorMessage = MessageConstance.PricePerMonthMessage)]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } 
            = new List<HouseCategoryServiceModel>();
    }
}
