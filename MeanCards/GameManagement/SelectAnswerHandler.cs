using MeanCards.Model.Core.Games;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISelectAnswerHandler
    {
        Task<SelectAnswerResult> Handle(SelectAnswer request);
    }

    public class SelectAnswerHandler : ISelectAnswerHandler
    {
        public async Task<SelectAnswerResult> Handle(SelectAnswer request)
        {
            throw new NotImplementedException();
        }
    }
}
