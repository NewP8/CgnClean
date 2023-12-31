using System.ComponentModel.DataAnnotations;
using CgnClean.CgnFintech.Domain.Events;
using CgnClean.CgnFintech.Domain.Seedwork;

namespace CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;

public class Tenant
    : Entity, IAggregateRoot
{
    private DateTime _tenantDate;

    public string Nome { get; set; }

    public string Guid { get; set; }

    protected Tenant()
    {    }

    public Tenant(string tenantName, string guid) : this()
    {
        Nome = tenantName;
        Guid = guid;

        // Add the OrderStarterDomainEvent to the domain events collection 
        // to be raised/dispatched when committing changes into the Database [ After DbContext.SaveChanges() ]
        this.AddDomainEvent(new TenantCreato(
            this.Id,
            this.Nome,
            this.Guid
        ));

        // AddOrderStartedDomainEvent(userId, userName);
    }

    // private void AddOrderStartedDomainEvent(string userId, string userName, int cardTypeId, string cardNumber,
    //         string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
    // {
    //     var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardTypeId,
    //                                                                 cardNumber, cardSecurityNumber,
    //                                                                 cardHolderName, cardExpiration);

    //     this.AddDomainEvent(orderStartedDomainEvent);
    // }
}
