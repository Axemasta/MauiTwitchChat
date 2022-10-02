using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Axemasta.TwitchChat.Abstractions;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Axemasta.TwitchChat.ViewModels
{
    internal class ChatViewModel : ViewModelBase
    {
        private readonly ITwitchChatService twitchChatService;

        public ChatViewModel(INavigationService navigationService, ITwitchChatService twitchChatService)
            : base(navigationService)
        {
            this.twitchChatService = twitchChatService;

            Title = "Axemasta's Chat";

            this.twitchChatService.Start("riotgames");
        }
    }
}
