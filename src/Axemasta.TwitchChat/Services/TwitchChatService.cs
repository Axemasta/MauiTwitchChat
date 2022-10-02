using Axemasta.TwitchChat.Abstractions;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Axemasta.TwitchChat.Services
{
    internal class TwitchChatService : ITwitchChatService
    {
        TwitchClient client;

        private readonly ILogger logger;

        public TwitchChatService(ILogger<TwitchChatService> logger)
        {
            this.logger = logger;
        }

        public void Start(string channelName)
        {
            logger.LogInformation("Connecting to channel {channelName}", channelName);

            try
            {
                ConnectionCredentials credentials = new ConnectionCredentials("username", "oauth_token");

                var clientOptions = new ClientOptions
                {
                    MessagesAllowedInPeriod = 750,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };

                WebSocketClient customClient = new WebSocketClient(clientOptions);

                client = new TwitchClient(customClient);
                client.Initialize(credentials, channelName);

                client.OnLog += Client_OnLog;
                client.OnConnected += Client_OnConnected;
                client.OnMessageReceived += Client_OnMessageReceived;

                client.Connect();

                logger.LogInformation("Connected to TwitchClient successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred connecting to twitch chat");
            }
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            logger.LogInformation($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            logger.LogInformation($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            logger.LogInformation(e.ChatMessage.Username + " " + e.ChatMessage.Message);
        }
    }
}
