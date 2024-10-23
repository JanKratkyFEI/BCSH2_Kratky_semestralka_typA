namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Treasure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type {  get; set; }
        public decimal Value {  get; set; }
        public string Description { get; set; }
        public TreasureVault TreasureVault { get; set; }
    }
}
