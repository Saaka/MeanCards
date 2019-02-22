using MediatR;

namespace MeanCards.Model.Core.Users
{
    public class GetUserByCode : IRequest<GetUserByCodeResult>
    {
        public string UserCode { get; set; }
    }
}
