using System.Collections.Generic;

namespace CarPoolingEf.Models
{
    public class Car
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public string Model { get; set; }

        public int NoofSeat { get; set; }

        public string OwnerId { get; set; }
        
        public virtual User Owner { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
