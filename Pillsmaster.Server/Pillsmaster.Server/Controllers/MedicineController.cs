﻿using Microsoft.AspNetCore.Mvc;
using Pillsmaster.Application.Interfaces;
using Pillsmaster.Application.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pillsmaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService, IPillsmasterDbContext dbContext)
        {
            _medicineService = medicineService;
        }

        // GET: api/<MedicineController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MedicineController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicineController>
        [HttpPost]
        public async Task<ActionResult<Guid>> Post(CancellationToken cancellationToken, [FromBody] MedicineViewModel medicineVm)
        {
            var medicineId = await _medicineService.CreateMedicine(medicineVm, cancellationToken);
            return Ok(medicineId);
        }

        // PUT api/<MedicineController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicineController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
