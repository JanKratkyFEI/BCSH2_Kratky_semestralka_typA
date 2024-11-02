using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class TreasureVault
    {
        [Key]
        public int Id { get; set; }

        public int GuildId { get; set; } // Cizí klíč na Gildu
        public Guild Guild { get; set; }

        public decimal GoldAmount { get; set; } // Celkový počet zlaťáků v pokladnici

        public List<TreasureVaultTransaction> Transactions { get; set; }
    }
}
