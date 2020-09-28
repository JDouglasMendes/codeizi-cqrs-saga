﻿// <auto-generated />
using System;
using Codeizi.CQRS.Saga.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Codeizi.CQRS.Saga.FunctionalTest.Migrations
{
    [DbContext(typeof(SagaContext))]
    [Migration("20200926125532_InitialCreateSaga")]
    partial class InitialCreateSaga
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Codeizi.CQRS.Saga.Data.SagaActionSchudele", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancel")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SagaActionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeState")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ActionSchudele");
                });

            modelBuilder.Entity("Codeizi.CQRS.Saga.Data.SagaActions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ended")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdSaga")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Initiate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Position")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeState")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Codeizi.CQRS.Saga.Data.SagaInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SagaInfo");
                });

            modelBuilder.Entity("Codeizi.CQRS.Saga.Data.SagaLogStateAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FinshedState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InitialState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SagaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("LogStateAction");
                });

            modelBuilder.Entity("Codeizi.CQRS.Saga.Data.SagaState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExtendedData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdSaga")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("States");
                });
#pragma warning restore 612, 618
        }
    }
}