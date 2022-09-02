using BrankoBjelicZavrsni.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Controllers
{
    [Route("api/agencije")]
    [ApiController]
    public class AgenciesController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepository;
        public AgenciesController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetAgency(int id)
        {
            var agency = _agencyRepository.GetById(id);
            if (agency == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(agency);
            }
        }

        [HttpGet]
        public IActionResult GetAgencies()
        {
            return Ok(_agencyRepository.GetAll().ToList());
        }
        
        [HttpGet]
        [Route("~/api/prodaja")]
        public IActionResult GetAgenciesTotalPrices(int granica)
        {
            if(granica < 0)
            {
                return BadRequest();
            }
            return Ok(_agencyRepository.GetAllWithBiggerTotalPrices(granica).ToList());
        }

        [HttpGet]
        [Route("~/api/brojnost")]
        public IActionResult GetAgenciesAdverts()
        {
            return Ok(_agencyRepository.GetAllWithNumOfAdverts().ToList());
        }

        [HttpGet]
        [Route("~/api/agencije/trazi")]
        public IActionResult GetAgenciesByName(string naziv)
        {
            return Ok(_agencyRepository.GetAllByName(naziv).ToList());
        }

    }
}
