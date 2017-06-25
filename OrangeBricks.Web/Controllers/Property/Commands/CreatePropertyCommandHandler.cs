using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class CreatePropertyCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public CreatePropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(CreatePropertyCommand command)
        {
            var property = new Models.Property
            {
                PropertyType = command.PropertyType,
                StreetName = command.StreetName,
                Description = command.Description,
                NumberOfBedrooms = command.NumberOfBedrooms,
                Availability = new Availability
                {
                    StartDate = command.Availability.StartDate,
                    StartTime = command.Availability.StartTime,
                    EndTime = command.Availability.EndTime,
                    ViewingDuration = command.Availability.ViewingDuration
                },
                SellerUserId = command.SellerUserId,
            };
            
            _context.Properties.Add(property);

            _context.SaveChanges();
        }
    }
}