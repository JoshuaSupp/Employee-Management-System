namespace FullStack.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CallingName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string PersonalEmail { get; set; }
        public string Phone { get; set; }
        public string PersonalPhone { get; set; }
        public string Salary { get; set; }
        public string Pwd { get; set; }
        public string Department { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string ProfileImage { get; set; }
        public string PersonalAddress { get; set; }
        public string PermanentAddress { get; set; }

     //   public EmergencyContact EmergencyContact { get; set; }

    }
}
