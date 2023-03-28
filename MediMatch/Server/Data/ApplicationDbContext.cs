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
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Bill> Bills => Set<Bill>();
          
    }
}