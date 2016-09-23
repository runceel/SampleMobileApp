using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();

        Task InsertAsync(TodoItem todoItem);
    }
}
