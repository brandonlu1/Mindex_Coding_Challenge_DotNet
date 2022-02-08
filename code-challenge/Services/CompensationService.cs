using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ILogger<CompensationService> _logger;
        private readonly ICompensationRepository _compensationRepository;


        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            //Checks to see if compensation is null or not
            if (compensation != null)
            {
                //Adds compensation to database
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }
            return compensation;
        }

        public Compensation GetById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {            
                return _compensationRepository.GetById(id);

            }

            return null;
        }

    }
}
