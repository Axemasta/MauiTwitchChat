using Bogus;

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

	public TwitchChatView()
	{
		InitializeComponent();

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
}