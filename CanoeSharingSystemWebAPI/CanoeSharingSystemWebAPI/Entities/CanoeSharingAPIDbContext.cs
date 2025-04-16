using Microsoft.EntityFrameworkCore;

namespace CanoeSharingSystemWebAPI.Entities
{
    public class CanoeSharingAPIDbContext: DbContext
    {
        public CanoeSharingAPIDbContext(DbContextOptions<CanoeSharingAPIDbContext> options): base(options) { }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<RentalStore> RentalStores { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Setting up relations for Booking entity

            modelBuilder.Entity<Booking>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Booking)
                .HasForeignKey(x => x.BookingId);

            //Setting up relations for Listing entity

            modelBuilder.Entity<Listing>()
                .HasMany(x => x.Bookings)
                .WithOne(x => x.Listing)
                .HasForeignKey(x => x.ListingId);

            //Setting up relations for RentalStore entity

            modelBuilder.Entity<RentalStore>()
                .HasMany(x => x.Listings)
                .WithOne(x => x.RentalStore)
                .HasForeignKey(x => x.RentalStoreId);

            //Setting up relations for user entity

            modelBuilder.Entity<User>()
                .HasMany(x => x.Bookings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Listings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
    }

