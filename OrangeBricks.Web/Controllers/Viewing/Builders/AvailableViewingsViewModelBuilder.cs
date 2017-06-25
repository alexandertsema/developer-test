using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OrangeBricks.Web.Controllers.Viewing.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Builders
{
    public class AvailableViewingsViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public AvailableViewingsViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public AvailableViewingsViewModel Build(int propertyId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Viewing> GetViewingsOfDate(DateTime date, int propertyId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GenerateSchedule(TimeSpan startTime, TimeSpan endTime, int viewingDuration, DateTime date, IList<Models.Viewing> viewingsOnDate)
        {
            throw new NotImplementedException();
        }
    }
}