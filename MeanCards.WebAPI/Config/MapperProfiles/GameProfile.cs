using AutoMapper;
using MeanCards.Model.Core.Queries;
using MeanCards.ViewModel.Game;

namespace MeanCards.WebAPI.Config.MapperProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GetGameResult, GetGameResponse>();

        }
    }
}
