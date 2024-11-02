using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Guild
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public GuildType Type { get; set; } //. Adventure , Merchant, Craft 
        [Range (-100,100)]
        public int Prestige { get; set; }
        public decimal Wealth { get; set; }


        public List<Member> Members { get; set; } = new List<Member>();
        public List<Quest> Quests { get; set; } = new List<Quest>();
        public TreasureVault TreasureVault { get; set; }
    }
}
