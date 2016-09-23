using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    class TodoItemRepository : ITodoItemRepository
    {
        private IMobileServiceSyncTable<TodoItem> TodoItemTable { get; }

        public TodoItemRepository(MobileServiceClient client)
        {
            this.TodoItemTable = client.GetSyncTable<TodoItem>();
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return this.TodoItemTable.CreateQuery().OrderBy(x => x.Text).ToEnumerableAsync();
        }

        public Task InsertAsync(TodoItem todoItem)
        {
            return this.TodoItemTable.InsertAsync(todoItem);
        }
    }
}
