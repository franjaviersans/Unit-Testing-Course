using System.Linq;

namespace TestNinja.Mocking
{
    public class BookingStore : IBookingStore
    {
        public IQueryable<Booking> GetActiveBookings(int excludedBooking)
        {
            var unitOfWork = new UnitOfWork();
            return
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Id != excludedBooking && b.Status != "Cancelled");
        }
    }
}
