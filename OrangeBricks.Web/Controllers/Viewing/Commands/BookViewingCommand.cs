using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Controllers.Viewing.Commands
{
    public class BookViewingCommand
    {
        public string BuyerId { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public DateTime ViewingDate { get; set; }
        [Required]
        public TimeSpan ViewingTime { get; set; }
    }
}