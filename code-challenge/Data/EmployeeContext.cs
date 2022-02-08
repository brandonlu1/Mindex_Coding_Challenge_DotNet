using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        //Wrote this helper function because DBSet was returning properties that are lists at null. Source documented: https://stackoverflow.com/questions/70407884/ef-core-class-with-property-containing-list-returns-the-list-as-null-when-fet
        public Employee getFixedEmployee (string id)
        {
            return this.Employees.Include(e=>e.DirectReports).ToList().SingleOrDefault(e=>e.EmployeeId == id);
        }
    }
}
