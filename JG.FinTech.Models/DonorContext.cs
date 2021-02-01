namespace JG.FinTech.Models
{
    using Microsoft.EntityFrameworkCore;
    public class DonorContext : DbContext
    {
        public DonorContext(DbContextOptions<DonorContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<DonorDetails> DonorDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DonorDetails>().HasKey(m => m.DonorID);
            base.OnModelCreating(builder);
        }
    }
}
