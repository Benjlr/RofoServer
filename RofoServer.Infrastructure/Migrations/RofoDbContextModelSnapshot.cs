﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace RofoServer.Persistence.Migrations
{
    [DbContext(typeof(RofoDbContext))]
    partial class RofoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("UserAuthDetailsId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserAuthDetailsId");

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

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
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

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SecurityStamp")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RofoGroup");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.User", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.UserAuthentication", "UserAuthDetails")
                        .WithMany()
                        .HasForeignKey("UserAuthDetailsId");

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

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserAuthDetails");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.UserClaim", b =>
                {
                    b.HasOne("RofoServer.Domain.RofoObjects.RofoGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("RofoServer.Domain.IdentityObjects.User", null)
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.User", b =>
                {
                    b.Navigation("UserClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
