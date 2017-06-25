using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public bool Handle(BookViewingCommand command)
        {
            if (_context.Viewing.Any(x => x.BuyerId.Equals(command.BuyerId)
                                          && x.PropertyId.Equals(command.PropertyId)
                                          && DbFunctions.TruncateTime(x.ViewAt) ==
                                          DbFunctions.TruncateTime(command.ViewingDate)))
            {
                return false;
            }

            var vewing = new Models.Viewing
            {
                ViewAt = command.ViewingDate + command.ViewingTime,
                BuyerId = command.BuyerId,
                PropertyId = command.PropertyId
            };

            _context.Viewing.Add(vewing);

            _context.SaveChanges();

            return true;
        }
    }
}