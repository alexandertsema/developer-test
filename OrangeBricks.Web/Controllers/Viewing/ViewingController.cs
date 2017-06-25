using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewing.Builders;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing
{
    [OrangeBricksAuthorize]
    public class ViewingController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingController(IOrangeBricksContext context)
        {
            _context = context;
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult BookViewing(int propertyId, DateTime date)
        {
            try
            {
                var builder = new AvailableViewingsViewModelBuilder(_context);
                var viewModel = builder.Build(propertyId, date);

                return View(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, GetType().Name, MethodBase.GetCurrentMethod().Name));
            }
        }
    }
}