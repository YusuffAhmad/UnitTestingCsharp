using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SoftwareTesting.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingExist(IBookingRepository _bookingRepository, Booking booking)
        {
            if (booking.Status == "cancelled")
                return string.Empty;

            var bookings = _bookingRepository.GetAvailableBookingsExcept(booking.Id);

            var overlappingBooking = 
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && b.ArrivalDate < b.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
    public class Booking
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}