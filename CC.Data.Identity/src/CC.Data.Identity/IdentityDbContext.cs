using System.Text;
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using CC.Data.Identity.Models;

namespace CC.Data.Identity
{
    /// <summary>
    /// The IdentityDbContext
    /// </summary>
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { }

        public DbSet<Identity> Identities { get; set; }
        private Dictionary<string, Guid> _Ids;

        #region OnModelCreating
        /// <summary>
        /// OnModelCreating- set up the model relationships for the entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
        )
        {
            // Set up model relationships for navigation with keys and such
            SetupModelNavigation(modelBuilder);

            // Set up dictionary of model ids for seeding
            _Ids = new Dictionary<string, Guid>()
            {
                { "IdentityId", Guid.NewGuid() },
            };

            // Create test seed data (for each entity type in the module) initial migration/table generation
            var testIdentity = new Models.Identity()
            {
                Id = _Ids["IdentityId"],
                StartDate = DateTime.UtcNow,
                Enabled = true,
                Name = "Test Identity",
                Identifier = "test@test.com",
                Password = "hashedPassword",
                CreatedBy = "Test",
                ModifiedBy = "Test",
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            // Now seed the data into the database
            modelBuilder.Entity<Models.Identity>()
                .HasData(testIdentity);
        }
        #endregion

        #region SetupModelNavigation Helper Method
        /// <summary>
        /// SetupModelNavigation is used to set up the navigation properties for the entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SetupModelNavigation(
            ModelBuilder modelBuilder
        )
		{
            // Set up Object Shapes and Defaults

		}
        #endregion
    }

    /// <summary>
    /// creates a new instance of the IdentityDbContext
    /// </summary>
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        private string _connectionString;

        /// <summary>
        /// Default Constructor- no parameters- uses default connection string
        /// </summary>
        public IdentityDbContextFactory()
        {
            _connectionString = "Host=postgres.CC.Data.Dev;Port=5434;Database=CueCoach;Username=postgres;Password=postgres;Include Error Detail=true";
        }

        /// <summary>
        /// Constructor- takes in a connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public IdentityDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Constructor- takes in an args array
        /// </summary>
        /// <param name="args"></param>
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseNpgsql(_connectionString);
            return new IdentityDbContext(builder.Options);
        }
    }
}