using HouseRentingSystem.Core.Constants;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using HouseRentingSystem.Infrastructure.Data.Constance;

namespace HouseRentingSystem.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = MessageConstance.RequiredErrorMessage)]
        [StringLength(ValidationConstance.AgentPhoneNumberMaxLength, 
            MinimumLength = ValidationConstance.AgentPhoneNumberMinLength,
            ErrorMessage = MessageConstance.StringLengthErrorMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
