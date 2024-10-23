namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class TreasureVault
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public ICollection<Treasure> Treasures { get; set; }
        public Guild guild { get; set; }
    }
}
