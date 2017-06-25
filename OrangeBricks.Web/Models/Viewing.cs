using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime ViewAt { get; set; }
        public Property Property { get; set; }
        [Required]
        public int PropertyId { get; set; }
        public ApplicationUser Buyer { get; set; }
        [Required]
        public string BuyerId { get; set; }
    }
}