using CarPoolingEf.Models.Client;
using CarPoolingEf.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingEf
{
    public class UserInput
    {
        public static User NewUser(IUserService userService)
        {
            User user = new User();
            while (true)
            {
                Console.Write(Constant.UserId);
                user.UserName = Console.ReadLine();

                if (!userService.CheckUserName(user.UserName))
                    break;
                else
                    Console.WriteLine(Constant.UserNameNotAvailable);
            }
            

            Console.Write(Constant.Password);
            user.Password = Helper.ValidString();

            Console.Write(Constant.Name);
            user.Name = Helper.ValidString();

            Console.Write(Constant.MobileNumber);
            user.Mobile = Helper.ValidString();

            Console.Write(Constant.Email);
            user.Email = Helper.GetValidEmail();

            Console.Write(Constant.Address);
            user.Address = Helper.ValidString();

            Console.Write(Constant.DrivingLicence);
            user.DrivingLicence = Console.ReadLine();

            return user;
        }

        public static Login GetCredential()
        {
            Login login = new Login();

            Console.Write(Constant.UserId);
            login.UserName = Helper.ValidString();

            Console.Write(Constant.Password);
            login.Password = Helper.ValidString();

            return login;
        }

        public static Ride GetRideDetails()
        {
            Ride ride = new Ride();

            Console.Write(Constant.TravellingDate);
            ride.TravelDate = Helper.ValidDate();

            while (true)
            {
                Console.Write(Constant.From);
                ride.From = Helper.GetValidCity().ToUpper();

                Console.Write(Constant.To);
                ride.To = Helper.GetValidCity().ToUpper();
                List<Point> viaPoints = new List<Point>();

                int fromIndex = CitiesInfo.Cities.IndexOf(CitiesInfo.Cities.FirstOrDefault(a => a.City.Equals(ride.From, StringComparison.InvariantCultureIgnoreCase)));
                int toIndex = CitiesInfo.Cities.IndexOf(CitiesInfo.Cities.FirstOrDefault(a => a.City.Equals(ride.To, StringComparison.InvariantCultureIgnoreCase)));

                if (toIndex > fromIndex)
                {
                    for(int count = fromIndex; count <= toIndex; count++)
                    {
                        viaPoints.Add(new Point {City= CitiesInfo.Cities[count].City } );
                    }

                    ride.ViaPoints = JsonConvert.SerializeObject(viaPoints);
                    break;
                }
                else
                {
                    Console.WriteLine(Constant.NoRouteFound);
                }
            }

            Console.WriteLine(Constant.Distance + ride.TotalDistance);

            Console.Write(Constant.TravellingRate);
            ride.RatePerKM = Helper.GetValidFloat();

            return ride;
        }

        public static Car GetCarDetails()
        {
            Car car = new Car();

            Console.WriteLine(Constant.CarDetails);
            Console.Write(Constant.CarNumber);
            car.Number = Helper.ValidString();

            Console.Write(Constant.CarModel);
            car.Model = Helper.ValidString();

            Console.Write(Constant.CarMaxSeats);
            car.NoofSeat = Helper.ValidInteger();

            return car;
        }

        public static SearchRideRequest GetBooking()
        {
            SearchRideRequest booking = new SearchRideRequest();

            Console.Write(Constant.TravellingDate);
            booking.TravelDate = Helper.ValidDate();

            Console.Write(Constant.From);
            booking.From = Helper.GetValidCity();
            float TravellingDistance = 0;

            while (true)
            {
                Console.Write(Constant.To);
                booking.To = Helper.GetValidCity();

                if (booking.From != booking.To)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Constant.InValidInput);
                }
            }

            Console.WriteLine(Constant.Distance + TravellingDistance);
            Console.ReadKey();
            return booking;
        }

        public static ConfirmationResponse Confirmation()
        {
            Console.WriteLine(Constant.Confirmation);

            ConfirmationResponse option = (ConfirmationResponse)Helper.ValidInteger();
            switch (option)
            {
                case ConfirmationResponse.Yes:
                case ConfirmationResponse.No:
                    return option;

                default:
                    Console.WriteLine(Constant.InValidInput);
                    option = Confirmation();
                    return option;
            }
        }

        public static ConfirmationResponse Response()
        {
            Console.WriteLine(Constant.Confirmation);

            ConfirmationResponse option = (ConfirmationResponse)Helper.ValidInteger();
            switch (option)
            {
                case ConfirmationResponse.Yes:
                case ConfirmationResponse.No:
                    return option;

                default:
                    Console.WriteLine(Constant.InValidInput);
                    option = Confirmation();
                    return option;
            }
        }

        public static BookingStatus BookingChoice()
        {
            Console.WriteLine(Constant.RideResponseOptions);

            BookingStatus option = (BookingStatus)Helper.ValidInteger();

            switch (option)
            {
                case BookingStatus.Confirm:
                case BookingStatus.Rejected:
                case BookingStatus.Pending:
                    return option;

                default:
                    Console.WriteLine(Constant.InValidInput);
                    option = BookingChoice();
                    return option;
            }
        }
    }
}
