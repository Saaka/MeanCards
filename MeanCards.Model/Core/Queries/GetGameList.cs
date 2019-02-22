using MediatR;

namespace MeanCards.Model.Core.Queries
{
    public class GetGameList : IRequest<GetGameListResult>
    {
        public int UserId { get; set; }
    }
}
