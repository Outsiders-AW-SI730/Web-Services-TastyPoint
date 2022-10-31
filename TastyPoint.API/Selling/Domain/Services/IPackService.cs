﻿using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Services.Communication;

namespace TastyPoint.API.Selling.Domain.Services;

public interface IPackService
{
    Task<IEnumerable<Pack>> ListAsync();
    Task<PackResponse> SaveAsync(Pack pack);
    Task<PackResponse> UpdateAsync(int packId, Pack pack);
    Task<PackResponse> DeleteAsync(int id);
}