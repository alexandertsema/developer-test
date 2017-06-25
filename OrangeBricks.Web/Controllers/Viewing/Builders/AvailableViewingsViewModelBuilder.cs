using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var lowerBoundDate = date.Date;
            var upperBoundDate = lowerBoundDate.AddDays(1).Date;
            return _context.Viewing.Where(x => x.PropertyId == propertyId
                                               && x.ViewAt >= lowerBoundDate
                                               && x.ViewAt < upperBoundDate)
                                                .ToList();
        }

        public IEnumerable<SelectListItem> GenerateSchedule(TimeSpan startTime, TimeSpan endTime, int viewingDuration, DateTime date, IList<Models.Viewing> viewingsOnDate)
        {
            var schedule = new List<SelectListItem>();

            for (var i = startTime; i < endTime;
                i = i.Add(TimeSpan.FromMinutes(viewingDuration)))
            {
                var dateToCkeck = new DateTime(date.Year, date.Month, date.Day, i.Hours, i.Minutes, 0);
                if (viewingsOnDate.All(x => x.ViewAt != dateToCkeck))
                    schedule.Add(new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(@"hh\:mm")
                    });
            }

            return schedule;
        }
    }
}