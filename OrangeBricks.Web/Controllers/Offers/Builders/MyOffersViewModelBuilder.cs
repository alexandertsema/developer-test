using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyOffersViewModel Build(string buyerId)
        {
            var offers = _context.Offers
                .Where(x => x.BuyerId.Equals(buyerId) && x.Status == OfferStatus.Accepted);

            return new MyOffersViewModel
            {
                Offers = offers.Select(x => new OfferViewModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    IsPending = x.Status == OfferStatus.Pending,
                    Status = x.Status.ToString(),
                    UptatedAt = x.UpdatedAt,
                    Property = x.Property
                })
            };
        }
    }
}