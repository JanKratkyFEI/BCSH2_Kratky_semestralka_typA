using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class TreasureVault
    {
        [Key]
        public int Id { get; set; }
		[Required]
		public int GuildId { get; set; } // Cizí klíč na Gildu
        [ValidateNever]
        public Guild Guild { get; set; }


        [Required]
        [Range(0,double.MaxValue, ErrorMessage = "GoldAmount musí být kladné číslo.")]
        public decimal GoldAmount { get; set; } // Celkový počet zlaťáků v pokladnici

        public List<TreasureTransaction> Transactions { get; set; } = new List<TreasureTransaction>();
    }
}
