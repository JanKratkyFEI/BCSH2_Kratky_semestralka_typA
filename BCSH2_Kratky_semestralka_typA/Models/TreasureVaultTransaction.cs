using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class TreasureVaultTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TreasureVaultId { get; set; } //FK
        public TreasureVault TreasureVault { get; set; }

        [Required]public decimal Amount { get; set; } //kladné pro příjem záporné pro výdaj

        [Required]
        public string TransactionType { get; set; } //Typ transakce Příjem, výdaj

        [Required]
        public DateTime Date { get; set; } = DateTime.Now; //datum a čas transakce

        public string Description { get; set; } //popis
    }
}
