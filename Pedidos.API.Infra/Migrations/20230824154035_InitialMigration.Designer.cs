﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pedidos.API.Infra.Context;

#nullable disable

namespace Pedidos.API.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230824154035_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pedidos.API.Domain.Entities.EstadoDelPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadoDelPedido", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "CREADO"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "ASIGNADO"
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "CERRADO"
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "RECHAZADO"
                        });
                });

            modelBuilder.Entity("Pedidos.API.Domain.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CicloDelPedido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CodigoDeContratoInterno")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Cuando")
                        .HasColumnType("datetime2");

                    b.Property<string>("CuentaCorriente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoDelPedidoId")
                        .HasColumnType("int");

                    b.Property<int?>("NumeroPedido")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoDelPedidoId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("Pedidos.API.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("Pedidos.API.Domain.Entities.EstadoDelPedido", "EstadoDelPedido")
                        .WithMany("Pedidos")
                        .HasForeignKey("EstadoDelPedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoDelPedido");
                });

            modelBuilder.Entity("Pedidos.API.Domain.Entities.EstadoDelPedido", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
