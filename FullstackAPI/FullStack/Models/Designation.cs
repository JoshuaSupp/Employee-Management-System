using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models
{
    public class Designation
    {
        public int Id { get; set; }
        public Guid EmployeeGuidId { get; set; }
        public string EmployeeDesignation { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Duration  { get; set; }
        public string Remarks { get; set; }
        public Employee Employee { get; set; }
    }
}
