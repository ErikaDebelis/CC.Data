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
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<PronounChoice> PronounChoices { get; set; }
        public DbSet<ProfilePronounChoice> ProfilePronounChoices { get; set; }

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
                { "ProfileId", Guid.NewGuid() },
                { "PronounChoice1Id", Guid.NewGuid() },
                { "PronounChoice2Id", Guid.NewGuid() },
                { "PronounChoice3Id", Guid.NewGuid() },
                { "PronounChoice4Id", Guid.NewGuid() },
                { "PronounChoice5Id", Guid.NewGuid() },
                { "PronounChoice6Id", Guid.NewGuid() }
            };

            // Create test seed data (for each entity type in the module) initial migration/table generation
            var testIdentity = new Models.Identity()
            {
                Id = _Ids["IdentityId"],
                StartDate = DateTime.UtcNow,
                Enabled = true,
                Name = "Test Identity",
                Email = "test@test.com",
                Password = "hashedPassword",
                CreatedBy = "System",
                ModifiedBy = "System",
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            var testProfile = new Profile()
            {
                Id = _Ids["ProfileId"],
                IdentityId = testIdentity.Id,
                Name = "Test Profile",
                FirstName = "John",
                MiddleName = "Test",
                LastName = "Doe",
                Title = Identity.Models.Title.Dr,
                Gender = Identity.Models.Gender.Male,
                CreatedBy = "System",
                ModifiedBy = "System",
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            var testPronounChoices = new List<PronounChoice>()
            {
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice1Id"],
                    Pronoun = Pronoun.He,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                },
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice2Id"],
                    Pronoun = Pronoun.Him,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                },
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice3Id"],
                    Pronoun = Pronoun.She,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                },
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice4Id"],
                    Pronoun = Pronoun.Her,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                },
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice5Id"],
                    Pronoun = Pronoun.They,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                },
                new PronounChoice()
                {
                    Id = _Ids["PronounChoice6Id"],
                    Pronoun = Pronoun.Them,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                }
            };

            testProfilePronounChoices = new List<ProfilePronounChoice>()
            {
                new ProfilePronounChoice()
                {
                    ProfileId = _Ids["ProfileId"],
                    PronounChoiceId = _Ids["PronounChoice1Id"],
                },
                new ProfilePronounChoice()
                {
                    ProfileId = _Ids["ProfileId"],
                    PronounChoiceId = _Ids["PronounChoice2Id"],
                }
            };

            // Now seed the data into the database
            modelBuilder.Entity<Models.Identity>()
                .HasData(testIdentity);

            modelBuilder.Entity<Profile>()
                .HasData(testProfile);

            modelBuilder.Entity<PronounChoice>()
                .HasData(testPronounChoices);

            modelBuilder.Entity<ProfilePronounChoice>()
                .HasData(testProfilePronounChoices);
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
            modelBuilder.Entity<Profile>()
                .HasOne(x => x.Identity)
                .WithMany()
                .HasForeignKey(x => x.ProfileId);

            modelBuilder.Entity<Profile>()
                .HasMany(x => x.Pronouns)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            modelBuilder.Entity<Profile>()
                .HasMany(model => model.Pronouns)
                .WithMany()
                .UsingEntity<ProfilePronounChoice>(
                    a => a
                        .HasOne(pc => pc.PronounChoice)
                        .WithMany()
                        .HasForeignKey(x => x.PronounChoiceId),
                    b => b
                        .HasOne(p => p.Profile)
                        .WithMany()
                        .HasForeignKey(x => x.ProfileId)
                )
                .HasKey(x => new { x.PronounChoiceId, x.ProfileId });
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