﻿// <auto-generated />
using CgnClean.CgnFintech.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CgnClean.Migrations
{
    [DbContext(typeof(CgnFintechContext))]
    [Migration("20231231144204_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("tenant")
                .HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("CgnClean.CgnFintech.Domain.Aggregates.TenantAggregate.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Guid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("tenants", "tenant");
                });
#pragma warning restore 612, 618
        }
    }
}