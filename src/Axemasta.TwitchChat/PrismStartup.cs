using System.Diagnostics;
using Axemasta.TwitchChat.Abstractions;
using Axemasta.TwitchChat.Helpers;
using Axemasta.TwitchChat.Services;
using Axemasta.TwitchChat.ViewModels;
using Axemasta.TwitchChat.Views;

namespace Axemasta.TwitchChat
{
    internal static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .OnInitialized(OnInitialized)
                .OnAppStart(async navigationService =>
                {
                    var nav = await navigationService.NavigateAsync("NavigationPage/ConnectPage");

                    if (!nav.Success)
                    {
                        Debug.WriteLine("Unable to navigate to first page!");
                    }
                });
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterSingleton<ITwitchChatService, TwitchChatService>();

            // Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ChatPage, ChatViewModel>();
            containerRegistry.RegisterForNavigation<ConnectPage, ConnectViewModel>();
        }

        private static void OnInitialized(IContainerProvider containerProvider)
        {
        }

        private static Task OnAppStart(INavigationService navigationService)
        {
            return Task.CompletedTask;
        }
    }
}
