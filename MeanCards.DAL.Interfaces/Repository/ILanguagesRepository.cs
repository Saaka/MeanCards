using MeanCards.Model.DAL.Creation.Languages;
using MeanCards.Model.DTO.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface ILanguagesRepository
    {
        Task<int> CreateLanguage(CreateLanguageModel model);
        Task CreateLanguages(IEnumerable<CreateLanguageModel> models);
        Task<List<LanguageModel>> GetAllActiveLanguages();
        Task<LanguageModel> GetLanguageByCode(string code);
        Task<bool> HasLanguages();
    }
}
