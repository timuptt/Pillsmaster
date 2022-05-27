﻿using Microsoft.AspNetCore.Mvc;

using Pillsmaster.Application.Common.Exceptions;
using Pillsmaster.Application.Interfaces;
using Pillsmaster.Application.ViewModels;
using Pillsmaster.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pillsmaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        // GET: api/<PlanController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PlanController>/5
        [HttpGet("{planId}")]
        public async Task<ActionResult<Plan>> Get(Guid planId, CancellationToken cancellationToken)
        {
            try
            {
                var plan = await _planService.ReadPlan(planId, cancellationToken);
                return Ok(plan);
            }
            catch (NotFoundException e)
            {
                return NotFound($"Plan not found (Exception: {e.Message})");
            }
        }

        // POST api/<PlanController>
        [HttpPost]
        public async Task<ActionResult<Plan>> Post([FromBody] PlanViewModel planVm, 
            CancellationToken cancellationToken)
        {
            var plan = await _planService.CreatePlan(planVm, cancellationToken);

            return Ok(plan);
        }

        // PUT api/<PlanController>/5
        [HttpPut("{planId}")]
        public async Task<ActionResult<Plan>> Put(Guid planId, [FromBody] PlanViewModel planVm,
            CancellationToken cancellationToken)
        {
            var updatedPlan = await _planService.UpdatePlan(planId, planVm, cancellationToken);

            return Ok(updatedPlan);
        }

        // DELETE api/<PlanController>/5
        [HttpDelete("{planId}")]
        public async Task<ActionResult> Delete(Guid planId, CancellationToken cancellationToken)
        {
            await _planService.DeletePlan(planId, cancellationToken);

            return Ok();
        }
    }
}