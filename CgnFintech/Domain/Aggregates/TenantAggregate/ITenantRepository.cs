using CgnClean.CgnFintech.Domain.Seedwork;

namespace CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;

//This is just the RepositoryContracts or Interface defined at the Domain Layer
//as requisite for the Order Aggregate

public interface ITenantRepository : IRepository<Tenant>
{
    Tenant Add(Tenant tenant);

    // void Update(Order order);

    Task<Tenant> GetAsync(int tenantId);
}
