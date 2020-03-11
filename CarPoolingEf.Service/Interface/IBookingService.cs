using System.Collections.Generic;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IBookingService
    {
        bool CreateBooking(Models.Client.Booking booking, string rideId);

        bool CancelRideRequest(string id);

        List<Models.Client.Booking> BookingsStatus(string id);

        bool BookingResponse(string id, Models.Client.BookingStatus status);

        string GetRequester(string id);

        List<Models.Client.Booking> GetUserBookings(string userId);

        List<Models.Client.Booking> GetBookingsByRideId(string rideId);

        List<Models.Client.Booking> GetAllPendingBookings(string rideId);

        Models.Client.Booking GetBooking(string bookingId);
    }
}
