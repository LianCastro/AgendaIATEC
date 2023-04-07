using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEvents>(x => x.HasKey(ue => new {ue.UserId, ue.EventId }));

            builder.Entity<UserEvents>()
                .HasOne(u => u.User)
                .WithMany(e => e.Events)
                .HasForeignKey(ue => ue.UserId);

            builder.Entity<UserEvents>()
                .HasOne(u => u.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ue => ue.EventId);
        }
    }
}
