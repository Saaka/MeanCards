using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface ILanguagesRepository
    {
        Task<int> CreateLanguage(CreateLanguageModel model);
        Task CreateLanguages(IEnumerable<CreateLanguageModel> models);
        Task<List<Language>> GetAllActiveLanguages();
        Task<bool> HasLanguages();
    }
}
