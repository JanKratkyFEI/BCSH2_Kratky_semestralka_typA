using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Guild
    {
        [Key]
        public int Id_Guild { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public GuildType Type { get; set; } //. Adventure , Merchant, Craft 
        [Range(-100, 100)]
        public int Prestige { get; set; }


        public List<Member> Members { get; set; } = new List<Member>();
        public List<Quest> Quests { get; set; } = new List<Quest>();
        [NotMapped] //navigační vlastnost se nebude mapovat ani validovat 
        public List<TreasureVault> TreasureVault { get; set; } = new List<TreasureVault>();
    }
}
