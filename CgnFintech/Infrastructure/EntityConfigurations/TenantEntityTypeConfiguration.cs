using CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CgnClean.CgnFintech.Infrastructure.EntityConfigurations;

class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> orderConfiguration)
    {
        orderConfiguration.ToTable("tenants");

        orderConfiguration.Ignore(b => b.DomainEvents);

        //orderConfiguration.Property(o => o.Id).UseCollation("orderseq");

        // //Address value object persisted as owned entity type supported since EF Core 2.0
        // orderConfiguration
        //     .OwnsOne(o => o.Address);

        // orderConfiguration
        //     .Property("_tenantDate")
        //     .HasColumnName("TenantDate");

        //orderConfiguration.IndexerProperty<int>("Id");
        orderConfiguration.Property<string>("Nome");
        orderConfiguration.Property<string>("Guid");
    }
}
