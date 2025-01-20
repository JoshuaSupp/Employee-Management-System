using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class FullStackDbContext: DbContext
    {
        public FullStackDbContext(DbContextOptions<FullStackDbContext> options) : base(options) { 
        }

        public DbSet<Employee> Employees {  get; set; }
        public DbSet<User> Users { get; internal set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Designation> Designation { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmergencyContact>()
                .HasKey(ec => ec.Id); // Explicitly define Id as primary key

            modelBuilder.Entity<EmergencyContact>()
                .Property(ec => ec.Id)
                .ValueGeneratedOnAdd(); // Specify that Id is auto-incrementing

            // Configure one-to-many relationship
            modelBuilder.Entity<EmergencyContact>()
                .HasOne(ec => ec.Employee) // Each EmergencyContact has one Employee
                .WithMany() // An Employee can have many EmergencyContacts
                .HasForeignKey(ec => ec.EmployeeGuidId); // Specify the foreign key


            modelBuilder.Entity<Designation>()
                .HasKey(ec => ec.Id); // Explicitly define Id as primary key

            modelBuilder.Entity<Designation>()
                .Property(ec => ec.Id)
                .ValueGeneratedOnAdd(); // Specify that Id is auto-incrementing

            // Configure one-to-many relationship
            modelBuilder.Entity<Designation>()
                .HasOne(ec => ec.Employee) // Each Designation has one Employee
                .WithMany() // An Employee can have many Designations
                .HasForeignKey(ec => ec.EmployeeGuidId); // Specify the foreign key
        }

    }
}
