﻿// <auto-generated />
using System;
using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Architecture.Impl.EFDatabase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Architecture.Domain.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<Guid>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<bool>("IsOverdraftAllowed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Architecture.Domain.Models.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("Architecture.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Architecture.Domain.Models.NoOverdraftAccount", b =>
                {
                    b.HasBaseType("Architecture.Domain.Models.Account");

                    b.HasDiscriminator().HasValue("NoOverdraftAccount");
                });

            modelBuilder.Entity("Architecture.Domain.Models.OverdraftAccount", b =>
                {
                    b.HasBaseType("Architecture.Domain.Models.Account");

                    b.HasDiscriminator().HasValue("OverdraftAccount");
                });

            modelBuilder.Entity("Architecture.Domain.Models.Account", b =>
                {
                    b.HasOne("Architecture.Domain.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Architecture.Domain.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Architecture.Domain.Models.Customer", b =>
                {
                    b.HasOne("Architecture.Domain.Models.Bank", "Bank")
                        .WithMany("Customers")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Architecture.Domain.Models.Bank", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Architecture.Domain.Models.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
