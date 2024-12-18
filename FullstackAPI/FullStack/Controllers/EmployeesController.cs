using FullStack.API.Data;
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

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }


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
                    userAvailable.LastName,
                    userAvailable.Email,
                    userAvailable.Mobile,
                    userAvailable.Gender
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
                var token = new JwtService(_config).GenerateToken(
                    employeeAvailable.Id.ToString(),  // Employee's unique ID (use as UserID or other identifier)
                    employeeAvailable.Name,
                    employeeAvailable.LastName,
                    employeeAvailable.Email,
                    employeeAvailable.Phone, // Assuming employees have a phone field
                    "Employee" // Or some other default gender or value you want to return
                );

                return Ok(new
                {
                    status = "Success",
                    roleId = 2, // Assuming employees have a different roleId, modify as needed
                    token = token
                });
            }

            // If neither user nor employee is found
            return Ok(new { status = "Failure" });
        }

    }
}

