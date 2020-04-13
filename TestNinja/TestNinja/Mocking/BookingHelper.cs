using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class BookingHelper
    {
        IBookingStore _bookingStore;

        public BookingHelper(IBookingStore bookingStore)
        {
            _bookingStore = bookingStore ?? new BookingStore();
        }

        public string OverlappingBookingsExist(Booking booking)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = _bookingStore.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        !(booking.DepartureDate < b.ArrivalDate ||
                        b.DepartureDate <= booking.ArrivalDate));

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}