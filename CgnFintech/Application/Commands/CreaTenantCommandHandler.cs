using CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;
using CgnClean.CgnFintech.Infrastructure;
using MediatR;

namespace CgnClean.CgnFintech.Application.Commands;

// Regular CommandHandler
public class CreaTenantCommandHandler
    : IRequestHandler<CreaTenant, int>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IMediator _mediator;

    // Using DI to inject infrastructure persistence Repositories
    public CreaTenantCommandHandler(IMediator mediator,ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<int> Handle(CreaTenant message, CancellationToken cancellationToken)
    {
        // // Add Integration event to clean the basket
        // var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
        // await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

        var tenant = new Tenant(message.TenantName,Guid.NewGuid().ToString("N"));
        _tenantRepository.Add(tenant);
        _ = await _tenantRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
        
        //.Create(order);

        return tenant.Id; // Task.FromResult(creato);
    }
}