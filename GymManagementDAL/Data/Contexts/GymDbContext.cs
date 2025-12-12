using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Contexts
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public GymDbContext(DbContextOptions<GymDbContext> options)
        : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Session)
            .WithMany(s => s.SessionMembers)
            .HasForeignKey(b => b.SessionId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>(au =>
            {
                au.Property(x => x.FirstName)
                .HasColumnType("varchar")
                    .HasMaxLength(50);

                au.Property(x => x.LastName)
                .HasColumnType("varchar")
                    .HasMaxLength(50);
            });
        }

        

        #region DbSets

        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<GymUser> GymUsers { get; set; }


        #endregion
    }
}
