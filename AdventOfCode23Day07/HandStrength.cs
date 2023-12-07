namespace AdventOfCode23Day07;
internal enum HandStrength
{
	//Corresponding int is also the number of matches between other cards
	HighCard = 0,
	OnePair = 2,
	TwoPair = 4,
	ThreeOAK = 6,
	FullHouse = 8,
	FourOAK = 12,
	FiveOAK = 20
}
