using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    class BookingHelperUnitTests
    {
        BookingHelper _bookingHelper;
        Mock<IBookingStore> _bookingStore;
        Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _bookingStore = new Mock<IBookingStore>();
            _bookingHelper = new BookingHelper(_bookingStore.Object);

            _existingBooking = new Booking
            {
                ArrivalDate = new DateTime(2020, 12, 25, 10, 0, 0),
                DepartureDate = new DateTime(2020, 12, 25, 11, 0, 0),
                Reference = "Ref1",
                Id = 2
            };

            _bookingStore.Setup(fr => fr.GetActiveBookings(1)).Returns(
                new List<Booking> {
                   _existingBooking
                }.AsQueryable()
            );
        }

        [Test]
        public void OverlappingBookingsExist_BookingCancelled_ReturnsEmprtString()
        {
            Booking booking = new Booking { Status = "Cancelled"};
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingIsNotCancelled_GetActiveBookingsIsCalled()
        {

            Booking booking = new Booking { Id = 1 };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            _bookingStore.Verify(fr => fr.GetActiveBookings(1));
        }

        [Test]
        public void OverlappingBookingsExist_BookingDoesntOverLapsBeforeExisting_ReturnsEmptyString()
        {
            Booking booking = new Booking {
                Id = 1,
                ArrivalDate = BeforeOf(_existingBooking.ArrivalDate, minutesToAdd: 20),
                DepartureDate = BeforeOf(_existingBooking.ArrivalDate, minutesToAdd: 10),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingDoesntOverLapsAfterExisting_ReturnsEmptyString()
        {
            Booking booking = new Booking
            {
                Id = 1,
                ArrivalDate = AfterOf(_existingBooking.DepartureDate, minutesToAdd: 10),
                DepartureDate = AfterOf(_existingBooking.DepartureDate, minutesToAdd: 20),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingArrivalOverLaps_ReturnsOverlappingBooking()
        {
            Booking booking = new Booking
            {
                Id = 1,
                ArrivalDate = BeforeOf(_existingBooking.DepartureDate),
                DepartureDate = AfterOf(_existingBooking.DepartureDate),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingDepartureOverLaps_ReturnsOverlappingBooking()
        {
            Booking booking = new Booking
            {
                Id = 1,
                ArrivalDate = BeforeOf(_existingBooking.ArrivalDate),
                DepartureDate = AfterOf(_existingBooking.ArrivalDate),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStayingOverlaps_ReturnsOverlappingBooking()
        {
            Booking booking = new Booking
            {
                Id = 1,
                ArrivalDate = BeforeOf(_existingBooking.ArrivalDate),
                DepartureDate = AfterOf(_existingBooking.DepartureDate),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStayinCompletelyInsideExistingBooking_ReturnsOverlappingBooking()
        {
            Booking booking = new Booking
            {
                Id = 1,
                ArrivalDate = AfterOf(_existingBooking.ArrivalDate),
                DepartureDate = BeforeOf(_existingBooking.DepartureDate),
            };
            var result = _bookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime AfterOf(DateTime dT, int minutesToAdd = 10)
        {
            return dT.AddMinutes(minutesToAdd);
        }

        private DateTime BeforeOf(DateTime dT, int minutesToAdd = 10)
        {
            return AfterOf(dT, -minutesToAdd);
        }
    }
}
