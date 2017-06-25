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
            var property = _context.Properties.Find(propertyId);
            return new ViewingsOnPropertyViewModel
            {
                Property = new PropertyViewModel()
                {
                    StreetName = property?.StreetName,
                    NumberOfBedrooms = property?.NumberOfBedrooms ?? 0,
                    PropertyType = property?.PropertyType,
                },
                Viewings = _context.Viewing
                    .Include(x => x.Property)
                    .Where(x => x.Property.Id == propertyId)
                    .OrderBy(x => x.ViewAt)
                    .Select(x => new ViewingViewModel
                    {
                        ViewAt = x.ViewAt,
                        BuyerUserName = x.Buyer.UserName
                    })
            };
        }
    }
}