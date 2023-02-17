using Microsoft.EntityFrameworkCore;

namespace SoftwareTesting.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetAvailableBookingsExcept(int? id = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetAvailableBookingsExcept(int? id = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "cancelled");
            if (id.HasValue)
                bookings = bookings.Where(b => b.Id != id.Value);

            return bookings;
        }
    }
}