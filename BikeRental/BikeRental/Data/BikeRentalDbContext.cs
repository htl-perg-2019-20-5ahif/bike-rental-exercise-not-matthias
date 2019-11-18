using BikeRental.Model;
using Microsoft.EntityFrameworkCore;

namespace web
{
    public class BikeRentalDbContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public BikeRentalDbContext(DbContextOptions<BikeRentalDbContext> options) : base(options)
        { }
    }
}
