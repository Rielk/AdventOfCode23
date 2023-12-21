using AdventOfCode23Day20;
using AdventOfCode23Day20.Modules;
using AdventOfCode23Day20.Properties;
using AdventOfCode23Factors;

string input = Resources.Input1;

Network network = new(input.Split(Environment.NewLine));

foreach (int _ in Enumerable.Range(0, 1000))
	network.PressButton();

int lowPulses = network.LowPulses;
int highPulses = network.HighPulses;

long crossOfPulses = lowPulses * highPulses;

//Part 2 feels ugly and unsatisfying, but I can't think of the general sollution right now.
Module moduleRx = network.GetModule("rx");
Module rxInput = moduleRx.ReadInputs[0];
Module[] inverters = rxInput.ReadInputs;
int[] firstHigh = inverters.Select(i => i.FindFirstHigh()).ToArray();
long pressesForRXLow = LCM.Find(firstHigh.Select(i => (long)i));

Console.WriteLine($"Low pulses sent multiplied by High pulses sent: {crossOfPulses}");
Console.WriteLine();
Console.WriteLine($"Presses required for a single low pulse to Rx: {pressesForRXLow}");
