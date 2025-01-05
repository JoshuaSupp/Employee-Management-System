using FullStack.API.Data;
using FullStack.API.Migrations;
using FullStack.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        private readonly IConfiguration _config;

        public EmployeesController(IConfiguration config, FullStackDbContext fullStackDbContext) {
            _config = config;
            _fullStackDbContext = fullStackDbContext;
        }

        //all employees for admin
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        //add employees for admin
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        //get employee from ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = 
                await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);

        }

        //get employee data from employeeId
        [HttpGet]
        [Route("{employeeId:Int}")]
        public async Task<IActionResult> GetEmployeeProfile([FromRoute] int employeeId)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);

        }

        //todo find correct API endpoint link
        //get employee name from employeeid
        [HttpGet]
        [Route("/employeeName/{employeeId:Int}")]
        public async Task<IActionResult> GetEmployeeName([FromRoute] int employeeId)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(new { employee.Name, employee.LastName });

        }


        //update employee in admin
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest )
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.LastName = updateEmployeeRequest.LastName;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }

      
        //update employee profile (employee)
        [HttpPut("/updateEmployee")]
        public async Task<IActionResult> updateProfileEmployee([FromBody] Employee user)
        {
            var existingUser = _fullStackDbContext.Employees.FirstOrDefault(u => u.EmployeeId == user.EmployeeId);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Update the user's properties
            existingUser.Name = user.Name;
            existingUser.MiddleName = user.MiddleName;
            existingUser.LastName = user.LastName;
            existingUser.CallingName = user.CallingName;
            existingUser.DOB = user.DOB;
            existingUser.MaritalStatus = user.MaritalStatus;
            existingUser.BloodGroup = user.BloodGroup;
            existingUser.PersonalAddress = user.PersonalAddress;
            existingUser.PermanentAddress = user.PermanentAddress;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(existingUser);
        }

        //delete employee in admin
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            
            if(employee == null)
            {
                return NotFound();
            }

            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();   
            
            return Ok(employee);
        }

        //login user used
        //[AllowAnonymous]
        //[HttpPost("LoginUser")]
        //public IActionResult Login(Login user)
        //{
        //    var userAvaiable = _fullStackDbContext.Users.Where(u => u.Email == user.Email && u.Pwd == user.Pwd).FirstOrDefault();
        //    if (userAvaiable != null)
        //    {
        //        return Ok(new JwtService(_config).GenerateToken(
        //            userAvaiable.UserID.ToString(),
        //            userAvaiable.FirstName,
        //            userAvaiable.LastName,
        //            userAvaiable.Email,
        //            userAvaiable.Mobile,
        //            userAvaiable.Gender
        //        )
        //    );

        //    }
        //    return Ok("Failure");
        //}


     
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            // First check if the user exists in the Users table
            var userAvailable = _fullStackDbContext.Users
                .Where(u => u.Email == user.Email && u.Pwd == user.Pwd)
                .FirstOrDefault();

            // If the user is found in Users table
            if (userAvailable != null)
            {
                var token = new JwtService(_config).GenerateToken(
                    userAvailable.UserID.ToString(),
                    userAvailable.FirstName,
                    null,
                    userAvailable.LastName,
                    userAvailable.Email,
                    userAvailable.Mobile,
                    userAvailable.Gender,
                    userAvailable.RoleID.ToString(),
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null

                );

                return Ok(new
                {
                    status = "Success",
                    roleId = userAvailable.RoleID, // Role ID from Users table
                    token = token
                });
            }

            // If the user is not found in Users table, check the Employees table
            var employeeAvailable = _fullStackDbContext.Employees
                .Where(e => e.Email == user.Email && e.Pwd == user.Pwd)
                .FirstOrDefault();

            if (employeeAvailable != null)
            {
                var employeeId = employeeAvailable.EmployeeId;
                

                var token = new JwtService(_config).GenerateToken(
                    employeeAvailable.Id.ToString(),  // Employee's unique ID (use as UserID or other identifier)
                    null,
                    employeeAvailable.Name,
                    employeeAvailable.LastName,
                    employeeAvailable.Email,
                    employeeAvailable.Phone,
                    employeeAvailable.Gender,
                    "2",
                    employeeAvailable.EmployeeId.ToString(),
                    employeeAvailable.DOB,
                    employeeAvailable.MaritalStatus,
                    employeeAvailable.BloodGroup,
                    employeeAvailable.MiddleName,
                    employeeAvailable.CallingName,
                    employeeAvailable.PersonalEmail,
                    employeeAvailable.PersonalPhone,
                    employeeAvailable.Title,
                    employeeAvailable.Department,
                    employeeAvailable.PermanentAddress,
                    employeeAvailable.PersonalAddress

                );

                return Ok(new
                {
                    status = "Success",
                    roleId = 2,
                   // employeeId = employeeAvailable.EmployeeId,
                    token = token
                });
            }

            // If neither user nor employee is found
            return Unauthorized(new { status = "Failure", message = "Invalid login credentials" });
        }

    }
}

