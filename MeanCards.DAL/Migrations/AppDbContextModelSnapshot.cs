﻿// <auto-generated />
using System;
using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MeanCards.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("meancards")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MeanCards.DataModel.Entity.AnswerCard", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("GameCode")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<byte>("GameStatus");

                    b.Property<bool>("IsActive");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("OwnerId");

                    b.Property<bool>("ShowAdultContent");

                    b.HasKey("GameId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.GameRound", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.Language", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.Player", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.PlayerAnswer", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.QuestionCard", b =>
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

            modelBuilder.Entity("MeanCards.DataModel.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsActive");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.AnswerCard", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.Language", "Language")
                        .WithMany("AnswerCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.Game", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.Language", "Language")
                        .WithMany("Games")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.User", "Owner")
                        .WithMany("Games")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.GameRound", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.Game", "Game")
                        .WithMany("GameRounds")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.QuestionCard", "QuestionCard")
                        .WithMany("GameRounds")
                        .HasForeignKey("QuestionCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.Player", "RoundOwner")
                        .WithMany("OwnedGameRounds")
                        .HasForeignKey("RoundOwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.Player", "RoundWinner")
                        .WithMany("WonRounds")
                        .HasForeignKey("RoundWinnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.Player", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.PlayerAnswer", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.AnswerCard", "AnswerCard")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("AnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.GameRound", "GameRound")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("GameRoundId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.Player", "Player")
                        .WithMany("PlayerAnswers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MeanCards.DataModel.Entity.AnswerCard", "SecondaryAnswerCard")
                        .WithMany("SecondaryPlayerAnswers")
                        .HasForeignKey("SecondaryAnswerCardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MeanCards.DataModel.Entity.QuestionCard", b =>
                {
                    b.HasOne("MeanCards.DataModel.Entity.Language", "Language")
                        .WithMany("QuestionCards")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
