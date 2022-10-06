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
    internal class ChatViewModel : ViewModelBase, IInitialize
    {
        public ChatViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("ChannelName", out string channelName))
            {
                Title = string.Format("{0}'s Chat", channelName);
            }
        }
    }
}
