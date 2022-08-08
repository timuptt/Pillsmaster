﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pillsmaster.Persistence;

#nullable disable

namespace Pillsmaster.Persistence.Migrations
{
    [DbContext(typeof(PillsmasterDbContext))]
    [Migration("20220526182607_Migration_3")]
    partial class Migration_3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Pillsmaster.Domain.Models.MedicationDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CountPerTake")
                        .HasColumnType("float");

                    b.Property<int>("TakesPerDay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MedicationDays");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Medicine", b =>
                {
                    b.Property<Guid>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveIngredientCount")
                        .HasColumnType("int");

                    b.Property<string>("InternationalName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PharmaType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TradeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("FoodStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnoughToFinish")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFoodDependent")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastTakeTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MedicationDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MedicineCount")
                        .HasColumnType("int");

                    b.Property<string>("PlanStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MedicationDayId")
                        .IsUnique();

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Take", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TakeDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Takes");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.UserMedicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.ToTable("UserMedicines");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Plan", b =>
                {
                    b.HasOne("Pillsmaster.Domain.Models.MedicationDay", "MedicationDay")
                        .WithOne("Plan")
                        .HasForeignKey("Pillsmaster.Domain.Models.Plan", "MedicationDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicationDay");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Take", b =>
                {
                    b.HasOne("Pillsmaster.Domain.Models.Plan", "Plan")
                        .WithMany("Takes")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.UserMedicine", b =>
                {
                    b.HasOne("Pillsmaster.Domain.Models.Medicine", "Medicine")
                        .WithMany("UserMedicines")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.MedicationDay", b =>
                {
                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Medicine", b =>
                {
                    b.Navigation("UserMedicines");
                });

            modelBuilder.Entity("Pillsmaster.Domain.Models.Plan", b =>
                {
                    b.Navigation("Takes");
                });
#pragma warning restore 612, 618
        }
    }
}
