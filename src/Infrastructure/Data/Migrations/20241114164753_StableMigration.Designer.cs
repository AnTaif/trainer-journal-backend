﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrainerJournal.Infrastructure.Data;

#nullable disable

namespace TrainerJournal.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241114164753_StableMigration")]
    partial class StableMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StudentsUserId")
                        .HasColumnType("uuid");

                    b.HasKey("GroupsId", "StudentsUserId");

                    b.HasIndex("StudentsUserId");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("a95549fd-fdf6-498a-8390-2779f7b1d534"),
                            ConcurrencyStamp = "6cc26b7c-0379-470e-a7bf-3b97139ca86f",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("d8ca5f69-cdf7-43ca-a051-ae3184295815"),
                            ConcurrencyStamp = "7c07888a-de42-474f-afa9-84036887e733",
                            Name = "Trainer",
                            NormalizedName = "TRAINER"
                        },
                        new
                        {
                            Id = new Guid("6c2c6c03-48fb-4ad2-aae0-b2f009ecc37d"),
                            ConcurrencyStamp = "07d84805-5977-4b57-9eaf-e0230c4f3fc4",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.AttendanceMark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PracticeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PracticeTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PracticeId");

                    b.HasIndex("StudentId");

                    b.ToTable("AttendanceMarks");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Relation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StudentUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StudentUserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HallAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("HexColor", "TrainerJournal.Domain.Entities.Group.HexColor#HexColor", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("text");
                        });

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeclineComment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("boolean");

                    b.Property<float>("PreviousBalance")
                        .HasColumnType("real");

                    b.Property<string>("ReceiptUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Practice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("HallAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PracticeType")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Practices");

                    b.HasDiscriminator().HasValue("Practice");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Until")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Kyu")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("KyuUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SchoolGrade")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TrainingStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Trainer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("TelegramUsername")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "TrainerJournal.Domain.Entities.User.FullName#PersonName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("MiddleName")
                                .HasColumnType("text");
                        });

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.SchedulePractice", b =>
                {
                    b.HasBaseType("TrainerJournal.Domain.Entities.Practice");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uuid");

                    b.HasIndex("ScheduleId");

                    b.HasDiscriminator().HasValue("SchedulePractice");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.SinglePractice", b =>
                {
                    b.HasBaseType("TrainerJournal.Domain.Entities.Practice");

                    b.Property<string>("CancelComment")
                        .HasColumnType("text");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("OriginalStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("OverridenPracticeId")
                        .HasColumnType("uuid");

                    b.HasIndex("OverridenPracticeId");

                    b.HasDiscriminator().HasValue("SinglePractice");
                });

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainerJournal.Domain.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainerJournal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.AttendanceMark", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Practice", "Practice")
                        .WithMany()
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainerJournal.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practice");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Contact", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Student", null)
                        .WithMany("Contacts")
                        .HasForeignKey("StudentUserId");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Group", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Payment", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Practice", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainerJournal.Domain.Entities.Trainer", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Student", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Trainer", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.SchedulePractice", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.Schedule", "Schedule")
                        .WithMany("Practices")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.SinglePractice", b =>
                {
                    b.HasOne("TrainerJournal.Domain.Entities.SchedulePractice", "OverridenPractice")
                        .WithMany()
                        .HasForeignKey("OverridenPracticeId");

                    b.Navigation("OverridenPractice");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Schedule", b =>
                {
                    b.Navigation("Practices");
                });

            modelBuilder.Entity("TrainerJournal.Domain.Entities.Student", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}