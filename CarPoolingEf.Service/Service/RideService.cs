using CarPoolingEf.Model;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf.Services.Services
{
    public class RideServices: IRideServices
    {
        private CarPoolingEfContext Db { get; set; }

        private IBookingService BookingService { get; set; }

        public RideServices(CarPoolingEfContext context, IBookingService bookingService)
        {
            this.Db = context;

            this.BookingService = bookingService;
        }

        public bool CreateRide(Ride ride)
        {
            ride.RideDate = DateTime.Now;
            ride.Id = Guid.NewGuid().ToString();
            ride.Status = RideStatus.Pending;
            this.Db.Rides.Add(ride);

            return this.Db.SaveChanges() > 0;
        }

        public List<Ride> GetRidesOffers(SearchRideRequest booking)
        {
            int count = 0;
            List<Ride> rides = new List<Ride>();
            foreach (var ride in this.Db.Rides)
            {
                count++;
                var viaPoints = JsonConvert.DeserializeObject<List<Point>>(ride.ViaPoints);

                if (viaPoints.IndexOf(viaPoints.FirstOrDefault(a => a.City.Equals(booking.To, StringComparison.InvariantCultureIgnoreCase))) >
                    viaPoints.IndexOf(viaPoints.FirstOrDefault(a => a.City.Equals(booking.From, StringComparison.InvariantCultureIgnoreCase))) 
                    && ride.TravelDate == booking.TravelDate && ride.AvailableSeats > 0)
                {
                    rides.Add(ride);
                }
            }

            return rides;
        }

        public bool CancelRide(string rideId)
        {
            Ride ride = this.Db.Rides?.FirstOrDefault(a => a.Id == rideId);
            if (ride != null && this.BookingService.GetBookingsByRideId(rideId).Any())
            {
                ride.Status = RideStatus.Cancel;
                return true;
            }

            return false;
        }

        public bool SeatBookingResponse(string rideId)
        {
            Ride ride = GetRide(rideId);
            if (ride.AvailableSeats > 0)
            {
                ride.AvailableSeats--;
                return true;
            }

            return false;
        }

        public bool ModifyRide(Ride newRide, string id)
        {
            Ride oldRide = this.GetRide(id);
            if (oldRide != null)
            {
                oldRide.RideDate = newRide.RideDate;
                oldRide.From = newRide.From;
                oldRide.CarId = newRide.CarId;
                oldRide.To = newRide.To;
            }

            return true;
        }

        public Ride GetRide(string id)
        {
            return this.Db.Rides?.FirstOrDefault(ride => ride.Id == id);
        }

        public List<Ride> GetRides(string ownerId)
        {
            return this.Db.Rides?.Where(ride => ride.OwnerId == ownerId).ToList();
        }
    }
}
