﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadXmlInvoce.DB;

#nullable disable

namespace ReadXmlInvoce.Migrations
{
    [DbContext(typeof(MssqlConnect))]
    [Migration("20240105075244_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReadXmlInvoce.Models.Invoce", b =>
                {
                    b.Property<string>("numDock")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("dateSell")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("numDock");

                    b.ToTable("Invoces");
                });

            modelBuilder.Entity("ReadXmlInvoce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("amount")
                        .HasColumnType("float");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("invoceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("lp")
                        .HasColumnType("int");

                    b.Property<string>("nameProduct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("taxVat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.Property<double>("valueTax")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("invoceNumber")
                        .IsUnique();

                    b.ToTable("products");
                });

            modelBuilder.Entity("ReadXmlInvoce.Models.Product", b =>
                {
                    b.HasOne("ReadXmlInvoce.Models.Invoce", "Invoce")
                        .WithOne("product")
                        .HasForeignKey("ReadXmlInvoce.Models.Product", "invoceNumber")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Invoce");
                });

            modelBuilder.Entity("ReadXmlInvoce.Models.Invoce", b =>
                {
                    b.Navigation("product")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
