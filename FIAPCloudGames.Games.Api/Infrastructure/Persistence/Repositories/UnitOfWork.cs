using System.Data;
using FIAPCloudGames.Games.Api.Commom.Interfaces;
using FIAPCloudGames.Games.Api.Infrastructure.Persistence.Context;
using FIAPCloudGames.Games.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace FIAPCloudGames.Games.Api.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly GamesDbContext _context;

    public UnitOfWork(GamesDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IDbTransaction BeginTransaction(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction();

        return dbContextTransaction.GetDbTransaction();
    }
}
