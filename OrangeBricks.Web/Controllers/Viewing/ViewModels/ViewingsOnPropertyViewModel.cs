using System.Collections.Generic;
using OrangeBricks.Web.Controllers.Property.ViewModels;

namespace OrangeBricks.Web.Controllers.Viewing.ViewModels
{
    public class ViewingsOnPropertyViewModel
    {
        public PropertyViewModel Property { get; set; }
        public IEnumerable<ViewingViewModel> Viewings { get; set; }
    }
}