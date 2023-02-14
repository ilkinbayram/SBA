﻿// <auto-generated />
using System;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SBA.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230206144939_FullCornerAndFullWinLast")]
    partial class FullCornerAndFullWinLast
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Entities.Concrete.FilterResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AwayCornerCount")
                        .HasColumnType("int");

                    b.Property<bool>("Away_FT_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_FT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_HT_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_HT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_SH_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_SH_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Away_Win_Any_Half")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_7_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_8_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_9_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_Away_3_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_Away_4_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_Home_3_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Corner_Home_4_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("FT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("FT_2_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("FT_3_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("FT_GG")
                        .HasColumnType("bit");

                    b.Property<int>("FT_Result")
                        .HasColumnType("int");

                    b.Property<int>("FT_TotalBetween")
                        .HasColumnType("int");

                    b.Property<bool>("HT_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("HT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<int>("HT_FT_Result")
                        .HasColumnType("int");

                    b.Property<bool>("HT_GG")
                        .HasColumnType("bit");

                    b.Property<int>("HT_Result")
                        .HasColumnType("int");

                    b.Property<int>("HomeCornerCount")
                        .HasColumnType("int");

                    b.Property<bool>("Home_FT_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_FT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_HT_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_HT_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_SH_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_SH_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("Home_Win_Any_Half")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsCornerFound")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Corner_FT_Win1")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Corner_FT_Win2")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Corner_FT_X")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_FT_Win1")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_FT_Win2")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_FT_X")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_HT_Win1")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_HT_Win2")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_HT_X")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_SH_Win1")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_SH_Win2")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_SH_X")
                        .HasColumnType("bit");

                    b.Property<int>("ModelType")
                        .HasColumnType("int");

                    b.Property<int>("MoreGoalsBetweenTimes")
                        .HasColumnType("int");

                    b.Property<bool>("SH_0_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("SH_1_5_Over")
                        .HasColumnType("bit");

                    b.Property<bool>("SH_GG")
                        .HasColumnType("bit");

                    b.Property<int>("SH_Result")
                        .HasColumnType("int");

                    b.Property<int>("SerialUniqueID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SerialUniqueID")
                        .HasDatabaseName("IX_SerialUniqueID");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SerialUniqueID"), false);

                    b.ToTable("FilterResults", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Concrete.MatchBet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("FTDraw_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FTWin1_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FTWin2_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_01_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_23_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_45_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_6_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_GG_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<string>("FT_Match_Result")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("FT_NG_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Over_1_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Over_2_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Over_3_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Under_1_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Under_2_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("FT_Under_3_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("HTDraw_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("HTWin1_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("HTWin2_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<string>("HT_Match_Result")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("HT_Over_1_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("HT_Under_1_5_Odd")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LeagueName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("MatchDate")
                        .HasPrecision(7, 2)
                        .HasColumnType("datetime2(7)");

                    b.Property<int>("ModelType")
                        .HasColumnType("int");

                    b.Property<int>("SerialUniqueID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Country")
                        .HasDatabaseName("IX_Country");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("Country"), false);

                    b.HasIndex("LeagueName")
                        .HasDatabaseName("IX_LeagueName");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("LeagueName"), false);

                    b.HasIndex("SerialUniqueID")
                        .HasDatabaseName("IX_SerialUniqueID");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SerialUniqueID"), false);

                    b.ToTable("MatchBets", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
