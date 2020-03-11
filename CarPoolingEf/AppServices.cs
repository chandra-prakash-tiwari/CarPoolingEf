
using CarPoolingEf.Services.Interfaces;
using CarPoolingEf.Services.Services;

namespace CarPoolingEf
{
    public class AppService
    {
        public static string CurrentUserId { get; set; }

        public static IBookingService BookingService { get; set; }

        public static ICarService CarServices { get; set; }

        public static IRideService RideServices { get; set; }

        public static IUserService UserService { get; set; }

        public static void Initialize(string userId, IUserService userService, CarPoolingEfContext context)
        {
            CurrentUserId = userId;

            UserService = userService;

            BookingService = new BookingService(context);

            CarServices = new CarServices(context);

            RideServices = new RideServices(context, BookingService);
        }
    }
}
