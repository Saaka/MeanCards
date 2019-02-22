using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface IGetUserByCodeHandler : IRequestHandler<GetUserByCode, GetUserByCodeResult>
    {
    }

    public class GetUserByCodeHandler : IGetUserByCodeHandler
    {
        private readonly IUsersRepository repository;

        public GetUserByCodeHandler(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetUserByCodeResult> Handle(GetUserByCode request, CancellationToken cancellationToken)
        {
            var result = await repository.GetUserByCode(request.UserCode);
            if (!result.IsSuccessful)
                return new GetUserByCodeResult(result.Error);

            return new GetUserByCodeResult
            {
                User = result.Model
            };
        }
    }
}
