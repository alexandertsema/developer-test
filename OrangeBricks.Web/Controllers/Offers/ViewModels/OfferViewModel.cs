using System;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class OfferViewModel
    {
        public int Id;
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPending { get; set; }
        public string Status { get; set; }
    }
}