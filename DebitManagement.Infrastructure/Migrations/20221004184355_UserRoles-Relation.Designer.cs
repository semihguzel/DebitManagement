﻿// <auto-generated />
using System;
using DebitManagement.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DebitManagement.Repository.Migrations
{
    [DbContext(typeof(DebitContext))]
    [Migration("20221004184355_UserRoles-Relation")]
    partial class UserRolesRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DebitManagement.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<string>("ProductTypeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductTypeDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProductType", (string)null);
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.UserUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "UserRoleId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserUserRoles");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.Property<Guid>("UserRolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("UserRolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserUserRole");
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.Product", b =>
                {
                    b.HasOne("DebitManagement.Core.Entities.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.UserUserRole", b =>
                {
                    b.HasOne("DebitManagement.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DebitManagement.Core.Entities.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.HasOne("DebitManagement.Core.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("UserRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DebitManagement.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DebitManagement.Core.Entities.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
