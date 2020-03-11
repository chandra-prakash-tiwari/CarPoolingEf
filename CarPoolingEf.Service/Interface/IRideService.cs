using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface IRideService
    {
        bool CreateRide(Models.Client.Ride ride);

        List<Models.Client.Ride> GetRidesOffers(Models.Client.SearchRideRequest booking);

        bool CancelRide(string rideId);

        bool SeatBookingResponse(string rideId);

        bool ModifyRide(Models.Client.Ride newRide, string id);

        Models.Client.Ride GetRide(string id);

        List<Models.Client.Ride> GetRides(string ownerId);
    }
}
