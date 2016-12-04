using Microsoft.EntityFrameworkCore;
using url_compress_demo.Models;

namespace url_compress_demo.Persistance
{
    public class CompressorContext: DbContext
    {
        public DbSet<CompressedUrl> Urls { get; set; }

        public CompressorContext(DbContextOptions<CompressorContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompressedUrl>().HasKey(url => url.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
