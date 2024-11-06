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

    }
}
