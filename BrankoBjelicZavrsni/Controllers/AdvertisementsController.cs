using AutoMapper;
using AutoMapper.QueryableExtensions;
using BrankoBjelicZavrsni.Interfaces;
using BrankoBjelicZavrsni.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Controllers
{
    [Route("api/oglasi")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
        public AdvertisementsController(IAdvertisementRepository advertisementRepository, IMapper mapper)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAdvertisements()
        {
            return Ok(_advertisementRepository.GetAll().ProjectTo<AdvertisementDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetAdvertisement(int id)
        {
            var advertisement = _advertisementRepository.GetById(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<AdvertisementDTO>(advertisement));
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostAdvertisement(Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _advertisementRepository.Add(advertisement);
            return CreatedAtAction("GetAdvertisement", new { Id = advertisement.Id }, advertisement);
        }

        [HttpPut("{id}")]
        public IActionResult PutAdvertisement(int id, Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != advertisement.Id)
            {
                return BadRequest();
            }
            try
            {
                _advertisementRepository.Update(advertisement);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(advertisement);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteAdvertisement(int id)
        {
            var advertisement = _advertisementRepository.GetById(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            _advertisementRepository.Delete(advertisement);
            return Ok();    //moze i return NoContent();
        }

        [HttpGet]
        [Route("~/api/oglasi/trazi")]
        public IActionResult GetAdsByType(string type)
        {
            return Ok(_advertisementRepository.GetAllByType(type).ProjectTo<AdvertisementDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpPost]
        [Route("~/api/pretraga")]
        public IActionResult GetAdsByPrice(SearchDTO search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(search.Najmanje > search.Najvise)
            {
                return BadRequest();
            }
            return Ok(_advertisementRepository.GetAllBySearchPrice(search).ProjectTo<AdvertisementDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
