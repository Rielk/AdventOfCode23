using AdventOfCode23Day20;
using AdventOfCode23Day20.Properties;

string input = Resources.Input1;

Network network = new(input.Split(Environment.NewLine));

foreach (int _ in Enumerable.Range(0, 1000))
	network.PressButton();

int lowPulses = network.LowPulses;
int highPulses = network.HighPulses;

long crossOfPulses = lowPulses * highPulses;

Console.WriteLine($"Low pulses sent multiplied by High pulses sent: {crossOfPulses}");
