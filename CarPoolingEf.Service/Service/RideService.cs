using AutoMapper;
using CarPoolingEf.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class RideServices: IRideService
    {
        private CarPoolingEfContext Db { get; set; }

        private IBookingService BookingService { get; set; }

        public RideServices(CarPoolingEfContext context, IBookingService bookingService)
        {
            this.Db = context;

            this.BookingService = bookingService;
        }

        public bool CreateRide(Models.Client.Ride ride)
        {
            ride.RideDate = DateTime.Now;
            ride.Id = Guid.NewGuid().ToString();
            ride.Status = Models.Client.RideStatus.Pending;
            this.Db.Rides.Add(AppService.Mapping<Models.Client.Ride, Models.Data.Ride>().Map<Models.Client.Ride, Models.Data.Ride>(ride));

            return this.Db.SaveChanges() > 0;
        }

        public List<Models.Client.Ride> GetRidesOffers(Models.Client.SearchRideRequest booking)
        {
            int count = 0;
            List<Models.Client.Ride> rides = new List<Models.Client.Ride>();
            foreach (var ride in this.Db.Rides)
            {
                count++;
                var viaPoints = JsonConvert.DeserializeObject<List<Models.Client.Point>>(ride.ViaPoints);

                if (viaPoints.IndexOf(viaPoints.FirstOrDefault(a => a.City.Equals(booking.To, StringComparison.InvariantCultureIgnoreCase))) >
                    viaPoints.IndexOf(viaPoints.FirstOrDefault(a => a.City.Equals(booking.From, StringComparison.InvariantCultureIgnoreCase))) 
                    && ride.TravelDate == booking.TravelDate && ride.AvailableSeats > 0)
                {
                    rides.Add(AppService.Mapping<Models.Data.Ride, Models.Client.Ride>().Map<Models.Data.Ride,Models.Client.Ride>(ride));
                }
            }

            return rides;
        }

        public bool CancelRide(string rideId)
        {
            Models.Data.Ride ride = this.Db.Rides?.FirstOrDefault(a => a.Id == rideId);
            if (ride != null && this.BookingService.GetBookingsByRideId(rideId).Any())
            {
                ride.Status = Models.Client.RideStatus.Cancel;
                return true;
            }

            return false;
        }

        public bool SeatBookingResponse(string rideId)
        {
            Models.Client.Ride ride = GetRide(rideId);
            if (ride.AvailableSeats > 0)
            {
                ride.AvailableSeats--;
                return true;
            }

            return false;
        }

        public bool ModifyRide(Models.Client.Ride newRide, string id)
        {
            Models.Data.Ride oldRide = AppService.Mapping<Models.Client.Ride, Models.Data.Ride>().Map < Models.Client.Ride, Models.Data.Ride > (this.GetRide(id));
            if (oldRide != null)
            {
                oldRide.RideDate = newRide.RideDate;
                oldRide.From = newRide.From;
                oldRide.CarId = newRide.CarId;
                oldRide.To = newRide.To;
            }

            return true;
        }

        public Models.Client.Ride GetRide(string id)
        {
            return AppService.Mapping<Models.Data.Ride, Models.Client.Ride>().Map<Models.Data.Ride, Models.Client.Ride>(this.Db.Rides?.FirstOrDefault(ride => ride.Id == id));
        }

        public List<Models.Client.Ride> GetRides(string ownerId)
        
        {
            return AppService.Mapping<Models.Data.Ride, Models.Client.Ride>().Map<List<Models.Data.Ride>, List<Models.Client.Ride>>(this.Db.Rides?.Where(ride => ride.OwnerId == ownerId).ToList());
        }
    }
}
