using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IRideServices
    {
        bool CreateRide(Ride ride);

        List<Ride> GetRidesOffers(SearchRideRequest booking);

        bool CancelRide(string rideId);

        bool SeatBookingResponse(string rideId);

        bool ModifyRide(Ride newRide, string id);

        Ride GetRide(string id);

        List<Ride> GetRides(string ownerId);
    }
}
