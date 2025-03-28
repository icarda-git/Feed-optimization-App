﻿// <auto-generated />
using System;
using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLibrary.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250120010804_UpdateDataTypesCalculations")]
    partial class UpdateDataTypesCalculations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("DataLibrary.Models.Calculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ADG")
                        .HasColumnType("TEXT");

                    b.Property<string>("BodyWeight")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("DietQualityEstimate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("FatContent")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Gestation")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Grazing")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("KidsLambs")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("MilkYield")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Calculations");
                });

            modelBuilder.Entity("DataLibrary.Models.CalculationHasFeed", b =>
                {
                    b.Property<decimal>("CPDM")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CalculationId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DM")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FeedId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Intake")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MEMJKGDM")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MaxLimit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MinLimit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.ToTable("CalculationHasFeeds");
                });

            modelBuilder.Entity("DataLibrary.Models.CalculationHasResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CalculationId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("GFresh")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PercentDryMatter")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PercentFresh")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalRation")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CalculationHasResults");
                });

            modelBuilder.Entity("DataLibrary.Models.CountryTranslation", b =>
                {
                    b.Property<Guid>("CountryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.ToTable("CountryTranslations");
                });

            modelBuilder.Entity("DataLibrary.Models.Feed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CPPercentage")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DCPPercentage")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DryMatterPercentage")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MEMJKg")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MEMcalKg")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TDNPercentage")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("DataLibrary.Models.FeedTranslation", b =>
                {
                    b.Property<Guid>("FeedId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.ToTable("FeedTranslations");
                });

            modelBuilder.Entity("DataLibrary.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LabelKey")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("DataLibrary.Models.LabelTranslation", b =>
                {
                    b.Property<int>("LabelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("TranslationId")
                        .HasColumnType("INTEGER");

                    b.ToTable("LabelTranslations");
                });

            modelBuilder.Entity("DataLibrary.Models.LanguageTranslation", b =>
                {
                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.ToTable("LanguageTranslations");
                });

            modelBuilder.Entity("DataLibrary.Models.RefCountry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateFormat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RefCountries");
                });

            modelBuilder.Entity("DataLibrary.Models.RefLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RefLanguages");
                });

            modelBuilder.Entity("DataLibrary.Models.RefSpecies", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RefSpeciesList");
                });

            modelBuilder.Entity("DataLibrary.Models.SpeciesTranslation", b =>
                {
                    b.Property<string>("LanguageCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SpeciesId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatedDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.ToTable("SpeciesTranslations");
                });

            modelBuilder.Entity("DataLibrary.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceIdiom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceManufacturer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DevicePlatform")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceVersionString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RefCountryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RefLanguageId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RefSpeciesId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TermsAndConditions")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
