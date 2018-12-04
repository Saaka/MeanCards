﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Players;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface IJoinGameHandler
    {
        Task<JoinGameResult> Handle(JoinGame request);
    }

    public class JoinGameHandler : IJoinGameHandler
    {
        protected readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        protected readonly IPlayersRepository playersRepository;
        protected readonly IPlayerCardsRepository playerCardsRepository;
        protected readonly IAnswerCardsRepository answerCardsRepository;

        public JoinGameHandler(IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IPlayersRepository playersRepository,
            IPlayerCardsRepository playerCardsRepository,
            IAnswerCardsRepository answerCardsRepository)
        {
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.playersRepository = playersRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.answerCardsRepository = answerCardsRepository;
        }

        public async Task<JoinGameResult> Handle(JoinGame request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var maxNumber = await playersRepository.GetMaxPlayerNumberForGame(request.GameId);
                var player = await CreatePlayer(request.GameId, request.UserId, ++maxNumber);

                var cardCount = await CreatePlayerAnswerCards(request.GameId, player.PlayerId, GameConstants.StartingCardsCount);
                if (cardCount != GameConstants.StartingCardsCount)
                    return new JoinGameResult(GameErrors.NotEnoughAnswerCards);

                transaction.CommitTransaction();

                return new JoinGameResult
                {
                    PlayerId = player.PlayerId
                };
            }
        }

        private async Task<int> CreatePlayerAnswerCards(int gameId, int playerId, int count)
        {
            var cards = await answerCardsRepository.GetRandomAnswerCardsForGame(gameId, count);

            var playerCards = cards.Select(c => new CreatePlayerCardModel
            {
                PlayerId = playerId,
                AnswerCardId = c.AnswerCardId
            }).ToList();

            return await playerCardsRepository.CreatePlayerCards(playerCards);
        }

        private async Task<PlayerModel> CreatePlayer(int gameId, int userId, int number)
        {
            var player = await playersRepository.CreatePlayer(new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId,
                Number = number
            });

            return player;
        }
    }
}
