using BCSH2_Kratky_semestralka_typA.Models;
using Microsoft.EntityFrameworkCore;
namespace BCSH2_Kratky_semestralka_typA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {


        }

        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<TreasureVault> TreasureVaults { get; set; }
        public DbSet<Treasure> Treasures { get; set; }

        public DbSet<TreasureTransaction> Transactions { get; set;}
        public DbSet<Quest> Quests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurace relace mezi Member a Quest
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.AcceptedBy)
                .WithMany(m => m.AcceptedQuests)
                .HasForeignKey(q => q.AcceptedById)
                .OnDelete(DeleteBehavior.SetNull); // Quest může být nepřijatý (AcceptedBy může být null)
        }

    }
}
