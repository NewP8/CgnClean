using CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;
using CgnClean.CgnFintech.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace CgnClean.CgnFintech.Infrastructure.Repositories;

public class TenantRepository
    : ITenantRepository
{
    private readonly CgnFintechContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public TenantRepository(CgnFintechContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Tenant Add(Tenant tenant)
    {
        return _context.Tenants.Add(tenant).Entity;

    }

    public async Task<Tenant> GetAsync(int tenantId)
    {
        var tenant = await _context.Tenants.FindAsync(tenantId);
        return tenant;
    }

    public void Update(Tenant tenant)
    {
        _context.Entry(tenant).State = EntityState.Modified;
    }
}
