namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Guild
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        
        public ICollection<Member> Members { get; set; }
        public TreasureVault TreasureVault { get; set; }
    }
}
