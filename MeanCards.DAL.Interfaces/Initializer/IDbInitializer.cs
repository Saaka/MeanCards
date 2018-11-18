using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Initializer
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
