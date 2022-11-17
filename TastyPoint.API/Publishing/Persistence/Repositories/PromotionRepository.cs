﻿using Microsoft.EntityFrameworkCore;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Publishing.Domain.Repositories;
using TastyPoint.API.Shared.Persistence.Contexts;
using TastyPoint.API.Shared.Persistence.Repositories;

namespace TastyPoint.API.Publishing.Persistence.Repositories;

public class PromotionRepository: BaseRepository, IPromotionRepository
{
    public PromotionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Promotion>> ListAsync()
    {
        return await _context.Promotions
            .Include(p=>p.Pack)
            .ToListAsync();
    }

    public async Task AddAsync(Promotion promotion)
    {
        await _context.Promotions.AddAsync(promotion);
    }

    public async Task<Promotion> FindByIdAsync(int id)
    {
        return await _context.Promotions
            .Include(p => p.Pack)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(Promotion promotion)
    {
        _context.Promotions.Update(promotion);
    }

    public void Remove(Promotion promotion)
    {
        _context.Promotions.Remove(promotion);
    }
}