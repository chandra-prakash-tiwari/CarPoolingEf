using System.Collections.Generic;

namespace CarPoolingEf.Models.Client
{
    public class Place
    {
        public string City { get; set; }

        public int Pincode { get; set; }
    }


    public static class CitiesInfo
    {
        public static List<Place> Cities { get; set; }

        static CitiesInfo()
        {
            Cities = new List<Place>()
            {
                new Place()
                {
                    City="HYD",
                    Pincode=12345
                },
                new Place()
                {
                    City="Bhilai",
                    Pincode=490023
                },
                new Place()
                {
                    City="Ranchi",
                    Pincode=834001
                },
                new Place()
                {
                    City="Delhi",
                    Pincode=000001
                },
                new Place()
                {
                    City="Gaya",
                    Pincode=12345
                },
                new Place()
                {
                    City="Bokaro",
                    Pincode=834001
                },
                new Place()
                {
                    City="Jamshedpur",
                    Pincode=000001
                },
                new Place()
                {
                    City="Ambikapur",
                    Pincode=12345
                },
                new Place()
                {
                    City="Bero",
                    Pincode=490023
                },
                new Place()
                {
                    City="Manendragarh",
                    Pincode=834001
                },
                new Place()
                {
                    City="Raipur",
                    Pincode=000001
                },
                new Place()
                {
                    City="Mumbai",
                    Pincode=490023
                }
            };
        }
    }
}
