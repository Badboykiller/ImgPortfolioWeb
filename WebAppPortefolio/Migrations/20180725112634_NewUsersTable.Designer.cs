﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppPortefolio.Data;

namespace WebAppPortefolio.Migrations
{
    [DbContext(typeof(PortefolioContext))]
    [Migration("20180725112634_NewUsersTable")]
    partial class NewUsersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebAppPortefolio.Models.Utilizador", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeleted");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Nome");

                    b.Property<string>("PasswordH");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Utilizadores");
                });
#pragma warning restore 612, 618
        }
    }
}