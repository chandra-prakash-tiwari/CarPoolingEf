using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IBookingService
    {
        bool CreateBooking(Booking booking, string rideId);

        bool CancelRideRequest(string id);

        List<Booking> BookingsStatus(string id);

        bool BookingResponse(string id, BookingStatus status);

        string GetRequester(string id);

        List<Booking> GetUserBookings(string userId);

        List<Booking> GetBookingsByRideId(string rideId);

        List<Booking> GetAllPendingBookings(string rideId);

        Booking GetBooking(string bookingId);
    }
}
