namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status {  get; set; }
        public string Rank {  get; set; }
        public ICollection<Member> AssignedMembers { get; set; }
    }
}
