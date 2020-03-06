using CarPoolingEf;
using CarPoolingEf.Models;
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

        public bool CreateBooking(Booking booking, string rideId)
        {

            booking.Id = Guid.NewGuid().ToString();
            booking.RideId = rideId;
            this.Db.Bookings.Add(booking);

            return this.Db.SaveChanges() > 0;
        }

        public bool CancelRideRequest(string id)
        {
            Booking booking = this.Db.Bookings?.FirstOrDefault(a => a.Id == id);
            if (booking != null && booking.Status == BookingStatus.Pending)
            {
                booking.Status = BookingStatus.Cancel;
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public List<Booking> BookingsStatus(string id)
        {
            return this.Db.Bookings?.Where(a => a.BookerId == id && a.Status != BookingStatus.Completed).ToList();
        }

        public bool BookingResponse(string id, BookingStatus status)
        {
            Booking bookingResponse = this.Db.Bookings?.FirstOrDefault(booking => booking.Id == id);
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

        public List<Booking> GetUserBookings(string userId)
        {
            return this.Db.Bookings?.Where(booking => booking.BookerId == userId).ToList();
        }

        public List<Booking> GetBookingsByRideId(string rideId)
        {
            return this.Db.Bookings?.Where(booking => booking.RideId == rideId).ToList();
        }

        public List<Booking> GetAllPendingBookings(string rideId)
        {
            return this.Db.Bookings?.Where(booking => booking.Status == BookingStatus.Pending && booking.RideId == rideId).ToList();
        }

        public Booking GetBooking(string bookingId)
        {
            return this.Db.Bookings?.FirstOrDefault(booking => booking.Id == bookingId);
        }
    }
}
