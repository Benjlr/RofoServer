﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RofoServer.Persistence;

namespace RofoServer.Persistence.Migrations
{
    [DbContext(typeof(RofoDbContext))]
    [Migration("20211009101331_weip2")]
    partial class weip2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int?>("UserAuthentication")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserAuthentication");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.UserAuthentication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AccountConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("FailedLogInAttempts")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LockOutExpiry")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SecurityStamp")
                        .HasColumnType("uuid");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("UserAuthentication");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.Property<int>("RofoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int?>("UploadedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("RofoId");

                    b.HasIndex("UploadedById");

                    b.ToTable("Rofos");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.User", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.UserAuthentication", "UserAuthDetails")
                        .WithMany()
                        .HasForeignKey("UserAuthentication");

                    b.OwnsMany("RofoServer.Domain.IdentityObjects.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("RefreshTokenId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("ReasonRevoked")
                                .HasColumnType("text");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("text");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("text");

                            b1.Property<string>("Token")
                                .HasColumnType("text");

                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.HasKey("RefreshTokenId");

                            b1.HasIndex("UserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("RofoServer.Domain.IdentityObjects.UserClaim", "UserClaims", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Description")
                                .HasColumnType("text");

                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<string>("Value")
                                .HasColumnType("text");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserClaim");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserAuthDetails");

                    b.Navigation("UserClaims");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");

                    b.Navigation("UploadedBy");
                });
#pragma warning restore 612, 618
        }
    }
}