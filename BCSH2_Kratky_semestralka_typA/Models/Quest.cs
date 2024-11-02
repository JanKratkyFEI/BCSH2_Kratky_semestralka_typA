using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } //nazev questu

        public string AssignedBy { get; set; } //kdo zadal

        public string AcceptedBy { get; set; } //kdo přijal

        [Required]
        public string Type { get; set; }

        public string Danger { get; set; } 
        public string Skill { get; set; } 

        [Required]
        public decimal Pay { get; set; } //odměna ukolu
        
        public string Description { get; set; } //popis ukolu
   
     public int GuildId { get; set; }
        public Guild Guild { get; set; }
    }
}
