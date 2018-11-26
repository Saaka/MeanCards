using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Model.DTO.Users;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface IGetUserByCredentialsHandler
    {
        Task<GetUserByCredentialsResult> Handle(GetUserByCredentials data);
    }

    public class GetUserByCredentialsHandler : IGetUserByCredentialsHandler
    {
        private readonly IUsersRepository repository;

        public GetUserByCredentialsHandler(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetUserByCredentialsResult> Handle(GetUserByCredentials data)
        {
            var result = await repository.GetUserByCredentials(new Model.DAL.Access.Users.GetUserByCredentialsModel
            {
                Email = data.Email,
                Password = data.Password
            });

            if (!result.IsSuccessful)
                return new GetUserByCredentialsResult(result.Error);

            return new GetUserByCredentialsResult
            { 
                User = new UserModel
                {
                    Code = result.Model.Code,
                    DisplayName = result.Model.DisplayName,
                    Email = result.Model.Email,
                    ImageUrl = result.Model.ImageUrl,
                    UserId = result.Model.UserId
                }
            };
        }
    }
}
