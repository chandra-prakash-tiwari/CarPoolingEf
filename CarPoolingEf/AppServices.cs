using CarPoolingEf;
using CarPoolingEf.Model;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using CarPoolingEf.Services.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf
{
    public class AppServices
    {
        public static User CurrentUser { get; set; }

        public static IBookingService BookingService { get; set; }

        public static ICarServices CarServices { get; set; }

        public static IRideServices RideServices { get; set; }

        public static IUserServices UserServices { get; set; }

        public static void Initialize(string userId, IUserServices userServices, CarPoolingEfContext context)
        {
            CurrentUser = userServices.GetUser(userId);

            UserServices = userServices;

            BookingService = new BookingService(context);

            CarServices = new CarServices(context);

            RideServices = new RideServices(context, BookingService);
        }
    }
}
