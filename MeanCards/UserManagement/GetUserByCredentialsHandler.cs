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
            var user = await repository.GetUserByCredentials(new Model.DAL.Access.Users.GetUserByCredentialsModel
            {
                Email = data.Email,
                Password = data.Password
            });

            if (user == null)
                return new GetUserByCredentialsResult(AccessErrors.InvalidUserCredentials);

            return new GetUserByCredentialsResult
            { 
                User = new UserModel
                {
                    Code = user.Code,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    UserId = user.UserId
                }
            };
        }
    }
}
