using System.ComponentModel.DataAnnotations;

namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName {  get; set; }
        [StringLength(50)]
        public string SurName { get; set; }
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        public int Level { get; set; }
        public int GuildId { get; set; } //FK
        public Guild Guild { get; set; }
    }
}
