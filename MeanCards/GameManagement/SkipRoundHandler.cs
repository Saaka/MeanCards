using MeanCards.Model.Core.Games;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISkipRoundHandler
    {
        Task<SkipRoundResult> Handle(SkipRound request);
    }

    public class SkipRoundHandler : ISkipRoundHandler
    {
        public async Task<SkipRoundResult> Handle(SkipRound request)
        {
            throw new NotImplementedException();
        }
    }
}
