using System;
using System.Collections.Generic;
using System.Linq;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using CarPoolingEf.Services.Services;

namespace CarPoolingEf
{
    class Program
    {
        static void Main()
        {
            Console.Write(Constant.MainMenuOptions);
            MainMenu option = (MainMenu)Helper.ValidInteger();
            CarPoolingEfContext context = new CarPoolingEfContext();
            IUserServices userServices = new UserServices(context);

            switch (option)
            {
                case MainMenu.Login:
                    try
                    {
                        User user = userServices.Authentication(UserInput.GetCredential());
                        if (user != null)
                        {
                            AppServices.Initialize(user.Id, userServices, context);
                            Menu menu = new Menu();
                            menu.UserMainMenu();
                        }
                        else
                        {
                            Console.WriteLine(Constant.InvalidCredentials);
                        }
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex);
                    }

                    break;

                case MainMenu.Signup:
                    userServices.AddNewUser(UserInput.NewUser(userServices));
                    Main();

                    break;

                case MainMenu.Exit:
                    Environment.Exit(0);

                    break;
            }
            Main();
        }
    }

    public class Menu
    {
        public void UserMainMenu()
        {
            Console.Write(Constant.UserMainMenuOptions);
            HomeMenu option = (HomeMenu)Helper.ValidInteger();
            var user = AppServices.UserServices.GetUser(AppServices.CurrentUser.Id);
            if (user != null)
            {
                switch (option)
                {
                    case HomeMenu.CreateRide:
                        var cars = AppServices.CarServices.GetCarsByUser(AppServices.CurrentUser.Id);
                        if (cars != null && cars.Any())
                        {
                            foreach (var car in cars)
                            {
                                Console.WriteLine((cars.IndexOf(car) + 1) + ". " + car.Model + " " + car.Number + "\n");
                            }
                            Console.WriteLine("Select one car for create ride or for returing mainmenu press 0");
                            while (true)
                            {
                                int choice = Helper.ValidInteger();
                                if (choice <= cars.Count && choice != 0)
                                {
                                    var ride = UserInput.GetRideDetails();
                                    ride.CarId = cars[choice - 1].Id;
                                    ride.OwnerId = AppServices.CurrentUser.Id;

                                    Car carRecord = AppServices.CarServices.GetCar(ride.CarId);
                                    while (true)
                                    {
                                        Console.Write(Constant.AvailableSeats);
                                        ride.AvailableSeats = Helper.ValidInteger();
                                        if (carRecord.NoofSeat >= ride.AvailableSeats)
                                            break;
                                    }
                                    AppServices.RideServices.CreateRide(ride);
                                    break;
                                }
                                else if (choice == 0)
                                    break;
                                else
                                    Console.WriteLine("Enter correct option");
                            }
                        }
                        else
                        {
                            Console.WriteLine(Constant.NoCarsAdded);
                            if (AppServices.CarServices.AddNewCar(UserInput.GetCarDetails(), AppServices.CurrentUser.Id))
                                Console.WriteLine(Constant.AllowRide);
                        }
                        Console.ReadLine();
                        UserMainMenu();

                        break;

                    case HomeMenu.BookARide:
                        SearchRideRequest bookingRequest = UserInput.GetBooking();
                        List<Ride> rides = AppServices.RideServices.GetRidesOffers(bookingRequest);
                        if (rides != null && rides.Count > 0)
                        {
                            foreach (var ride in rides)
                            {
                                Console.Write(rides.IndexOf(ride) + 1);
                                Display.OfferRide(ride);
                                Display.CarDetail(AppServices.CarServices.GetCar(ride.CarId));
                            }
                            Console.WriteLine(Constant.RideSelection);

                            while (true)
                            {
                                int choice = Helper.ValidInteger();
                                if (choice <= rides.Count && choice != 0)
                                {
                                    Booking booking = new Booking
                                    {
                                        From = bookingRequest.From,
                                        To = bookingRequest.To,
                                        TravelDate = bookingRequest.TravelDate,
                                        Status = Models.BookingStatus.Pending,
                                        BookerId= AppServices.CurrentUser.Id
                                    };
                                    if (AppServices.BookingService.CreateBooking(booking, rides[choice - 1].Id))
                                        Console.WriteLine(Constant.RequestSentToOwner);
                                    else
                                        Console.WriteLine(Constant.InvalidBookingRequest);

                                    break;
                                }
                                else if (choice == 0)
                                {
                                    Console.WriteLine("Ok select another time");
                                    break;
                                }
                                else
                                    Console.WriteLine(Constant.CorrectSelection);
                            }
                        }
                        else
                        {
                            Console.WriteLine(Constant.NoRequests);
                        }

                        Console.ReadKey();
                        UserMainMenu();

                        break;

                    case HomeMenu.ViewStatus:
                        BookingStatus();
                        UserMainMenu();

                        break;

                    case HomeMenu.AddNewCar:
                        if (AppServices.CarServices.AddNewCar(UserInput.GetCarDetails(), AppServices.CurrentUser.Id))
                            Console.Write("Car added");
                        else
                            Console.WriteLine("Sorry car not added right now");

                        UserMainMenu();

                        break;

                    case HomeMenu.ModifyRide:
                        rides = AppServices.RideServices.GetRides(AppServices.CurrentUser.Id);
                        foreach (var ride in rides)
                        {
                            Console.Write(rides.IndexOf(ride));
                            Display.OfferRide(ride);
                        }
                        Console.Write("Select ride offer or for exit press 0");

                        while (true)
                        {
                            int choice = Helper.ValidInteger();
                            if (choice <= rides.Count && choice != 0)
                            {
                                Display.OfferRide(rides[choice - 1]);
                                Ride newRide = UserInput.GetRideDetails();
                                if (UserInput.Confirmation() == ConfirmationResponse.Yes && AppServices.BookingService.GetBookingsByRideId(AppServices.CurrentUser.Id).Count == 0)
                                {
                                    AppServices.RideServices.ModifyRide(newRide, rides[choice - 1].Id);
                                    break;
                                }
                            }
                            else if (choice == 0)
                                break;
                        }

                        break;

                    case HomeMenu.DeleteRide:
                        rides = AppServices.RideServices.GetRides(AppServices.CurrentUser.Id);
                        foreach (var ride in rides)
                        {
                            Console.Write(rides.IndexOf(ride) + 1);
                            Display.OfferRide(ride);
                        }
                        while (true)
                        {
                            int choice = Helper.ValidInteger();
                            Display.OfferRide(rides[choice - 1]);
                            if (choice <= rides.Count && choice != 0)
                            {
                                Console.WriteLine(Constant.Confirmation);
                                if (UserInput.Confirmation() == ConfirmationResponse.Yes)
                                {
                                    AppServices.RideServices.CancelRide(rides[choice - 1].Id);
                                }
                            }
                            else if (choice == 0)
                                break;
                        }
                        Console.ReadKey();
                        UserMainMenu();

                        break;

                    case HomeMenu.UpdateAccountDetail:
                        if (!AppServices.UserServices.UpdateUser(UserInput.NewUser(AppServices.UserServices), AppServices.CurrentUser.Id))
                        {
                            Console.WriteLine("Updatation Done");
                        }

                        break;

                    case HomeMenu.DeleteUserAccount:
                        Console.WriteLine(Constant.Confirmation);
                        if (UserInput.Confirmation() == ConfirmationResponse.Yes)
                        {
                            if (AppServices.UserServices.DeleteUser(AppServices.CurrentUser.Id))
                            {
                                Console.WriteLine(Constant.DeleteAccoutResponse);
                            }
                        }

                        break;

                    case HomeMenu.SignOut:

                        break;

                    case HomeMenu.Exit:
                        Environment.Exit(0);

                        break;
                }

            }
        }

        public void BookingStatus()
        {
            Console.Write(Constant.RequestStatusOptions);
            BookingStatusMenu statusOption = (BookingStatusMenu)Helper.ValidInteger();
            switch (statusOption)
            {
                case BookingStatusMenu.RideOffer:

                    List<Ride> rides = AppServices.RideServices.GetRides(AppServices.CurrentUser.Id);
                    foreach (var ride in rides)
                    {
                        var pendingBookings = AppServices.BookingService.GetAllPendingBookings(ride.Id);

                        foreach (var pendingBooking in pendingBookings)
                        {
                            Console.Write(pendingBookings.IndexOf(pendingBooking) + 1);
                            Display.BookingRequest(pendingBooking);
                            Console.WriteLine(" ");
                        }
                    }

                    foreach (var ride in rides)
                    {
                        var pendingBookings = AppServices.BookingService.GetAllPendingBookings(ride.Id);
                        foreach (var pendingBooking in pendingBookings)
                        {
                            Display.BookingRequest(pendingBooking);

                            BookingStatus status = UserInput.BookingChoice();
                            if (AppServices.RideServices.SeatBookingResponse(pendingBooking.RideId) && status == Models.BookingStatus.Confirm)
                            {
                                AppServices.BookingService.BookingResponse(pendingBooking.Id, Models.BookingStatus.Confirm);
                            }
                            else if (status == Models.BookingStatus.Pending) { }
                            else
                            {
                                AppServices.BookingService.BookingResponse(pendingBooking.Id, Models.BookingStatus.Rejected);
                            }
                        }
                    }
                    if (rides.Count < 1)
                    {
                        Console.WriteLine(Constant.NoRequests);
                    }

                    Console.ReadKey();
                    UserMainMenu();

                    break;

                case BookingStatusMenu.RideRequest:
                    List<Booking> bookings = AppServices.BookingService.BookingsStatus(AppServices.CurrentUser.Id);

                    foreach (var offer in bookings)
                    {
                        Console.Write(bookings.IndexOf(offer) + 1);
                        Display.BookingRequest(offer);
                    }
                    Console.Write("Select any of the booking or for exit press 0");

                    while (true)
                    {
                        int choice = Helper.ValidInteger();
                        if (choice <= bookings.Count && choice != 0)
                        {
                            switch (bookings[choice - 1].Status)
                            {
                                case Models.BookingStatus.Confirm:
                                    Console.WriteLine(Constant.ConfirmedBooking);

                                    break;

                                case Models.BookingStatus.Rejected:
                                    Console.WriteLine(Constant.RejectedBooking);

                                    break;

                                case Models.BookingStatus.Pending:
                                    Console.WriteLine(Constant.WaitingBooking);

                                    break;
                            }
                            break;
                        }
                        else if (choice == 0)
                            break;
                    }

                    if (bookings.Count < 1)
                    {
                        Console.Write(Constant.NoRideOfferCreated);
                    }

                    Console.ReadKey();
                    UserMainMenu();

                    break;

                case BookingStatusMenu.RiderDetail:
                    rides = AppServices.RideServices.GetRides(AppServices.CurrentUser.Id);
                    foreach (var ride in rides)
                    {
                        int travellerCount = AppServices.BookingService.GetBookingsByRideId(ride.Id).Count;
                        Console.WriteLine(Constant.NoOfBookedSeats + travellerCount);
                        Display.OfferRide(ride);
                    }
                    Console.ReadKey();
                    UserMainMenu();

                    break;

                case BookingStatusMenu.SignOut:

                    break;

                case BookingStatusMenu.Exit:
                    Environment.Exit(0);

                    break;
            }
        }
    }
}
