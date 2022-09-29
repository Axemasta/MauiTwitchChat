using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axemasta.TwitchChat.ViewModels
{
    internal class ChatViewModel : ViewModelBase
    {
        public ChatViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Axemasta's Chat";
        }
    }
}
