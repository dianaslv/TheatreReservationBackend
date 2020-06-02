using Microsoft.EntityFrameworkCore;
using Theatre.Web.Core.Enums;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Spectator> Spectators { get; set; }
        public DbSet<TheatrePlay> TheatrePlays { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelsRelations(modelBuilder);
            SetDescriminators(modelBuilder);
        }
        
        private static void SetModelsRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.TheatrePlay)
                .WithMany(t => t.Reservations);
            
            modelBuilder.Entity<Reservation>()
                .HasOne(t => t.Spectator)
                .WithMany(t => t.Reservations);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithMany(t => t.Tickets);
        }
        
        private static void SetDescriminators(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<UserType>("Type")
                .HasValue<Administrator>(UserType.Administrator)
                .HasValue<Spectator>(UserType.Spectator);
        }
    }
}