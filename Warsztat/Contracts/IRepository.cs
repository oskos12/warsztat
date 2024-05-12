using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warsztat.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
