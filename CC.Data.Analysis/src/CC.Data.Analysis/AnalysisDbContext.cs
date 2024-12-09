using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using CC.Data.Basics;
using CC.Data.Identity;
using CC.Data.Chat;

using CC.Data.Analysis.Models.Entities;

namespace CC.Data.Analysis
{
    public class AnalysisDbContext : DbContext
    {
        public AnalysisDbContext(DbContextOptions<AnalysisDbContext> options) : base(options)
        { }

        public DbSet<Video> Videos { get; set; }
        public DbSet<EnhancedCaption> Captions { get; set; }
        public DbSet<Analysis> Analyses { get; set; }

        private Dictionary<string, Guid> _Ids;

        #region OnModelCreating
        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
        )
        {
            // Set up dictionary of model ids for seeding
            //Todo: changes these to constants rather than new guids
            _Ids = new Dictionary<string, Guid>()
            {
                //keep the same id for the identity so that the seed data can be used for testing
                { "Identity1Id", Guid.NewGuid()},
                { "Identity2Id", Guid.NewGuid()},
                { "VideoId", Guid.NewGuid()},
                { "CaptionId", Guid.NewGuid()},
                { "AnalysisId", Guid.NewGuid()},
            };

            // Create test seed data (for each entity type in the module) initial migration/table generation

            //todo: fix seeded data
            var testVideos = new List<Video>()
            {
                new Models.Entities.Video()
                {
                    Id = _Ids["VideoId"],
                    CreatedBy = "Test",
                    ModifiedBy = "Test",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                }
            };

            var testCaptions = new List<Models.Entities.Caption>()
            {
                new Models.Entities.Caption()
                {
                    Id = _Ids["CaptionId"],
                    CreatedBy = "Test",
                    ModifiedBy = "Test",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                }
            };

            var testAnalyses = new List<Models.Entities.Analysis>()
            {
                new Models.Entities.Analysis()
                {
                    Id = _Ids["AnalysisId"],
                    CreatedBy = "Test",
                    ModifiedBy = "Test",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                }
            };

            // Add Identity and Chat Models (Dependencies) + Ignore Dependant Tables for Migration (they've already been added/created)
            IdentityDbContext.SetupModelNavigation(modelBuilder);
            IdentityDbContext.IgnoreTables(modelBuilder);
            ChatDbContext.SetupModelNavigation(modelBuilder);
            ChatDbContext.IgnoreTables(modelBuilder);

            SetupModelNavigation(modelBuilder);

            // Now seed the data into the database
            modelBuilder.Entity<Video>()
                .HasData(testVideos);

            modelBuilder.Entity<Caption>()
                .HasData(testCaptions);

            modelBuilder.Entity<Analysis>()
                .HasData(testAnalyses);
        }
        #endregion

        #region IgnoreTables
        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void IgnoreTables(
            ModelBuilder modelBuilder
        )
        {
            modelBuilder.Entity<Video>()
                .ToTable(
                    nameof(Video),
                    t => t.ExcludeFromMigrations()
                );
            modelBuilder.Entity<Caption>()
                .ToTable(
                    nameof(Caption),
                    t => t.ExcludeFromMigrations()
                );
            modelBuilder.Entity<Analysis>()
                .ToTable(
                    nameof(Models.Entities.Analysis),
                    t => t.ExcludeFromMigrations()
                );
        }
        #endregion

        #region SetupModelNavigation
        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SetupModelNavigation(
            ModelBuilder modelBuilder
        )
        {
            //create the type variables for the entities
            var videoType = typeof(Models.Entities.Video);
            var captionType = typeof(Models.Entities.Caption);
            var analysisModelType = typeof(Models.Entities.Analysis);

            //create the entity helper and pass in the model builder and the types and recurse to build the entities
            var entityHelper = new EntityHelper();

            entityHelper.EntityBuilder(modelBuilder, videoType, null);
            entityHelper.EntityBuilder(modelBuilder, captionType, null);
            entityHelper.EntityBuilder(modelBuilder, analysisModelType, null);

            // Set up model relationships for navigation with keys and such
            modelBuilder.Entity<Video>()
                .HasMany(v => v.Captions)
                .WithOne(c => c.Video)
                .HasForeignKey(c => c.VideoId);

            modelBuilder.Entity<EnhancedCaption>()
                .HasMany(c => c.Analyses)
                .WithOne(a => a.Caption)
                .HasForeignKey(a => a.CaptionId);

            modelBuilder.Entity<Analysis>()
                .HasOne(a => a.Video)
                .WithMany(v => v.Analyses)
                .HasForeignKey(a => a.VideoId);
        }
        #endregion
    }

    /// <summary>
    ///create a new instance of the AnalysisDbContext
    /// </summary>
    public class AnalysisDbContextFactory : IDesignTimeDbContextFactory<AnalysisDbContext>
    {
        private string _connectionString;

        /// <summary>
        ///
        /// </summary>
        public AnalysisDbContextFactory()
        {
            _connectionString = "Host=postgres.CC.Data.Dev;Port=5434;Database=CueCoach;Username=postgres;Password=postgres;Include Error Detail=true";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString"></param>
        public AnalysisDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public AnalysisDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AnalysisDbContext>();
            builder.UseNpgsql(_connectionString);
            return new AnalysisDbContext(builder.Options);
        }
    }
}