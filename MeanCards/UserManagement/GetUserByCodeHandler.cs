using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface IGetUserByCodeHandler
    {
        Task<GetUserByCodeResult> Handle(GetUserByCode request);
    }

    public class GetUserByCodeHandler : IGetUserByCodeHandler
    {
        private readonly IUsersRepository repository;

        public GetUserByCodeHandler(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetUserByCodeResult> Handle(GetUserByCode request)
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
