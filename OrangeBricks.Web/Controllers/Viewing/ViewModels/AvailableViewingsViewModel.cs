using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OrangeBricks.Web.Controllers.Viewing.ViewModels
{
    public class AvailableViewingsViewModel
    {
        public int PropertyId { get; set; }

        public string StartViewingDate { get; set; }

        [DisplayName("Viewing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ViewingDate { get; set; }
        public TimeSpan ViewingTime { get; set; }

        [DisplayName("Viewing Time")]
        public IEnumerable<SelectListItem> Times { get; set; }

        public int ViewingDuration { get; set; }
    }
}