using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models
{
    public class EmergencyContact
    {
        public int Id { get; set; }
        public Guid EmployeeGuidId { get; set; }
        public string Relationship { get; set; }
        public string FullName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public Employee Employee { get; set; }

    }
}
