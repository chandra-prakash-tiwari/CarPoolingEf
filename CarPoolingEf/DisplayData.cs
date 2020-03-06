using CarPoolingEf.Model;
using CarPoolingEf.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarPoolingEf
{
    public class Display
    {
        public static void OfferRide(Ride ride)
        {
            Console.WriteLine("  " + Constant.TravellingDate + ride.TravelDate);
            Console.WriteLine("  " + Constant.AvailableSeats + ride.AvailableSeats);
            var viaPoints = JsonConvert.DeserializeObject<List<Point>>(ride.ViaPoints);
            Console.WriteLine("Travelling Route");
            foreach (var viaPoint in viaPoints)
            {
                if(viaPoints.Count-1!=viaPoints.IndexOf(viaPoint))
                Console.Write("  " + viaPoint.City + "  ---");
                else
                {
                    Console.WriteLine(viaPoint.City);
                }

            }
            Console.WriteLine("  " + Constant.TravellingRate + ride.RatePerKM);
        }

        public static void CarDetail(Car car)
        {
            Console.WriteLine(Constant.CarDetails);
            Console.WriteLine("  " + Constant.CarNumber + car.Number);
            Console.WriteLine("  " + Constant.CarModel + car.Model);
            Console.WriteLine("  " + Constant.CarMaxSeats + car.NoofSeat);
        }

        public static void BookingRequest(Booking booking)
        {
            Console.WriteLine("  " + Constant.From + booking.From);
            Console.WriteLine("  " + Constant.To + booking.To);
            Console.WriteLine("  " + Constant.TravellingDate + booking.TravelDate);
        }
    }
}
