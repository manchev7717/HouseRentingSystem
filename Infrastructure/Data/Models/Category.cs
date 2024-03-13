using System.ComponentModel.DataAnnotations;
using HouseRentingSystem.Infrastructure.Data.Constance;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House category")]
    public class Category
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstance.CategoryNameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;

        public IList<House> Houses { get; set; } = new List<House>();
    }
}