using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AvitoNotifier;

namespace AvitoNotifier.Migrations
{
    [DbContext(typeof(AvitoContext))]
    [Migration("20170729171206_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("AvitoNotifier.Entities.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Date");

                    b.Property<string>("Link");

                    b.Property<string>("Title");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });
        }
    }
}
