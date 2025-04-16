using Microsoft.EntityFrameworkCore;
using CanoeSharin.API.Models;
using CanoeSharin.API.Models;
using System.Collections.Generic;

namespace CanoeSharin.API.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Canoe> Canoes => Set<Canoe>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<RentalStore> RentalStores { get; set; }

    }

}
