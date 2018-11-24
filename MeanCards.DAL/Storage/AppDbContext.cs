using MeanCards.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeanCards.DAL.Storage
{
    public class AppDbContext
        : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public const string DefaultSchema = "meancards";
        public const string DefaultMigrationsTable = "Migrations_MeanCards";

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameRound> GameRounds { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerCard> PlayersCards { get; set; }
        public DbSet<PlayerAnswer> PlayerAnswers { get; set; }
        public DbSet<AnswerCard> AnswerCards { get; set; }
        public DbSet<QuestionCard> QuestionCards { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Configure(modelBuilder);
            InitTables(modelBuilder);
        }

        private void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
        }

        private void InitTables(ModelBuilder builder)
        {
            builder.Entity<Game>(b =>
            {
                b.HasKey(x => x.GameId);
                b.HasOne(x => x.Owner)
                    .WithMany(x => x.Games)
                    .HasForeignKey(x => x.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasMany(x => x.GameRounds)
                    .WithOne(x => x.Game)
                    .HasForeignKey(x => x.GameId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Language)
                    .WithMany(x => x.Games)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasIndex(x => x.Code)
                    .IsUnique();
            });
            builder.Entity<GameRound>(b =>
            {
                b.HasKey(x => x.GameRoundId);
                b.HasMany(x => x.PlayerAnswers)
                    .WithOne(x => x.GameRound)
                    .HasForeignKey(x => x.GameRoundId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.RoundOwner)
                    .WithMany(x => x.OwnedGameRounds)
                    .HasForeignKey(x => x.RoundOwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.RoundWinner)
                    .WithMany(x => x.WonRounds)
                    .HasForeignKey(x => x.RoundWinnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.QuestionCard)
                    .WithMany(x => x.GameRounds)
                    .HasForeignKey(x => x.QuestionCardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Player>(b =>
            {
                b.HasKey(x => x.PlayerId);
                b.HasMany(x => x.OwnedGameRounds)
                    .WithOne(x => x.RoundOwner)
                    .HasForeignKey(x => x.RoundOwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasMany(x => x.WonRounds)
                    .WithOne(x => x.RoundWinner)
                    .HasForeignKey(x => x.RoundWinnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Game)
                    .WithMany(x => x.Players)
                    .HasForeignKey(x => x.GameId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<PlayerCard>(b =>
            {
                b.HasKey(x => x.PlayerCardId);
                b.HasOne(x => x.Player)
                    .WithMany(x => x.PlayerCards)
                    .HasForeignKey(x => x.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.AnswerCard)
                    .WithMany(x => x.PlayerCards)
                    .HasForeignKey(x => x.AnswerCardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<PlayerAnswer>(b =>
            {
                b.HasKey(x => x.PlayerAnswerId);
                b.HasOne(x => x.Player)
                    .WithMany(x => x.PlayerAnswers)
                    .HasForeignKey(x => x.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.GameRound)
                    .WithMany(x => x.PlayerAnswers)
                    .HasForeignKey(x => x.GameRoundId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.SecondaryAnswerCard)
                    .WithMany(x => x.SecondaryPlayerAnswers)
                    .HasForeignKey(x => x.SecondaryAnswerCardId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.AnswerCard)
                    .WithMany(x => x.PlayerAnswers)
                    .HasForeignKey(x => x.AnswerCardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<QuestionCard>(b =>
            {
                b.HasKey(x => x.QuestionCardId);
                b.HasMany(x => x.GameRounds)
                    .WithOne(x => x.QuestionCard)
                    .HasForeignKey(x => x.QuestionCardId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.Language)
                    .WithMany(x => x.QuestionCards)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Language>(b =>
            {
                b.HasKey(x => x.LanguageId);
                b.HasMany(x => x.Games)
                    .WithOne(x => x.Language)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasMany(x => x.AnswerCards)
                    .WithOne(x => x.Language)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasMany(x => x.QuestionCards)
                    .WithOne(x => x.Language)
                    .HasForeignKey(x => x.LanguageId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
