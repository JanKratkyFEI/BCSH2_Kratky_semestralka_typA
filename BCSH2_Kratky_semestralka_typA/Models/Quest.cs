using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } //nazev questu

        [Required]
        public int GuildId { get; set; } //kdo zadal
        [ValidateNever]
        public Guild Guild { get; set; }

        public int? AcceptedById { get; set; } // FK na člena, který přijal
        [ValidateNever]
        public Member? AcceptedBy { get; set; } //Navigační vlastnost

        [Required]
        public string Type { get; set; }
        public string Danger { get; set; } 
        

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Odměna musí být kladná hodnota.")]
        public decimal Pay { get; set; } //odměna ukolu
        
        public string Description { get; set; } //popis ukolu

        

       
    }
}
