/* 
--Extract the repository
--Confirm the methid invocation from the repository
--create get overllaping method
--when the booking status is cancelled return empty string
*/
using System.Net;
using Moq;
using SoftwareTesting.Mocking;

namespace SoftwareTesting.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperOverlappingBookingExistTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existingBooking;
        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 10),
                DepartureDate = DepartOn(2017, 1, 14),
                Reference = "a"
            };

            _bookingRepository.Setup(r => r.GetAvailableBookingsExcept(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void WhenBookingStatusIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object, new Booking { Status = "cancelled" });

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            });

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleAnExistingBooking_ReturnExistngBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfetrAnExistingBooking_ReturnExistngBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistngBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
            });

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2),
            });

            Assert.That(result, Is.Empty);
        }

          [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingExist(_bookingRepository.Object,
            new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate),
                Status = "Cancelled"
            });

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}