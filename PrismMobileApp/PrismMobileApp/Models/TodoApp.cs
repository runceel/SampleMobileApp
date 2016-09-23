using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismMobileApp.Models
{
    public class TodoApp : BindableBase
    {
        private MobileServiceClient Client { get; }

        private ISQLiteDBPathProvider SQLiteDBPathProvider { get; }

        private ITodoItemRepository TodoItemRepository { get; }

        private IDataSynchronizer DataSynchronizer { get; }

        private IAuthenticator Authenticator { get; }

        public ObservableCollection<TodoItem> TodoItems { get; } = new ObservableCollection<TodoItem>();

        private bool isAuthenticated;

        public bool IsAuthenticated
        {
            get { return this.isAuthenticated; }
            set { this.SetProperty(ref this.isAuthenticated, value); }
        }

        private bool IsInitialized { get; set; }


        public TodoApp(MobileServiceClient client, 
            ISQLiteDBPathProvider sqliteDBPathProvider, 
            IAuthenticator authenticator,
            ITodoItemRepository todoItemRepository,
            IDataSynchronizer dataSynchronizer)
        {
            this.Client = client;
            this.Authenticator = authenticator;
            this.SQLiteDBPathProvider = sqliteDBPathProvider;
            this.TodoItemRepository = todoItemRepository;
            this.DataSynchronizer = dataSynchronizer;
        }

        public async Task InitializeAsync()
        {
            if (this.IsInitialized) { return; }

            var store = new MobileServiceSQLiteStore(this.SQLiteDBPathProvider.GetPath());
            store.DefineTable<TodoItem>();
            await this.Client.SyncContext.InitializeAsync(store);
            this.IsInitialized = true;
        }

        public async Task AuthenticateAsync()
        {
            if (this.IsAuthenticated) { return; }

            this.IsAuthenticated = await this.Authenticator.AuthenticateAsync();
        }

        public Task SyncAsync() => this.DataSynchronizer.SyncAsync();

        public async Task LoadTodoItemsAsync()
        {
            var todoItems = await this.TodoItemRepository.GetAllAsync();
            this.TodoItems.Clear();
            foreach (var item in todoItems)
            {
                this.TodoItems.Add(item);
            }
        }

        public async Task InsertTodoItemAsync(TodoItem todoItem)
        {
            await this.TodoItemRepository.InsertAsync(todoItem);
            await this.LoadTodoItemsAsync();
        }
    }
}
