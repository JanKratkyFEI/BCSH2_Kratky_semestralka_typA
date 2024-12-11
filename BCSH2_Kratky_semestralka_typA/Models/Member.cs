using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Member
    {
        [Key]
        public int Id_Member { get; set; }
        [Required]
        [StringLength(50)]
        public string Name {  get; set; }
        [StringLength(50)]
        public string Surname { get; set; } //volitelný 

        [Required]
        [StringLength(50)]
        public string Rank { get; set; } //pozice  člena

        public string Class { get; set; } //povolání

        [Required]
        public int GuildId { get; set; } //FK
        [ValidateNever]
        public Guild Guild { get; set; }
        public List<Quest> AcceptedQuests { get; set; } = new List<Quest>(); //navigační vlastnost
        public List<TreasureTransaction> Transactions { get; set; } = new List<TreasureTransaction>();
    }
}
