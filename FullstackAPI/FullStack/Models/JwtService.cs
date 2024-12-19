using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FullStack.API.Models
{
    public class JwtService
    {
        public String SecretKey { get; set; }

        public int TokenDuration { get; set; }
        private IConfiguration config;

        public JwtService(IConfiguration _config) {
        
            this.config = _config;
            this.SecretKey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string id, string firstname, string lastname, string email, string mobile, string gender, string roleId, string employeeId, string dob, string maritalstatus, string bloodgroup, string middlename, string callingname, string personalemail, string personalphone, string title, string department)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Conditionally add employeeId claim if it's not null
            var claims = new List<Claim>
            {
                new Claim("id", id),
                new Claim("firstname", firstname),
                new Claim("lastname", lastname),
                new Claim("email", email),
                new Claim("mobile", mobile),
                new Claim("gender", gender),
                new Claim("roleid", roleId)
            };

            if (!string.IsNullOrEmpty(employeeId))  // Add employeeId claim only for employees
            {
                claims.Add(new Claim("employeeId", employeeId));
            }
            if (!string.IsNullOrEmpty(dob))  // Add DOB claim only for employees
            {
                claims.Add(new Claim("dob", dob));
            }
            if (!string.IsNullOrEmpty(maritalstatus))  // Add maritalstatus claim only for employees
            {
                claims.Add(new Claim("maritalstatus", maritalstatus));
            }
            if (!string.IsNullOrEmpty(bloodgroup))  // Add bloodgroup claim only for employees
            {
                claims.Add(new Claim("bloodgroup", bloodgroup));
            }
            if (!string.IsNullOrEmpty(middlename))  // Add middlename claim only for employees
            {
                claims.Add(new Claim("middlename", middlename));
            }
            if (!string.IsNullOrEmpty(callingname))  // Add callingname claim only for employees
            {
                claims.Add(new Claim("callingname", callingname));
            }
            if (!string.IsNullOrEmpty(personalemail))  // Add personalemail claim only for employees
            {
                claims.Add(new Claim("personalemail", personalemail));
            }
            if (!string.IsNullOrEmpty(personalphone))  // Add personalphone claim only for employees
            {
                claims.Add(new Claim("personalphone", personalphone));
            }
            if (!string.IsNullOrEmpty(title))  // Add title claim only for employees
            {
                claims.Add(new Claim("title", title));

            }
            if (!string.IsNullOrEmpty(department))  // Add department claim only for employees
            {
                claims.Add(new Claim("department", department));
            }

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
