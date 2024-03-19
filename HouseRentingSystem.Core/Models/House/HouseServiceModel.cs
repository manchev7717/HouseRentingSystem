using HouseRentingSystem.Core.Constants;
using HouseRentingSystem.Infrastructure.Data.Constance;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseServiceModel
    {
        public int Id { get; set; }

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

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [Range(ValidationConstance.HousePricePerMonthMinValue,
            ValidationConstance.HousePricePerMonthMaxValue, ErrorMessage = MessageConstance.PricePerMonthMessage)]
        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth {  get; set; }
        [Display(Name ="Is Rented")]
        public bool IsRented { get;set; }
    }
}