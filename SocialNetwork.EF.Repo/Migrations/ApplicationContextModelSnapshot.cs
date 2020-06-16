﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialNetwork.EF.Repo;

namespace SocialNetwork.EF.Repo.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialNetwork.DataModel.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Title")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Venue")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("SocialNetwork.DataModel.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<string>("FirstName")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<string>("LastName")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("SocialNetwork.DataModel.IdentityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Passoword")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Salt")
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("VARCHAR(24)")
                        .HasMaxLength(24);

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("SocialNetwork.DataModel.UserActivity", b =>
                {
                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsHost")
                        .HasColumnType("bit");

                    b.HasKey("ActivityId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserActivity");
                });

            modelBuilder.Entity("SocialNetwork.DataModel.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Value");
                });

            modelBuilder.Entity("SocialNetwork.DataModel.IdentityUser", b =>
                {
                    b.HasOne("SocialNetwork.DataModel.AppUser", "AppUser")
                        .WithOne("IdentityUser")
                        .HasForeignKey("SocialNetwork.DataModel.IdentityUser", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialNetwork.DataModel.UserActivity", b =>
                {
                    b.HasOne("SocialNetwork.DataModel.Activity", "Activity")
                        .WithMany("UserActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialNetwork.DataModel.AppUser", "AppUser")
                        .WithMany("UserActivities")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
