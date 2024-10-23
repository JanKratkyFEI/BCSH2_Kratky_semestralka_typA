namespace BCSH2_Kratky_semestralka_typA.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public int Level { get; set; }
        public Guild Guild { get; set; }
    }
}
