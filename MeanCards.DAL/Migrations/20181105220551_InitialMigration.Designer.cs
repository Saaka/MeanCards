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
    [Migration("20181105220551_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("meancards")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeanCards.Model.DataModels.AnswerCard", b =>
                {
                    b.Property<int>("AnswerCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdultContent");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("AnswerCardId");

                    b.HasIndex("LanguageId");

                    b.ToTable("AnswerCards");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<byte>("GameStatus");

                    b.Property<bool>("IsActive");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<bool>("ShowAdultContent");

                    b.HasKey("GameId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.GameRound", b =>
                {
                    b.Property<int>("GameRoundId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("GameId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("QuestionCardId");

                    b.Property<int>("RoundOwnerId");

                    b.Property<int?>("RoundWinnerId");

                    b.HasKey("GameRoundId");

                    b.HasIndex("GameId");

                    b.HasIndex("QuestionCardId");

                    b.HasIndex("RoundOwnerId");

                    b.HasIndex("RoundWinnerId");

                    b.ToTable("GameRounds");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("Points");

                    b.Property<int>("UserId");

                    b.HasKey("PlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.PlayerAnswer", b =>
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

            modelBuilder.Entity("MeanCards.Model.DataModels.QuestionCard", b =>
                {
                    b.Property<int>("QuestionCardId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdultContent");

                    b.Property<int>("LanguageId");

                    b.Property<byte>("NumberOfAnswers");

                    b.Property<string>("Text");

                    b.HasKey("QuestionCardId");

                    b.HasIndex("LanguageId");

                    b.ToTable("QuestionCards");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MappedUserId");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.AnswerCard", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.Language", "Language")
                        .WithMany("AnswerCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.Game", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.Language", "Language")
                        .WithMany("Games")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.User", "Owner")
                        .WithMany("Games")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.GameRound", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.Game", "Game")
                        .WithMany("GameRounds")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.QuestionCard", "QuestionCard")
                        .WithMany("GameRounds")
                        .HasForeignKey("QuestionCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.Player", "RoundOwner")
                        .WithMany("OwnedGameRounds")
                        .HasForeignKey("RoundOwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.Player", "RoundWinner")
                        .WithMany("WonRounds")
                        .HasForeignKey("RoundWinnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.Player", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.PlayerAnswer", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.AnswerCard", "AnswerCard")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("AnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.GameRound", "GameRound")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("GameRoundId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.Player", "Player")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.Model.DataModels.AnswerCard", "SecondaryAnswerCard")
                        .WithMany("SecondaryPlayerAnswers")
                        .HasForeignKey("SecondaryAnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.Model.DataModels.QuestionCard", b =>
                {
                    b.HasOne("MeanCards.Model.DataModels.Language", "Language")
                        .WithMany("QuestionCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
