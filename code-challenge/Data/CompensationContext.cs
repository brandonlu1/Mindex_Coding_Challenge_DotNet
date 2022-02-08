using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class CompensationContext : DbContext
    {
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }

        public DbSet<Compensation> Compensations { get; set; }

        //Wrote this helper function because DBSet was returning properties that are lists at null. Source documented: https://stackoverflow.com/questions/70407884/ef-core-class-with-property-containing-list-returns-the-list-as-null-when-fet
        public Compensation getFixedEmployee(string id)
        {
            return this.Compensations.Include(e => e.employee).ToList().SingleOrDefault(e => e.EmployeeId == id);
        }
    }
    
}

