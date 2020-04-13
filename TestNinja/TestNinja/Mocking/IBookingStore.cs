using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingStore
    {
        IQueryable<Booking> GetActiveBookings(int excludedBooking);
    }
}