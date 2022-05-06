using JobManager.Core.Data.DataTransferObjects;
using JobManager.Core.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JobManager.Core.Database
{
    public class JobManagerContext : DbContext
    {
        public virtual DbSet<DbUser> Users { get; set; }
        public virtual DbSet<DbJobOffer> JobOffers { get; set; }
        public virtual DbSet<DbCategory> Categories { get; set; }
        public DbSet<DbJobOfferCategory> DbJobOfferCategories { get; set; }


        private readonly AppSettings _appSettings;


        public JobManagerContext(DbContextOptions options, IOptions<AppSettings> appSettings) : base(options)
        {
            _appSettings = appSettings.Value;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySQL(_appSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbJobOfferCategory>()
                .HasKey(jc => new { jc.CategoryId, jc.JobOfferId });
            modelBuilder.Entity<DbJobOfferCategory>()
                .HasOne(jc => jc.JobOffer)
                .WithMany(jobOffer => jobOffer.JobOfferCategories)
                .HasForeignKey(jobOffer => jobOffer.JobOfferId);
            modelBuilder.Entity<DbJobOfferCategory>()
                .HasOne(jc => jc.Category)
                .WithMany(jobOffer => jobOffer.JobOfferCategories)
                .HasForeignKey(jobOffer => jobOffer.CategoryId);
        }
    }
}
