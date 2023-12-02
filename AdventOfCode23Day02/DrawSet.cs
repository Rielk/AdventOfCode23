namespace AdventOfCode23Day02;
internal record DrawSet(int Red, int Green, int Blue)
{
	public bool CanDraw(DrawSet testSet) => Red >= testSet.Red && Green >= testSet.Green && Blue >= testSet.Blue;
}
