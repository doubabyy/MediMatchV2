using Duende.IdentityServer.EntityFramework.Options;
using MediMatch.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MediMatch.Shared;

namespace MediMatch.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>().ToTable("Doctors");
            builder.Entity<Doctor>(e =>
            {
                e.HasKey(d => d.ApplicationUserId);
                e.Property(d => d.Description).IsRequired(true).HasColumnType<string>("nvarchar(400)");
                e.Property(d => d.Availability).IsRequired(true).HasColumnType<string>("nvarchar(400)");
                e.Property(d => d.Rates).IsRequired(true).HasColumnType<int>("int");
                e.Property(d => d.AcceptsInsurance).IsRequired(true).HasColumnType<bool>("bit");
            });
            builder.Entity<Doctor>()
                .HasOne(e => e.ApplicationUser)
                .WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            

            
            builder.Entity<Patient>().ToTable("Patients");
            builder.Entity<Patient>(e =>
            {
                e.HasKey(d => d.ApplicationUserId);
                e.Property(d => d.Gender).HasColumnType<string>("nvarchar(50)");
            });
            builder.Entity<Patient>()
                .HasOne(e => e.ApplicationUser)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Bill> Bills => Set<Bill>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Appointment> Appointments => Set<Appointment>();
    }
}