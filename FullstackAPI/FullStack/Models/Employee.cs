﻿namespace FullStack.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Phone { get; set; }
        public string Salary { get; set; }
        public string Department { get; set; }

    }
}
