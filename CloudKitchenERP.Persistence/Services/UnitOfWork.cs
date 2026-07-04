using CloudKitchenERP.Application.Interfaces;
using CloudKitchenERP.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudKitchenERP.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}