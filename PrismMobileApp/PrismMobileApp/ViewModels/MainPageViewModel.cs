using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismMobileApp.Models;
using System.Collections.ObjectModel;

namespace PrismMobileApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private TodoApp TodoApp { get; }

        private string text;

        public string Text
        {
            get { return this.text; }
            set { this.SetProperty(ref this.text, value); }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetProperty(ref this.isRefreshing, value); }
        }

        public ObservableCollection<TodoItem> TodoItems => this.TodoApp.TodoItems;

        public DelegateCommand AddCommand { get; }

        public DelegateCommand RefreshCommand { get; }

        public MainPageViewModel(TodoApp todoApp)
        {
            this.TodoApp = todoApp;

            this.AddCommand = new DelegateCommand(async () =>
                {
                    await this.TodoApp.InsertTodoItemAsync(new TodoItem { Text = this.Text });
                    this.Text = "";
                    await this.TodoApp.LoadTodoItemsAsync();
                }, () => !string.IsNullOrWhiteSpace(this.Text) && this.TodoApp.IsAuthenticated)
                .ObservesProperty(() => this.Text);

            this.RefreshCommand = new DelegateCommand(async () =>
            {
                await this.TodoApp.SyncAsync();
                await this.TodoApp.LoadTodoItemsAsync();
                this.IsRefreshing = false;
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await this.TodoApp.InitializeAsync();
            await this.TodoApp.AuthenticateAsync();
            await this.TodoApp.SyncAsync();
            await this.TodoApp.LoadTodoItemsAsync();
        }
    }
}
