using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewing.Builders;
using OrangeBricks.Web.Controllers.Viewing.Commands;
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

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult MyViewings(int propertyId)
        {
            try
            {
                var builder = new ViewingsOnPropertyViewModelBuilder(_context);
                var viewModel = builder.Build(propertyId);

                return View(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, GetType().Name, MethodBase.GetCurrentMethod().Name));
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult BookViewing(BookViewingCommand bookViewingCommand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var handler = new BookViewingCommandHandler(_context);

                    bookViewingCommand.BuyerId = User.Identity.GetUserId();

                    if (!handler.Handle(bookViewingCommand))
                    {
                        ModelState.AddModelError("ViewingDate", @"You can book only 1 viewing per day on this property!");
                        var builder = new AvailableViewingsViewModelBuilder(_context);
                        var viewModel = builder.Build(bookViewingCommand.PropertyId, bookViewingCommand.ViewingDate);
                        viewModel.ViewingTime = bookViewingCommand.ViewingTime;
                        return View(viewModel);
                    }

                    return RedirectToAction("Index", "Property");
                }
                catch (Exception e)
                {
                    return View("Error", new HandleErrorInfo(e, GetType().Name, MethodBase.GetCurrentMethod().Name));
                }
            }
            return View();
        }
    }
}