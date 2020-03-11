using System.Collections.Generic;

namespace CarPoolingEf.Models.Client
{
    public class Car
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public string Model { get; set; }

        public int NoofSeat { get; set; }

        public string OwnerId { get; set; }
    }
}
