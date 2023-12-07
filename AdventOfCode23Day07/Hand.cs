namespace AdventOfCode23Day07;
internal class Hand : IComparable<Hand>
{
	public Card[] Cards { get; }

	public HandStrength Strength { get; }

	public Hand(IEnumerable<Card> cards)
	{
		Cards = cards.ToArray();
		if (Cards.Length != 5) throw new ArgumentException("Wrong number of cards in hand", nameof(cards));

		if (Cards.Contains(Card.Joker))
		{
			List<Hand> testHands = [];
			foreach (Card replacementCard in Cards.Where(c => c != Card.Joker).Distinct()) //Only have to consider Joker being swapped to other cards in hand.
				testHands.Add(new(Cards.Select(c => c == Card.Joker ? replacementCard : c)));
			if (testHands.Count == 0) //Cards are all Jokers, so 5oak
				Strength = HandStrength.FiveOAK;
			else
				Strength = testHands.Select(h => h.Strength).OrderDescending().First();
		}
		else
		{
			int matches = 0;
			foreach (Card card in Cards)
				matches += Cards.Where(c => c == card).Count() - 1; // -1 to ignore the match with self.

			Strength = (HandStrength)matches;
			if (!Enum.IsDefined(Strength)) throw new Exception("Shouldn't happen. Just double checking algorithm works");
		}
	}

	public int CompareTo(Hand? other)
	{
		if (other == null)
			return 1;

		if (Strength > other.Strength)
			return 1;
		else if (Strength < other.Strength)
			return -1;
		else
		{
			for (int i = 0; i < 5; i++)
			{
				Card thisCard = Cards[i];
				Card otherCard = other.Cards[i];
				if (thisCard != otherCard)
					return thisCard > otherCard ? 1 : -1;
			}
		}
		return 0;
	}
}
