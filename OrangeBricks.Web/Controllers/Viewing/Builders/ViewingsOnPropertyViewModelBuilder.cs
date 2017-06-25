using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Controllers.Viewing.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Builders
{
    public class ViewingsOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingsOnPropertyViewModel Build(int propertyId)
        {
            return new ViewingsOnPropertyViewModel
            {
                Viewings = _context.Viewing
                    .Include(x => x.Property)
                    .Where(x => x.Property.Id == propertyId)
                    .OrderBy(x => x.ViewAt)
                    .Select(x => new ViewingViewModel
                    {
                        ViewAt = x.ViewAt,
                        BuyerUserName = x.Buyer.UserName,
                        Property = new PropertyViewModel()
                        {
                            StreetName = x.Property.StreetName,
                            Description = x.Property.Description,
                            NumberOfBedrooms = x.Property.NumberOfBedrooms,
                            PropertyType = x.Property.PropertyType,
                        }
                    })
            };
        }
    }
}