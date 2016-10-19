using System.Collections.Generic;
using System.Threading.Tasks;
namespace AzureSqlDotnetCore
{
    public interface IProduceItems<T>
    {
        Task<IEnumerable<T>> GetItemsAsync();
        IEnumerable<T> GetItems();

    }
}