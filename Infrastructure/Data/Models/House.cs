using HouseRentingSystem.Infrastructure.Data.Constance;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House for rent")]
    public class House
    {
        [Key]
        [Comment("House identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstance.HouseTitleMaxLength)]
        [Comment("House title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstance.HouseAddressMaxLength)]
        [Comment("House address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstance.HouseDescriptionMaxLength)]
        [Comment("Description of the house")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("House image url")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("House rent price for month")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        [Comment("Agent identifier")]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;
        [Comment("User id of the renterer")]
        public string? RenterId { get; set; }
    }
}