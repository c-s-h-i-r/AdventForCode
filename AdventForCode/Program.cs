using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventForCode
{
    class Program
    {
        public static string[] ReadInput(string filePath)
        {
            using (var stream = new StreamReader(filePath))
            {
                return stream.ReadToEnd().Trim().Split("\n");
            }
        }

        static void Main()
        {
            //Console.WriteLine(day1.TotalFuelNeeded);//3346639
            //var day1 = new Day1( @"C:\Users\Chris\source\repos\AdventForCode Challenge\AdventForCode\inputDay2.txt");
            //Console.WriteLine(day1.TotalFuelNeededBetter);//5017110

            //var day2 = new Day2(@"C:\Users\Chris\source\repos\AdventForCode Challenge\AdventForCode\inputDay2.txt");
            //day2.RunChallengePart1();
            //Console.WriteLine(day2.Output);
            //Console.WriteLine(day2.RunChallengePart2()); //7195
            //var day3 = new Day3(@"C:\Users\Chris\source\repos\AdventForCode Challenge\AdventForCode\inputDay3.txt");
            //Console.WriteLine(day3.RunChallenge3Part1());//1064
            //Console.WriteLine(day3.RunChallenge3Part2());// 25676
            //var day4 = new Day4("158126-624574");
            //Console.WriteLine(day4.RunChallengePart1()); //1665
            //Console.WriteLine(day4.RunChallengePart2()); //1131
        }
    }
}
