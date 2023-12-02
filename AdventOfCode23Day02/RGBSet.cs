
namespace AdventOfCode23Day02;
internal record RGBSet(int Red, int Green, int Blue)
{
	public int Power => Red * Green * Blue;

	public bool CanDraw(RGBSet testSet) => Red >= testSet.Red && Green >= testSet.Green && Blue >= testSet.Blue;
}
