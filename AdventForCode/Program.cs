using System;
using System.IO;

namespace AdventForCode
{
    internal class Program
    {
        /// <summary>
        /// Read input from given file and return the lines.
        /// </summary>
        public static string[] ReadInput(string filePath, string separator="\n")
        {
            using var stream = new StreamReader(filePath);
            return stream.ReadToEnd().Trim().Split(separator);
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
            Console.WriteLine(string.Join(',',day5.RunChallengePart1()));
            Console.WriteLine(string.Join(',', day5.RunChallengePart2()));
            //var day6 = new Day6(Path.Join(appFolder.FullName, "inputDay6.txt"));
            //Console.WriteLine(day6.RunChallengePart1());
            var day8 = new Day8(Path.Join(appFolder.FullName, "inputDay8.txt"), 25, 6);
            //Console.WriteLine(day8.RunChallengePart1());
            Console.WriteLine(day8.RunChallengePart2());
            
            // 100010110011100100011111010001100101001010001000100101010000100100101000100001001011011100001000100000100100101010000100100000010001110100100010011110
            // 100010110011100100011111010001100101001010001000100101010000100100101000100001001011011100001000100000100100101010000100100000010001110100100010011110
                }
    }
}
