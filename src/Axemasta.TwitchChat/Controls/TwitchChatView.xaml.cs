using Axemasta.TwitchChat.Helpers;
using Axemasta.TwitchChat.Models;
using Bogus;
using TwitchLib.Client.Models;

namespace Axemasta.TwitchChat.Controls;

public partial class TwitchChatView : ContentView
{
	private static Color[] usernameColors = new Color[]
	{
		Colors.Red,
        Colors.Orange,
        Colors.LimeGreen,
        Colors.Purple,
        Colors.Blue,
        Colors.Pink,
        Colors.Brown,
    };

    private readonly IEventAggregator eventAggregator;

    public TwitchChatView()
        : this(ContainerHelper.Resolve<IEventAggregator>())
    {
    }

	public TwitchChatView(IEventAggregator eventAggregator)
	{
        this.eventAggregator = eventAggregator;

		InitializeComponent();

        this.eventAggregator.GetEvent<ChatMessageEvent>()
            .Subscribe(OnMessageRecieved);
	}

    void OnMessageRecieved(ChatMessage chatMessage)
    {
        var label = new Label()
        {
            FormattedText = FormatChatMessage(chatMessage),
        };

        ChatMessagesStack.Dispatcher.Dispatch(() => ChatMessagesStack.Add(label));

        // Auto scroll when messages fill screen & new messages come in
        if (ChatScrollContainer.Content.Height > ChatScrollContainer.Height)
        {
            var lastElement = ChatMessagesStack.Last() as Element;

            ChatScrollContainer.Dispatcher.Dispatch(() => ChatScrollContainer.ScrollToAsync(lastElement, ScrollToPosition.End, true));
        }
    }

    void ShowDummyMessages()
    {
        var iterations = new Faker().Random.Int(30, 60);

        for (int i = 0; i < iterations; i++)
        {
            var label = new Label();
            label.FormattedText = CreateDummyMessage();
            ChatMessagesStack.Add(label);
        }
    }

    FormattedString CreateDummyMessage()
	{
		var faker = new Faker();

		var usernameSpan = new Span()
		{
			Text = faker.Internet.UserName(),
			TextColor = faker.PickRandom<Color>(usernameColors)
		};

		var chatMessage = new Span()
		{
			Text = faker.Company.Bs(),
		};

		return new FormattedString()
		{
			Spans =
			{
				usernameSpan,
				new Span()
				{
					Text = ": "
				},
				chatMessage,
			}
		};
	}

    FormattedString FormatChatMessage(ChatMessage chatMessage)
    {
        var faker = new Faker();

        var usernameSpan = new Span()
        {
            Text = chatMessage.Username,
            TextColor = faker.PickRandom<Color>(usernameColors)
        };

        var textMessage = new Span()
        {
            Text =chatMessage.Message,
        };

        return new FormattedString()
        {
            Spans =
            {
                usernameSpan,
                new Span()
                {
                    Text = ": "
                },
                textMessage,
            }
        };
    }
}