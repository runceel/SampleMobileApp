using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Prism.Unity;
using PrismMobileApp.Models;
using PrismMobileApp.Views;
using System;
using System.Net.Http;

namespace PrismMobileApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await this.NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterTypeForNavigation<MainPage>();
            // MobileServiceClientをシングルトンで登録
            this.Container.RegisterType<MobileServiceClient>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    //new Uri("https://okazuki0920.azurewebsites.net"),
                    new Uri("http://169.254.80.80:1782/"),
                    new HttpMessageHandler[] { }),
                new InjectionProperty(nameof(MobileServiceClient.AlternateLoginHost), new Uri("https://okazuki0920.azurewebsites.net")));
            this.Container.RegisterType<TodoApp>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IDataSynchronizer, DataSynchronizer>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ITodoItemRepository, TodoItemRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
