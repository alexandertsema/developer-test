using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Viewing.Builders;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;
using Assert = NUnit.Framework.Assert;

namespace OrangeBricks.Web.Tests.Controllers.Viewing.Builders
{
    [TestFixture]
    public class AvailableViewingsViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void GetViewingsOfDateShouldReturnAllViewwingsOfPropertyOfDateProvided()
        {
            // Arrange
            var builder = new AvailableViewingsViewModelBuilder(_context);

            var propertyId = 1;

            var viewings = new List<Models.Viewing>
            {
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 08:00:00.000"),
                    PropertyId = propertyId
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 10:00:00.000"),
                    PropertyId = propertyId
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 20:00:00.000"),
                    PropertyId = propertyId
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 00:00:00.000"),
                    PropertyId = propertyId
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-25 00:00:00.000"),
                    PropertyId = propertyId
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-23 23:59:00.000"),
                    PropertyId = propertyId
                }
            };

            var date = DateTime.Parse("2017-06-24 12:00:00.000");

            var mockSet = Substitute.For<IDbSet<Models.Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewing.Returns(mockSet);

            // Act
            var viewModel = builder.GetViewingsOfDate(date, propertyId);

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(4));
        }

        [Test]
        [TestCase(60, Result = 10)]
        [TestCase(30, Result = 22)]
        [TestCase(10, Result = 70)]
        [TestCase(15, Result = 46)]
        public int GenerateScheduleShouldReturnResultTimesOfDateProvided(int viewingDuration)
        {
            // Arrange
            var builder = new AvailableViewingsViewModelBuilder(_context);

            var startTime = TimeSpan.Parse("08:00:00");

            var endTime = TimeSpan.Parse("20:00:00");

            var viewings = new List<Models.Viewing>
            {
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 08:00:00.000")
                },
                new Models.Viewing
                {
                    ViewAt = DateTime.Parse("2017-06-24 10:00:00.000")
                }
            };

            var date = DateTime.Parse("2017-06-24");

            // Act
            var scheduleList = builder.GenerateSchedule(startTime, endTime, viewingDuration, date, viewings);

            // Assert
            return scheduleList.Count();
        }
    }
}
