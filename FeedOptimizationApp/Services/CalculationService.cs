﻿using DataLibrary;
using DataLibrary.DTOs;
using DataLibrary.Models;
using FeedOptimizationApp.Services.Interfaces;
using FeedOptimizationApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp.Services;

public class CalculationService : ICalculationService
{
    private readonly ApplicationDbContext _context;

    public CalculationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CalculationEntity>> GetCalculationById(int id)
    {
        try
        {
            var calculation = await _context.Calculations
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (calculation == null)
                throw new Exception($"Unable to return calculation with id {id}.");

            return await Result<CalculationEntity>.SuccessAsync(calculation);
        }
        catch (Exception ex)
        {
            return await Result<CalculationEntity>.FailAsync(new List<string> { ex.Message });
        }
    }

    public async Task<Result<int>> SaveCalculationAsync(CalculationEntity request)
    {
        try
        {
            // Validate foreign key references
            if (!await _context.SpeciesList.AnyAsync(s => s.Id == request.SpeciesId))
                throw new Exception("Invalid SpeciesId.");
            if (!await _context.Grazings.AnyAsync(g => g.Id == request.GrazingId))
                throw new Exception("Invalid GrazingId.");
            if (!await _context.BodyWeights.AnyAsync(b => b.Id == request.BodyWeightId))
                throw new Exception("Invalid BodyWeightId.");
            if (!await _context.DietQualityEstimates.AnyAsync(d => d.Id == request.DietQualityEstimateId))
                throw new Exception("Invalid DietQualityEstimateId.");
            if (!await _context.KidsLambs.AnyAsync(k => k.Id == request.KidsLambsId))
                throw new Exception("Invalid KidsLambsId.");

            var existingCalculation = await _context.Calculations.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (existingCalculation != null)
                throw new Exception("Calculation already exists. Please edit existing entry.");

            var calculation = new CalculationEntity(
                request.SpeciesId,
                request.Name,
                request.Description,
                request.Type,
                request.GrazingId,
                request.BodyWeightId,
                request.ADG,
                request.Gestation,
                request.MilkYield,
                request.FatContent,
                request.DietQualityEstimateId,
                request.KidsLambsId
            );

            await _context.Calculations.AddAsync(calculation);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculation.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationAsync(CalculationEntity request)
    {
        try
        {
            var calculation = await _context.Calculations.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (calculation == null)
                throw new Exception("Calculation does not exist.");
            calculation.Set(
                request.SpeciesId,
                request.Name,
                request.Description,
                request.Type,
                request.GrazingId,
                request.BodyWeightId,
                request.ADG,
                request.Gestation,
                request.MilkYield,
                request.FatContent,
                request.DietQualityEstimateId,
                request.KidsLambsId
            );
            _context.Calculations.Update(calculation);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculation.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<CalculationHasFeedEntity>> GetCalculationHasFeedById(int id)
    {
        try
        {
            var calculationHasFeed = await _context.CalculationHasFeeds
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            if (calculationHasFeed == null)
                throw new Exception($"Unable to return calculation has feed with Id {id}.");
            return await Result<CalculationHasFeedEntity>.SuccessAsync(calculationHasFeed);
        }
        catch (Exception ex)
        {
            return await Result<CalculationHasFeedEntity>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<List<CalculationHasFeedEntity>>> GetCalculationHasFeedsByCalculationId(int calculationId)
    {
        try
        {
            var calculationHasFeeds = await _context.CalculationHasFeeds
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(s => s.CalculationId == calculationId)
                .ToListAsync();
            if (calculationHasFeeds == null || !calculationHasFeeds.Any())
                throw new Exception($"Unable to return calculation has feeds with calculation id {calculationId}.");
            return await Result<List<CalculationHasFeedEntity>>.SuccessAsync(calculationHasFeeds);
        }
        catch (Exception ex)
        {
            return await Result<List<CalculationHasFeedEntity>>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> GetNumberOfFeedsInCalculationHasFeedByCalculationId(int calculationId)
    {
        try
        {
            var numberOfFeeds = await _context.CalculationHasFeeds
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(s => s.CalculationId == calculationId)
                .Select(s => s.FeedId)
                .Distinct()
                .CountAsync();

            return await Result<int>.SuccessAsync(numberOfFeeds);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> SaveCalculationHasFeedAsync(CalculationHasFeedEntity request)
    {
        try
        {
            var existingCalculationHasFeed = await _context.CalculationHasFeeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (existingCalculationHasFeed != null)
                throw new Exception("Calculation has feed already exists. Please edit existing entry.");
            var calculationHasFeed = new CalculationHasFeedEntity(
                request.FeedId,
                request.CalculationId,
                request.DM,
                request.CPDM,
                request.MEMJKGDM,
                request.Price,
                request.Intake,
                request.MinLimit,
                request.MaxLimit
            );
            await _context.CalculationHasFeeds.AddAsync(calculationHasFeed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasFeed.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationHasFeedAsync(CalculationHasFeedEntity request)
    {
        try
        {
            var calculationHasFeed = await _context.CalculationHasFeeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (calculationHasFeed == null)
                throw new Exception("Calculation has feed does not exist.");
            calculationHasFeed.Set(
                request.FeedId,
                request.CalculationId,
                request.DM,
                request.CPDM,
                request.MEMJKGDM,
                request.Price,
                request.Intake,
                request.MinLimit,
                request.MaxLimit
            );
            _context.CalculationHasFeeds.Update(calculationHasFeed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasFeed.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<List<CalculationHasResultEntity>>> GetAllCalculationHasResults()
    {
        try
        {
            var calculationHasResults = await _context.CalculationHasResults
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
            if (calculationHasResults == null)
                throw new Exception("No calculation has results found.");
            return await Result<List<CalculationHasResultEntity>>.SuccessAsync(calculationHasResults);
        }
        catch (Exception ex)
        {
            return await Result<List<CalculationHasResultEntity>>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<CalculationHasResultEntity>> GetCalculationHasResultById(int id)
    {
        try
        {
            var calculationHasResult = await _context.CalculationHasResults
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            if (calculationHasResult == null)
                throw new Exception($"Unable to return calculation has result with id {id}.");
            return await Result<CalculationHasResultEntity>.SuccessAsync(calculationHasResult);
        }
        catch (Exception ex)
        {
            return await Result<CalculationHasResultEntity>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<List<CalculationHasResultEntity>>> GetCalculationHasResultByCalculationId(int calculationId)
    {
        try
        {
            var calculationHasResults = await _context.CalculationHasResults
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(s => s.CalculationId == calculationId)
                .ToListAsync();

            if (calculationHasResults == null || !calculationHasResults.Any())
                throw new Exception($"Unable to return calculation has results with calculation id {calculationId}.");

            return await Result<List<CalculationHasResultEntity>>.SuccessAsync(calculationHasResults);
        }
        catch (Exception ex)
        {
            return await Result<List<CalculationHasResultEntity>>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> SaveCalculationHasResultAsync(CalculationHasResultEntity request)
    {
        try
        {
            var existingCalculationHasResult = await _context.CalculationHasResults.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (existingCalculationHasResult != null)
                throw new Exception("Calculation has result already exists. Please edit existing entry.");

            var calculationHasResult = new CalculationHasResultEntity(
                request.CalculationId,
                request.GFresh,
                request.PercentFresh,
                request.PercentDryMatter,
                request.TotalRation,
                request.DMi,
                request.CPi,
                request.MEi,
                request.Cost,
                request.DMiRequirement,
                request.CPiRequirement,
                request.MEiRequirement,
                request.EnergyRequirementMaintenance,
                request.EnergyRequirementAdditional,
                request.EnergyRequirementTotal,
                request.CrudeProteinRequirementMaintenance,
                request.CrudeProteinRequirementAdditional,
                request.DryMatterIntakeEstimateBase,
                request.DryMatterIntakeEstimateAdditional
            );

            await _context.CalculationHasResults.AddAsync(calculationHasResult);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasResult.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationHasResultAsync(CalculationHasResultEntity request)
    {
        try
        {
            var calculationHasResult = await _context.CalculationHasResults.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == request.Id);
            if (calculationHasResult == null)
                throw new Exception("Calculation has result does not exist.");
            calculationHasResult.Set(
                request.CalculationId,
                request.GFresh,
                request.PercentFresh,
                request.PercentDryMatter,
                request.TotalRation,
                request.DMi,
                request.CPi,
                request.MEi,
                request.Cost,
                request.DMiRequirement,
                request.CPiRequirement,
                request.MEiRequirement,
                request.EnergyRequirementMaintenance,
                request.EnergyRequirementAdditional,
                request.EnergyRequirementTotal,
                request.CrudeProteinRequirementMaintenance,
                request.CrudeProteinRequirementAdditional,
                request.DryMatterIntakeEstimateBase,
                request.DryMatterIntakeEstimateAdditional
            );
            _context.CalculationHasResults.Update(calculationHasResult);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasResult.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }
}