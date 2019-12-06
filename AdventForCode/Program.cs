using System;
using System.IO;

namespace AdventForCode
{
    internal class Program
    {
        public static string[] ReadInput(string filePath)
        {
            using var stream = new StreamReader(filePath);
            return stream.ReadToEnd().Trim().Split("\n");
        }

        private static void Main()
        {
            var appFolder = new FileInfo(Environment.CurrentDirectory).Directory.Parent.Parent.Parent;

            var day1 = new Day1(Path.Join(appFolder.FullName, "inputDay2.txt"));
            Console.WriteLine(day1.TotalFuelNeeded);
            Console.WriteLine(day1.TotalFuelNeededBetter);

            var day2 = new Day2(Path.Join(appFolder.FullName, "inputDay2.txt"));
            Console.WriteLine(day2.RunChallengePart1());
            Console.WriteLine(day2.RunChallengePart2());

            var day3 = new Day3(Path.Join(appFolder.FullName, "inputDay3.txt"));
            Console.WriteLine(day3.RunChallenge3Part1());
            Console.WriteLine(day3.RunChallenge3Part2());

            var day4 = new Day4("158126-624574");
            Console.WriteLine(day4.RunChallengePart1());
            Console.WriteLine(day4.RunChallengePart2());

            var day5 = new Day5(Path.Join(appFolder.FullName, "inputDay5.txt"));
        }
    }
}
