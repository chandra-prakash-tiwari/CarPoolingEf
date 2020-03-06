using CarPoolingEf.Model;
using CarPoolingEf.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingEf
{
    public class CarPoolingEfContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SANDEEPINT-PC\MSSQLSERVER1;Database=CarPoolDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
             .HasOne<User>(s => s.Booker)
             .WithMany(ad => ad.Bookings)
             .HasForeignKey(s => s.BookerId);

            modelBuilder.Entity<Ride>()
             .HasOne<User>(s => s.Owner)
             .WithMany(ad => ad.Rides)
             .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<Car>()
             .HasOne<User>(s => s.Owner)
             .WithMany(ad => ad.Cars)
             .HasForeignKey(s => s.OwnerId);

            modelBuilder.Entity<Booking>()
             .HasOne<Ride>(s => s.Ride)
             .WithMany(ad => ad.Bookings)
             .HasForeignKey(s => s.RideId);

            modelBuilder.Entity<Ride>()
             .HasOne<Car>(s => s.Car)
             .WithMany(ad => ad.Rides)
             .HasForeignKey(s => s.CarId);

            modelBuilder.Entity<Ride>()
                .Property(c => c.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Booking>()
                .Property(c => c.Status)
                .HasConversion<int>();
        }
    }
}
