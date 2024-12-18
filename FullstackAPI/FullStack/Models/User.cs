namespace FullStack.API.Models
{
    public class User
    { 
        public int UserID { get; set; }
        public String FirstName { get; set; }   
        public String LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Pwd { get; set; }
        public DateTime MemberSince { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
