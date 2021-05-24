﻿// <auto-generated />
using System;
using DBL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBL.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DBL.Models.Burse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Burses");
                });

            modelBuilder.Entity("DBL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("BurseId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BurseId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DBL.Models.ExtraCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ExtraCategories");
                });

            modelBuilder.Entity("DBL.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DBL.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("DBL.Models.Subscribe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("EndSubscribe")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsSubscribe")
                        .HasColumnType("boolean");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscribes");
                });

            modelBuilder.Entity("DBL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActiveSendOrder")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExtraCategoryOrder", b =>
                {
                    b.Property<int>("ExtraCategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.HasKey("ExtraCategoriesId", "OrdersId");

                    b.HasIndex("OrdersId");

                    b.ToTable("ExtraCategoryOrder");
                });

            modelBuilder.Entity("ExtraCategoryUser", b =>
                {
                    b.Property<int>("ExtraCategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("ExtraCategoriesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ExtraCategoryUser");
                });

            modelBuilder.Entity("DBL.Models.Category", b =>
                {
                    b.HasOne("DBL.Models.Burse", "Burse")
                        .WithMany("Categories")
                        .HasForeignKey("BurseId");

                    b.Navigation("Burse");
                });

            modelBuilder.Entity("DBL.Models.ExtraCategory", b =>
                {
                    b.HasOne("DBL.Models.Category", "Category")
                        .WithMany("ExtraCategories")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DBL.Models.Permission", b =>
                {
                    b.HasOne("DBL.Models.User", "User")
                        .WithOne("Permission")
                        .HasForeignKey("DBL.Models.Permission", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DBL.Models.Subscribe", b =>
                {
                    b.HasOne("DBL.Models.User", "User")
                        .WithOne("Subscribe")
                        .HasForeignKey("DBL.Models.Subscribe", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExtraCategoryOrder", b =>
                {
                    b.HasOne("DBL.Models.ExtraCategory", null)
                        .WithMany()
                        .HasForeignKey("ExtraCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBL.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExtraCategoryUser", b =>
                {
                    b.HasOne("DBL.Models.ExtraCategory", null)
                        .WithMany()
                        .HasForeignKey("ExtraCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DBL.Models.Burse", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("DBL.Models.Category", b =>
                {
                    b.Navigation("ExtraCategories");
                });

            modelBuilder.Entity("DBL.Models.User", b =>
                {
                    b.Navigation("Permission");

                    b.Navigation("Subscribe");
                });
#pragma warning restore 612, 618
        }
    }
}
