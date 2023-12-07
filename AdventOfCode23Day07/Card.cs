namespace AdventOfCode23Day07;
internal enum Card
{
	Ace = 14,
	King = 13,
	Queen = 12,
	Jack = 11,
	Ten = 10,
	Nine = 9,
	Eight = 8,
	Seven = 7,
	Six = 6,
	Five = 5,
	Four = 4,
	Three = 3,
	Two = 2
}

internal static class CardExtensions
{
	public static Card ToCard(this char c) => c switch
	{
		'A' => Card.Ace,
		'K' => Card.King,
		'Q' => Card.Queen,
		'J' => Card.Jack,
		'T' => Card.Ten,
		'9' => Card.Nine,
		'8' => Card.Eight,
		'7' => Card.Seven,
		'6' => Card.Six,
		'5' => Card.Five,
		'4' => Card.Four,
		'3' => Card.Three,
		'2' => Card.Two,
		_ => throw new ArgumentException($"\"{c}\" is not a card", nameof(c))
	};

	public static int Value(this Card card) => (int)card;
}
