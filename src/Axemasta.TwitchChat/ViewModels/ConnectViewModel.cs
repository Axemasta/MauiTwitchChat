using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Axemasta.TwitchChat.Abstractions;

namespace Axemasta.TwitchChat.ViewModels
{
    internal class ConnectViewModel : ViewModelBase
    {
        private readonly ITwitchChatService twitchChatService;

        public ICommand ConnectCommand { get; }

        private string channelName;
        public string ChannelName
        {
            get => channelName;
            set => SetProperty(ref channelName, value);
        }

        public ConnectViewModel(
            INavigationService navigationService,
            ITwitchChatService twitchChatService)
            : base(navigationService)
        {
            this.twitchChatService = twitchChatService;

            ConnectCommand = new DelegateCommand(() => CommandWrapper(OnConnect()).Await())
                .ObservesCanExecute(() => CanExecuteCommand);
        }

        private async Task OnConnect()
        {
            if (string.IsNullOrEmpty(channelName))
            {
                return;
            }

            twitchChatService.Start(channelName);

            var navParams = new NavigationParameters()
            {
                { "ChannelName", channelName }
            };

            var nav = await navigationService.NavigateAsync("ChatPage", navParams);

            if (!nav.Success)
            {
                Debugger.Break();
            }
        }

        private async Task CommandWrapper(Task action)
        {
            IsBusy = true;

            try
            {
                await action;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
