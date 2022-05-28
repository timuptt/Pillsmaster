﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pillsmaster.Application.Common.Exceptions;
using Pillsmaster.Application.Interfaces;
using Pillsmaster.Application.ViewModels;
using Pillsmaster.Domain.Models;

namespace Pillsmaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserMedicineController : ControllerBase
    {
        private readonly IUserMedicineService _userMedicineService;

        public UserMedicineController(IUserMedicineService userMedicineService, IPillsmasterDbContext dbContext)
        {
            _userMedicineService = userMedicineService;
        }

        // GET: api/<UserMedicineController>
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserMedicine>>> GetUserMedicines(Guid userId, 
            CancellationToken cancellationToken)
        {
            try
            {
                var userMedicines = await _userMedicineService.ReadUserMedicines(userId, cancellationToken);
                return Ok(userMedicines);
            }
            catch (NotFoundException e)
            {
                return NotFound($"UserMedicine not found (Exception: {e.Message})");
            }
        }

        // POST api/<UserMedicineController>
        [HttpPost]
        public async Task<ActionResult<UserMedicine>> Post([FromBody] UserMedicineViewModel userMedicineVm,
            CancellationToken cancellationToken)
        {
            var userMedicine = await _userMedicineService.CreateUserMedicine(userMedicineVm, cancellationToken);

            return Ok(userMedicine);
        }

        // PUT api/<UserMedicineController>/5
        [HttpPut("{userMedecineId}")]
        public async Task<ActionResult<UserMedicine>> Put(Guid userMedecineId, [FromBody] UserMedicineViewModel userMedicineVm,
            CancellationToken cancellationToken)
        {
            try
            {
               var updatedUserMedecine = await _userMedicineService
                   .UpdateUserMedicine(userMedecineId, userMedicineVm, cancellationToken);
               return Ok(updatedUserMedecine);
            }
            catch (NotFoundException e)
            {
                return NotFound($"UserMedicine not found (Exception: {e.Message})");
            }
        }

        // DELETE api/<UserMedicineController>/5
        [HttpDelete("{userMedecineId}")]
        public async Task<ActionResult> Delete(Guid userMedecineId, CancellationToken cancellationToken)
        {
            try
            {
                await _userMedicineService.DeleteUserMedicine(userMedecineId, cancellationToken);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound($"UserMedicine not found (Exception: {e.Message})");
            }
        }
    }
}
