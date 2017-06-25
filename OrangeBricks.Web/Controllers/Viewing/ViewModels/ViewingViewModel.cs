using System;
using System.ComponentModel.DataAnnotations;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.ViewModels
{
    public class ViewingViewModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime ViewAt { get; set; }
        public PropertyViewModel Property { get; set; }
        [Required]
        public int PropertyId { get; set; }
        public string BuyerUserName { get; set; }
        [Required]
        public string BuyerId { get; set; }
    }
}