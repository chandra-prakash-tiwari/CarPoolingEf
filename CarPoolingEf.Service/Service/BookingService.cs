using AutoMapper;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class BookingService : IBookingService
    {
        private CarPoolingEfContext Db { get; set; }

        public BookingService(CarPoolingEfContext context)
        {
            this.Db = context;
        }

        public bool CreateBooking(Models.Client.Booking clientBooking, string rideId)
        {
            clientBooking.Id = Guid.NewGuid().ToString();
            clientBooking.RideId = rideId;
            this.Db.Bookings.Add(AppService.Mapping<Models.Client.Booking, Models.Data.Booking>().Map<Models.Client.Booking, Models.Data.Booking>(clientBooking));

            return this.Db.SaveChanges() > 0;
        }

        public bool CancelRideRequest(string id)
        {
            Models.Data.Booking booking = this.Db.Bookings?.FirstOrDefault(a => a.Id == id);
            if (booking != null && booking.Status == Models.Client.BookingStatus.Pending)
            {
                booking.Status = Models.Client.BookingStatus.Cancel;
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public List<Models.Client.Booking> BookingsStatus(string id)
        {
            var bookings = AppService.Mapping<Models.Data.Booking, Models.Client.Booking>().Map<List<Models.Data.Booking>, List<Models.Client.Booking>>(this.Db.Bookings?.Where(a => a.BookerId == id && a.Status != Models.Client.BookingStatus.Completed).ToList());

            return bookings;
        }

        public bool BookingResponse(string id, Models.Client.BookingStatus status)
        {
            Models.Data.Booking bookingResponse = this.Db.Bookings?.FirstOrDefault(booking => booking.Id == id);
            if (bookingResponse == null)
            {
                return false;
            }

            bookingResponse.Status = status;

            return this.Db.SaveChanges() > 0;
        }

        public string GetRequester(string id)
        {
            return this.Db.Bookings?.FirstOrDefault(a => a.Id == id).RideId;
        }

        public List<Models.Client.Booking> GetUserBookings(string userId)
        { 
            return AppService.Mapping<Models.Client.Booking, Models.Data.Booking>().Map<List<Models.Data.Booking>, List<Models.Client.Booking>>(this.Db.Bookings?.Where(booking => booking.BookerId == userId).ToList());
        }

        public List<Models.Client.Booking> GetBookingsByRideId(string rideId)
        {
            return AppService.Mapping<Models.Data.Booking, Models.Client.Booking>().Map<List<Models.Data.Booking>, List<Models.Client.Booking>>(this.Db.Bookings?.Where(booking => booking.RideId == rideId).ToList());
        }

        public List<Models.Client.Booking> GetAllPendingBookings(string rideId)
        {
            return AppService.Mapping<Models.Data.Booking, Models.Client.Booking>().Map<List<Models.Data.Booking>, List<Models.Client.Booking>>(this.Db.Bookings?.Where(booking => booking.Status == Models.Client.BookingStatus.Pending && booking.RideId == rideId).ToList());
        }

        public Models.Client.Booking GetBooking(string bookingId)
        {
            return AppService.Mapping<Models.Data.Booking, Models.Client.Booking>().Map<Models.Data.Booking, Models.Client.Booking>(this.Db.Bookings?.FirstOrDefault(bkg => bkg.Id == bookingId));
        }
    }
}
