using CarPoolingEf.Models.Client;
using System;

namespace CarPoolingEf.Models.Data
{
    public class Booking
    {
        public string Id { get; set; }

        public string RideId { get; set; }

        public string BookerId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public float TravellingDistance { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime TravelDate { get; set; }

        public BookingStatus Status { get; set; }

        public virtual Ride Ride { get; set; }

        public virtual User Booker { get; set; }
    }
}
