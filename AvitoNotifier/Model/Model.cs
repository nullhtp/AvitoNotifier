using Microsoft.EntityFrameworkCore;

namespace AvitoNotifier
{
    public class AvitoContext: DbContext
    {
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=avitonotifier.db");
        }
    }
}