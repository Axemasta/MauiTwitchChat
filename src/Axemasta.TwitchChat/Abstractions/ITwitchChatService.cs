using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axemasta.TwitchChat.Abstractions
{
    internal interface ITwitchChatService
    {
        void Start(string channelName);
    }
}
