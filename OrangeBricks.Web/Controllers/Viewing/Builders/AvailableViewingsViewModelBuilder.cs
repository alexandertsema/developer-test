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
            var availability = _context.Properties
                .Include(x => x.Availability)
                .SingleOrDefault(x => x.Id == propertyId)
                ?.Availability;

            if (availability == null)
            {
                return new AvailableViewingsViewModel
                {
                    ViewingDate = date,
                    Times = new List<SelectListItem>(),
                    PropertyId = propertyId
                };
            }
            if (date < availability.StartDate)
            {
                return new AvailableViewingsViewModel
                {
                    ViewingDate = date,
                    Times = new List<SelectListItem>(),
                    PropertyId = propertyId,
                    ViewingDuration = availability.ViewingDuration
                };
            }

            var viewingsOnDate = GetViewingsOfDate(date, propertyId);

            var schedule = GenerateSchedule(availability.StartTime, availability.EndTime, availability.ViewingDuration, date, viewingsOnDate.ToList());

            return new AvailableViewingsViewModel
            {
                StartViewingDate = availability.StartDate.ToString("yyyy-MM-dd"),
                ViewingDate = date,
                Times = schedule,
                PropertyId = propertyId,
                ViewingDuration = availability.ViewingDuration
            };
        }

        public IEnumerable<Models.Viewing> GetViewingsOfDate(DateTime date, int propertyId)
        {
            return _context.Viewing.Where(x => x.PropertyId == propertyId
                                               && DbFunctions.TruncateTime(x.ViewAt) == DbFunctions.TruncateTime(date))
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