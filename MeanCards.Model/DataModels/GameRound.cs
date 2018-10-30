﻿using System;

namespace MeanCards.Model.DataModels
{
    public class GameRound
    {
        public int GameRoundId { get; set; }
        public int GameId { get; set; }
        public int QuestionCardId { get; set; }
        public int RoundOwnerId { get; set; }
        public bool IsActive { get; set; }
        public int? RoundWinnerId { get; set; }
        public DateTime CreateDate { get; set; }

        public Game Game { get; set; }
        public QuestionCard QuestionCard { get; set; }
        public Player RoundOwner { get; set; }
        public Player RoundWinner { get; set; }
    }
}
