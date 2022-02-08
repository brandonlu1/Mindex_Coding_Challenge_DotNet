using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetById(string employeeId)
        {
            //Checks to see if the passed id is empty or not
            if(!String.IsNullOrEmpty(employeeId))
            {
                //Finds the employee associated with the id
                Employee employee = _employeeRepository.GetById(employeeId);
                //Sets the number of direct reports
                int numDirectReports = 0;
                if (employee != null && employee.DirectReports != null)
                {
                    numDirectReports = GetReports(employee);

                }
                //Creates reportingStructure object with set information
                var newReportingStructure = new ReportingStructure()
                {
                    employee = employee,
                    numberOfReports = numDirectReports
                };
                //Returns the reportingStructure object
                return newReportingStructure;
            }
            return null;
        }

        private int GetReports(Employee employee)
        {
            //takes length of direct reports and sets variable value
            if (employee.DirectReports == null)
            {
                return 0;
            }
            else
            {
                int sum = 0;
                //for loop that loops through every DirectReport of an employee
                for (int i = 0; i < employee.DirectReports.Count; i++)
                {
                    //Adds the DirectReport count to the sum of direct reports
                    sum += 1 + GetReports(employee.DirectReports[i]);
                }
                //return sum of direct reports
                return sum;
            }
        }
    }
}
