﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

#nullable disable

namespace SBA.ExternalDataAccess.Migrations
{
    [DbContext(typeof(ExternalAppDbContext))]
    [Migration("20230317085041_foreignKeyAddedForAll")]
    partial class foreignKeyAddedForAll
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.AverageStatisticsHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Average_FT_Corners_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Corners_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_HT_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_HT_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_SH_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_SH_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("Away_FT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_Win_Any_Half")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("BySideType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_7_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_8_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_9_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_3_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_4_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_5_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_3_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_4_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_5_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_25_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_35_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_FT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_Win_Any_Half")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("LeagueStaisticsHolderId")
                        .HasColumnType("int");

                    b.Property<int>("MatchIdentifierId")
                        .HasColumnType("int");

                    b.Property<int>("SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.HasKey("Id");

                    b.HasIndex("LeagueStaisticsHolderId");

                    b.ToTable("AverageStatisticsHolders", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.ComparisonStatisticsHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Average_FT_Corners_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Corners_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_HT_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_HT_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_SH_Goals_AwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_SH_Goals_HomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("Away_FT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Away_Win_Any_Half")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("BySideType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_7_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_8_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_9_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_3_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_4_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Away_5_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_3_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_4_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Home_5_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_25_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_35_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_FT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Home_Win_Any_Half")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_Win1")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_Win2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("LeagueStaisticsHolderId")
                        .HasColumnType("int");

                    b.Property<int>("MatchIdentifierId")
                        .HasColumnType("int");

                    b.Property<int>("SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.HasKey("Id");

                    b.HasIndex("LeagueStaisticsHolderId");

                    b.ToTable("ComparisonStatisticsHolders", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.LeagueStatisticsHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountFound")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DateOfAnalyse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

                    b.Property<decimal>("FT_GoalsAverage")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("FT_Over15_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_Over25_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_Over35_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("GG_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<decimal>("HT_GoalsAverage")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("HT_Over05_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_Over15_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<string>("LeagueName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("SH_GoalsAverage")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("SH_Over05_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_Over15_Percentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.HasKey("Id");

                    b.ToTable("LeagueStatisticsHolders", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.MatchIdentifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("MatchDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 3, 17, 0, 0, 0, 0, DateTimeKind.Local));

                    b.Property<int>("Serial")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("MatchIdentifiers", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.TeamPerformanceStatisticsHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Average_FT_Corners_Team")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_FT_Goals_Team")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_HT_Goals_Team")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<decimal>("Average_SH_Goals_Team")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)")
                        .HasDefaultValue(-1m);

                    b.Property<int>("BySideType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_7_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_8_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_9_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Team_3_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Team_4_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Corner_Team_5_5_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_25_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_35_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("FT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HT_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("HomeOrAway")
                        .HasColumnType("int");

                    b.Property<int>("Is_Corner_FT_Win")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_Corner_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_Win")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_FT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_Win")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_HT_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_Win")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Is_SH_X")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("LeagueStaisticsHolderId")
                        .HasColumnType("int");

                    b.Property<int>("MatchIdentifierId")
                        .HasColumnType("int");

                    b.Property<int>("SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SH_GG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_FT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_FT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_HT_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_HT_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_SH_05_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_SH_15_Over")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("Team_Win_Any_Half")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.HasKey("Id");

                    b.HasIndex("LeagueStaisticsHolderId");

                    b.ToTable("TeamPerformanceStatisticsHolders", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.AverageStatisticsHolder", b =>
                {
                    b.HasOne("Core.Entities.Concrete.ExternalDbEntities.LeagueStatisticsHolder", "LeagueStatisticsHolder")
                        .WithMany("AverageStatisticsHolders")
                        .HasForeignKey("LeagueStaisticsHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeagueStatisticsHolder");
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.ComparisonStatisticsHolder", b =>
                {
                    b.HasOne("Core.Entities.Concrete.ExternalDbEntities.LeagueStatisticsHolder", "LeagueStatisticsHolder")
                        .WithMany("ComparisonStatisticsHolders")
                        .HasForeignKey("LeagueStaisticsHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeagueStatisticsHolder");
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.TeamPerformanceStatisticsHolder", b =>
                {
                    b.HasOne("Core.Entities.Concrete.ExternalDbEntities.LeagueStatisticsHolder", "LeagueStatisticsHolder")
                        .WithMany("TeamPerformanceStatisticsHolders")
                        .HasForeignKey("LeagueStaisticsHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeagueStatisticsHolder");
                });

            modelBuilder.Entity("Core.Entities.Concrete.ExternalDbEntities.LeagueStatisticsHolder", b =>
                {
                    b.Navigation("AverageStatisticsHolders");

                    b.Navigation("ComparisonStatisticsHolders");

                    b.Navigation("TeamPerformanceStatisticsHolders");
                });
#pragma warning restore 612, 618
        }
    }
}
