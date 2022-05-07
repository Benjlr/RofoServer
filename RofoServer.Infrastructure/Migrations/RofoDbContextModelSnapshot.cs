﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RofoServer.Persistence;

#nullable disable

namespace RofoServer.Persistence.Migrations
{
    [DbContext(typeof(RofoDbContext))]
    partial class RofoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.RofoUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("AccountConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("FailedLogInAttempts")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LockOutExpiry")
                        .HasColumnType("timestamp with time zone");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("RofoUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RofoUserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.Property<int>("RofoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RofoId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FileMetaData")
                        .HasColumnType("text");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("SecurityStamp")
                        .HasColumnType("uuid");

                    b.Property<int?>("UploadedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("RofoId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UploadedById");

                    b.ToTable("Rofos");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoComment", b =>
                {
                    b.Property<int>("RofoCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RofoCommentId"));

                    b.Property<int?>("ParentPhotoRofoId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<int?>("UploadedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UploadedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean");

                    b.HasKey("RofoCommentId");

                    b.HasIndex("ParentPhotoRofoId");

                    b.HasIndex("UploadedById");

                    b.ToTable("RofoComment");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SecurityStamp")
                        .HasColumnType("uuid");

                    b.Property<string>("StorageLocation")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoGroupAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Rights")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupAccess");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.RofoUser", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.UserAuthentication", "UserAuthDetails")
                        .WithMany()
                        .HasForeignKey("UserAuthDetailsId");

                    b.OwnsMany("RofoServer.Domain.IdentityObjects.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("RefreshTokenId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("RefreshTokenId"));

                            b1.Property<DateTime>("Created")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("ReasonRevoked")
                                .HasColumnType("text");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("text");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("text");

                            b1.Property<int>("RofoUserId")
                                .HasColumnType("integer");

                            b1.Property<string>("Token")
                                .HasColumnType("text");

                            b1.HasKey("RefreshTokenId");

                            b1.HasIndex("RofoUserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("RofoUserId");
                        });

                    b.Navigation("RefreshTokens");

                    b.Navigation("UserAuthDetails");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.UserClaim", b =>
                {
                    b.HasOne("RofoServer.Domain.IdentityObjects.RofoUser", null)
                        .WithMany("UserClaims")
                        .HasForeignKey("RofoUserId");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.HasOne("RofoServer.Domain.RofoObjects.RofoGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("RofoServer.Domain.IdentityObjects.RofoUser", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");

                    b.Navigation("Group");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoComment", b =>
                {
                    b.HasOne("RofoServer.Domain.RofoObjects.Rofo", "ParentPhoto")
                        .WithMany("Comments")
                        .HasForeignKey("ParentPhotoRofoId");

                    b.HasOne("RofoServer.Domain.IdentityObjects.RofoUser", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");

                    b.Navigation("ParentPhoto");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.RofoGroupAccess", b =>
                {
                    b.HasOne("RofoServer.Domain.RofoObjects.RofoGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("RofoServer.Domain.IdentityObjects.RofoUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RofoServer.Domain.IdentityObjects.RofoUser", b =>
                {
                    b.Navigation("UserClaims");
                });

            modelBuilder.Entity("RofoServer.Domain.RofoObjects.Rofo", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
