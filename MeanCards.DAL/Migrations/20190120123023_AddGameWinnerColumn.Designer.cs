﻿// <auto-generated />
using System;
using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeanCards.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190120123023_AddGameWinnerColumn")]
    partial class AddGameWinnerColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("meancards")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeanCards.DAL.Entity.AnswerCard", b =>
                {
                    b.Property<int>("AnswerCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdultContent");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text")
                        .HasMaxLength(256);

                    b.HasKey("AnswerCardId");

                    b.HasIndex("LanguageId");

                    b.ToTable("AnswerCards");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("OwnerId");

                    b.Property<int>("PointsLimit");

                    b.Property<bool>("ShowAdultContent");

                    b.Property<byte>("Status");

                    b.Property<int?>("WinnerId");

                    b.HasKey("GameId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.GameCheckpoint", b =>
                {
                    b.Property<int>("GameCheckpointId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("GameId");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("GameCheckpointId");

                    b.HasIndex("GameId");

                    b.ToTable("GameCheckpoints");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.GameRound", b =>
                {
                    b.Property<int>("GameRoundId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("GameId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Number");

                    b.Property<int>("OwnerPlayerId");

                    b.Property<int>("QuestionCardId");

                    b.Property<byte>("Status");

                    b.Property<int?>("WinnerPlayerId");

                    b.HasKey("GameRoundId");

                    b.HasIndex("GameId");

                    b.HasIndex("OwnerPlayerId");

                    b.HasIndex("QuestionCardId");

                    b.HasIndex("WinnerPlayerId");

                    b.ToTable("GameRounds");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(8);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Number");

                    b.Property<int>("Points");

                    b.Property<int>("UserId");

                    b.HasKey("PlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.PlayerAnswer", b =>
                {
                    b.Property<int>("PlayerAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerCardId");

                    b.Property<int>("GameRoundId");

                    b.Property<bool>("IsSelectedAnswer");

                    b.Property<int>("PlayerId");

                    b.Property<int?>("SecondaryAnswerCardId");

                    b.HasKey("PlayerAnswerId");

                    b.HasIndex("AnswerCardId");

                    b.HasIndex("GameRoundId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SecondaryAnswerCardId");

                    b.ToTable("PlayerAnswers");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.PlayerCard", b =>
                {
                    b.Property<int>("PlayerCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerCardId");

                    b.Property<bool>("IsUsed");

                    b.Property<int>("PlayerId");

                    b.HasKey("PlayerCardId");

                    b.HasIndex("AnswerCardId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayersCards");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.QuestionCard", b =>
                {
                    b.Property<int>("QuestionCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdultContent");

                    b.Property<int>("LanguageId");

                    b.Property<byte>("NumberOfAnswers");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("QuestionCardId");

                    b.HasIndex("LanguageId");

                    b.ToTable("QuestionCards");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Code")
                        .HasMaxLength(64);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("GoogleId")
                        .HasMaxLength(64);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.AnswerCard", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Language", "Language")
                        .WithMany("AnswerCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.Game", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Language", "Language")
                        .WithMany("Games")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.User", "Owner")
                        .WithMany("Games")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.User", "Winner")
                        .WithMany("WonGames")
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.GameCheckpoint", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Game", "Game")
                        .WithMany("GameCheckpoints")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.GameRound", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Game", "Game")
                        .WithMany("GameRounds")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.Player", "RoundOwner")
                        .WithMany("OwnedGameRounds")
                        .HasForeignKey("OwnerPlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.QuestionCard", "QuestionCard")
                        .WithMany("GameRounds")
                        .HasForeignKey("QuestionCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.Player", "RoundWinner")
                        .WithMany("WonRounds")
                        .HasForeignKey("WinnerPlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.Player", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.PlayerAnswer", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.AnswerCard", "AnswerCard")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("AnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.GameRound", "GameRound")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("GameRoundId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.Player", "Player")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.AnswerCard", "SecondaryAnswerCard")
                        .WithMany("SecondaryPlayerAnswers")
                        .HasForeignKey("SecondaryAnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.PlayerCard", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.AnswerCard", "AnswerCard")
                        .WithMany("PlayerCards")
                        .HasForeignKey("AnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DAL.Entity.Player", "Player")
                        .WithMany("PlayerCards")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DAL.Entity.QuestionCard", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.Language", "Language")
                        .WithMany("QuestionCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MeanCards.DAL.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MeanCards.DAL.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
