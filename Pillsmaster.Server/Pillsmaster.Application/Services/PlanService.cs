﻿using Microsoft.EntityFrameworkCore;

using Pillsmaster.Application.Common.Exceptions;
using Pillsmaster.Application.Interfaces;
using Pillsmaster.Application.ViewModels;
using Pillsmaster.Domain.Models;

namespace Pillsmaster.Application.Services
{
    public class PlanService : BaseService, IPlanService
    {
        public PlanService(IPillsmasterDbContext dbContext) : base(dbContext) { }

        public async Task<Plan> CreatePlan(Guid userId, PlanViewModel planVm,  CancellationToken cancellationToken)
        {
            var plan = new Plan()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                MedicineCount = planVm.MedicineCount,
                Duration = planVm.Duration,
                IsEnoughToFinish = planVm.IsEnoughToFinish,
                FoodStatus = planVm.FoodStatus,
                PlanStatus = planVm.PlanStatus,
                IsFoodDependent = planVm.IsFoodDependent,
                MedicationDay = new MedicationDay()
                {
                    Id = Guid.NewGuid(),
                    CountPerTake = planVm.MedicationDayVm.CountPerTake,
                    TakesPerDay = planVm.MedicationDayVm.TakesPerDay
                },
                LastTakeTime = planVm.LastTakeTime,
            };

            plan.Takes = new List<Take>();

            foreach (var takeVm in planVm.Takes)
            {
                plan.Takes.Add(
                    new Take()
                    {
                        Id = Guid.NewGuid(),
                        PlanId = plan.Id,
                        TakeDateTime = takeVm.TakeDateTime
                    });
            }

            await _dbContext.Plans.AddAsync(plan, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return plan;
        }

        public async Task<Plan> ReadPlan(Guid planId, CancellationToken cancellationToken)
        {
            var plan = await _dbContext.Plans
                .Include(plan => plan.MedicationDay)
                .Include(plan => plan.Takes)
                .FirstOrDefaultAsync(plan => plan.Id == planId, cancellationToken);

            if (plan is null)
                throw new NotFoundException(typeof(Plan), planId);

            return plan;
        }

        public async Task<Plan> UpdatePlan(Guid planId, PlanViewModel planVm, CancellationToken cancellationToken)
        {
            var dbPlan = await ReadPlan(planId, cancellationToken);

            dbPlan.MedicineCount = planVm.MedicineCount;
            dbPlan.Duration = planVm.Duration;
            dbPlan.IsEnoughToFinish = planVm.IsEnoughToFinish;
            dbPlan.FoodStatus = planVm.FoodStatus;
            dbPlan.PlanStatus = planVm.PlanStatus;
            dbPlan.IsFoodDependent = planVm.IsFoodDependent;
            dbPlan.MedicationDay.CountPerTake = planVm.MedicationDayVm.CountPerTake;
            dbPlan.MedicationDay.TakesPerDay = planVm.MedicationDayVm.TakesPerDay;
            dbPlan.LastTakeTime = planVm.LastTakeTime;

            await _dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return dbPlan;
        }

        public async Task DeletePlan(Guid planId, CancellationToken cancellationToken)
        {
            var plan = await ReadPlan(planId, cancellationToken);

            _dbContext.Plans.Remove(plan);

            await _dbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
