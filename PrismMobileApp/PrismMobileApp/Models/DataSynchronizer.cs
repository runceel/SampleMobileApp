using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    class DataSynchronizer : IDataSynchronizer
    {
        private MobileServiceClient Client { get; }

        private IMobileServiceSyncTable<TodoItem> TodoItemTable { get; }

        public DataSynchronizer(MobileServiceClient client)
        {
            this.Client = client;
            this.TodoItemTable = this.Client.GetSyncTable<TodoItem>();
        }

        public async Task SyncAsync()
        {
            try
            {
                await this.Client.SyncContext.PushAsync();
                await this.TodoItemTable.PullAsync("allTodoItem", this.TodoItemTable.CreateQuery());
            }
            catch (MobileServicePushFailedException ex)
            {
                var errors = ex.PushResult?.Errors;
                if (errors != null)
                {
                    foreach (var error in errors)
                    {
                        if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                        {
                            await error.CancelAndUpdateItemAsync(error.Result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
