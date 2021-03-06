﻿using System.Collections.Generic;

namespace CarPoolingEf.Models.Data
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string DrivingLicence { get; set; }

        public float Rating { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
