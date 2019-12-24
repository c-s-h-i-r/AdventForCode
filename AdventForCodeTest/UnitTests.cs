using AdventForCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventForCodeTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestDay1()
        {
            Assert.AreEqual(2, Day1.CalcFuelNeeded(12));
            Assert.AreEqual(2, Day1.CalcFuelNeeded(14));
            Assert.AreEqual(654, Day1.CalcFuelNeeded(1969));
            Assert.AreEqual(33583, Day1.CalcFuelNeeded(100756));

            Assert.AreEqual(2, Day1.CalcFuelNeededPart2(new string[] { "14" }));
            Assert.AreEqual(966, Day1.CalcFuelNeededPart2(new string[] { "1969" }));
            Assert.AreEqual(50346, Day1.CalcFuelNeededPart2(new string[] { "100756" }));
        }

        [TestMethod]
        public void TestDay2()
        {
            var computer = new Computer();
            computer.Memory = new long[] { 1, 0, 0, 0, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 2, 0, 0, 0, 99 }, computer.Memory);
            computer.Memory = new long[] { 2, 3, 0, 3, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 2, 3, 0, 6, 99 }, computer.Memory);
            computer.Memory = new long[] { 2, 4, 4, 5, 99, 0 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 2, 4, 4, 5, 99, 9801 }, computer.Memory);
            computer.Memory = new long[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 30, 1, 1, 4, 2, 5, 6, 0, 99 }, computer.Memory);
        }

        [TestMethod]
        public void TestDay3()
        {
            var day3 = new Day3("");

            var wire1a = new string[] { "R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72" };
            var wire2a = new string[] { "U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83" };
            Assert.AreEqual(159, day3.FindClosestDistanceToOrigin(wire1a, wire2a));

            var wire1b = new string[] { "R98", "U47", "R26", "D63", "R33", "U87", "L62", "D20", "R33", "U53", "R51" };
            var wire2b = new string[] { "U98", "R91", "D20", "R16", "D67", "R40", "U7", "R15", "U6", "R7" };
            Assert.AreEqual(135, day3.FindClosestDistanceToOrigin(wire1b, wire2b));

            Assert.AreEqual(610, day3.FindIntersectionWithLeastSteps(wire1a, wire2a));
            Assert.AreEqual(410, day3.FindIntersectionWithLeastSteps(wire1b, wire2b));
        }

        [TestMethod]
        public void TestDay4()
        {
            var day4 = new Day4("0-" + int.MaxValue.ToString());
            Assert.IsTrue(day4.MeetsCriteria("111111"));
            Assert.IsFalse(day4.MeetsCriteria("223450")); //223450 does not meet these criteria(decreasing pair of digits 50).
            Assert.IsFalse(day4.MeetsCriteria("123789")); //123789 does not meet these criteria(no double).

            //It is a six-digit number.
            Assert.IsFalse(day4.MeetsCriteria("000000"));
            Assert.IsFalse(day4.MeetsCriteria("123456"));
            Assert.IsFalse(day4.MeetsCriteria("000001"));
            Assert.IsTrue(day4.MeetsCriteria("999999"));
            Assert.IsFalse(day4.MeetsCriteria("088888"));
            Assert.IsFalse(day4.MeetsCriteria("001299"));
            Assert.IsFalse(day4.MeetsCriteria("12378"));
            Assert.IsFalse(day4.MeetsCriteria("1237"));
            Assert.IsFalse(day4.MeetsCriteria("001237"));
            Assert.IsFalse(day4.MeetsCriteria("011237"));
            Assert.IsFalse(day4.MeetsCriteria("9999"));
            Assert.IsFalse(day4.MeetsCriteria("1000000"));
            Assert.IsFalse(day4.MeetsCriteria("344322"));
            Assert.IsFalse(day4.MeetsCriteria("90999"));
            Assert.IsTrue(day4.MeetsCriteria("113333"));
            Assert.IsTrue(day4.MeetsCriteria("113788"));
            Assert.IsTrue(day4.MeetsCriteria("134588"));
            Assert.IsTrue(day4.MeetsCriteria("135578"));
            Assert.IsTrue(day4.MeetsCriteria("136688"));
            Assert.IsTrue(day4.MeetsCriteria("136778"));
            Assert.IsTrue(day4.MeetsCriteria("136788"));
            Assert.IsTrue(day4.MeetsCriteria("136799"));

            //112233 meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
            Assert.IsTrue(day4.MeetsCriteriaUpdated("112233"));
            //123444 no longer meets the criteria (the repeated 44 is part of a larger group of 444).
            Assert.IsFalse(day4.MeetsCriteriaUpdated("123444"));
            //111122 meets the criteria (even though 1 is repeated more than twice, it still contains a double 22).
            Assert.IsTrue(day4.MeetsCriteriaUpdated("111122"));
            Assert.IsTrue(day4.MeetsCriteria("111111"));
            Assert.IsFalse(day4.MeetsCriteria("223450")); //223450 does not meet these criteria(decreasing pair of digits 50).
            Assert.IsFalse(day4.MeetsCriteria("123789")); //123789 does not meet these criteria(no double).

            //It is a six-digit number.
            Assert.IsFalse(day4.MeetsCriteriaUpdated("000000"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("123456"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("000001"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("999999"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("088888"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("001299"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("12378"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("1237"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("001237"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("011237"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("9999"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("1000000"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("344322"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("90999"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("113333"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("113788"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("134588"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("135578"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("136688"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("136778"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("136788"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("136799"));
            //Two adjacent digits are the same (like 22 in 122345).
            //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
            day4 = new Day4("158126-624574");
            Assert.IsFalse(day4.MeetsCriteria("158125"));
            Assert.IsFalse(day4.MeetsCriteria("624574"));
            Assert.IsFalse(day4.MeetsCriteria("624575"));
            Assert.IsTrue(day4.MeetsCriteria("223679"));
            Assert.IsFalse(day4.MeetsCriteria("158126"));
            Assert.IsFalse(day4.MeetsCriteria("624574"));

            //The value is within the range given in your puzzle input.
            Assert.IsFalse(day4.MeetsCriteriaUpdated("1000000"));
            day4 = new Day4("158126-624574");
            Assert.IsFalse(day4.MeetsCriteriaUpdated("158125"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("624574"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("624575"));
            Assert.IsTrue(day4.MeetsCriteriaUpdated("223679"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("158126"));
            Assert.IsFalse(day4.MeetsCriteriaUpdated("624574"));

            //The value is within the range given in your puzzle input.
            Assert.IsFalse(day4.MeetsCriteriaUpdated("1000000"));
        }

        [TestMethod]
        public void TestDay5()
        {
            var computer = new Computer("04,0,99");
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 4 }, computer.DiagnosticOutput);
            computer = new Computer("1002,4,3,4,33");
            CollectionAssert.AreEqual(new List<long>() { 1002, 4, 3, 4, 33 }, computer.Memory);
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 1002, 4, 3, 4, 99 }, computer.Memory);

            //For example, here are several programs that take one input, compare it to the value 8, and then produce one output:
            // - Using position mode, consider whether the input is equal to 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,9,8,9,10,9,4,9,99,-1,8");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 8 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -8 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 0 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);

            // - Using position mode, consider whether the input is less than 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,9,7,9,10,9,4,9,99,-1,8");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 8 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 7 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -8 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 9 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);

            // - Using immediate mode, consider whether the input is equal to 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 8 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 7 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -8 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 9 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);

            //Here are some jump tests that take an input, then output 0 if the input was zero or 1 if the input was non-zero:
            // (using position mode)
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 0 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 1 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -8 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 9 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);

            // (using immediate mode)
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 0 }));
            CollectionAssert.AreEqual(new List<long>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 1 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -8 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 9 }));
            CollectionAssert.AreEqual(new List<long>() { 1 }, computer.DiagnosticOutput);

            //This example program uses an input instruction to ask for a single number.
            //The program will then
            //output 999 if the input value is below 8,
            //output 1000 if the input value is equal to 8,
            //or output 1001 if the input value is greater than 8.
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 0 }));
            CollectionAssert.AreEqual(new List<long>() { 999 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 8 }));
            CollectionAssert.AreEqual(new List<long>() { 1000 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 9 }));
            CollectionAssert.AreEqual(new List<long>() { 1001 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { -1 }));
            CollectionAssert.AreEqual(new List<long>() { 999 }, computer.DiagnosticOutput);
        }

        [TestMethod]
        public void TestDay6()
        {
            //For example, suppose you have the following map:
            //COM)B
            //B)C
            //C)D
            //D)E
            //E)F
            //B)G
            //G)H
            //D)I
            //E)J
            //J)K
            //K)L
            //Visually, the above map of orbits looks like this:
            //        G - H       J - K - L
            //       /           /
            //COM - B - C - D - E - F
            //               \
            //                I
            //In this visual representation, when two objects are connected by a line, the one on the right directly orbits the one on the left.
            //Here, we can count the total number of orbits as follows:

            //D directly orbits C and indirectly orbits B and COM, a total of 3 orbits.
            //L directly orbits K and indirectly orbits J, E, D, C, B, and COM, a total of 7 orbits.
            //COM orbits nothing.
            //The total number of direct and indirect orbits in this example is 42.
            var day6 = new Day6();
            day6.LoadOrbits("COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L\r\n");
            Assert.AreEqual(12, day6.allObjects.Count);
            Assert.AreEqual(0, day6.allObjects["COM"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["B"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["C"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["D"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["E"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["F"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["G"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["H"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["I"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["J"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["K"].Orbitting.Count);
            Assert.AreEqual(1, day6.allObjects["L"].Orbitting.Count);
            //Assert.AreEqual(42, day6.CalcOrbits());
        }

        [TestMethod]
        public void TestDay7()
        {
            int[] input = { 1, 2, 3 };
            var result = new List<int[]>();
            Util.FindPermutations(input, result);
            Assert.AreEqual(6, result.Count);

            var day7 = new Day7("");
            long lastOutputSignal = 0;
            Assert.AreEqual(43210, day7.RunProgramThroughAmplifiers(new int[] { 4, 3, 2, 1, 0 }, 0, ref lastOutputSignal, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0"));

            Assert.AreEqual(43210, day7.FindMaxSignal("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", new int[] { 4, 3, 2, 1, 0 }));
            Assert.AreEqual(54321, day7.FindMaxSignal("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", new int[] { 0, 1, 2, 3, 4 }));
            Assert.AreEqual(65210, day7.FindMaxSignal("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", new int[] { 1, 0, 4, 3, 2 }));

            //Max thruster signal 139629729 (from phase setting sequence 9,8,7,6,5):

            //3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
            //27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5
            //Max thruster signal 18216 (from phase setting sequence 9,7,8,5,6):

            //3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
            //-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
            //53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10
        }

        [TestMethod]
        public void TestDay8()
        {
            //For example, given an image 2 pixels wide and 2 pixels tall, the image data 0222112222120000 corresponds to the following image layers:
            //Layer 1: 02
            //         22
            //Layer 2: 11
            //         22
            //Layer 3: 22
            //         12
            //Layer 4: 00
            //         00
            //Then, the full image can be found by determining the top visible pixel in each position:

            //The top-left pixel is black because the top layer is 0.
            //The top-right pixel is white because the top layer is 2 (transparent), but the second layer is 1.
            //The bottom-left pixel is white because the top two layers are 2, but the third layer is 1.
            //The bottom-right pixel is black because the only visible pixel in that position is 0 (from layer 4).
            //So, the final image looks like this:
            //01
            //10
            var day8 = new Day8("", 2, 2);
            Assert.AreEqual("0110", string.Join("", day8.ConvertToImage(day8.SeparateIntoLayers("0222112222120000"))));
            day8 = new Day8("", 25, 6);
            Assert.AreEqual("100010110011100100011111010001100101001010001000100101010000100100101000100001001011011100001000100000100100101010000100100000010001110100100010011110",
                string.Join("", day8.ConvertToImage(day8.SeparateIntoLayers("222222222222222122222222222222122122022022222222120202222202202220202222202022022022222222022202222222102122222212220222122022022222222202222222212222222222222222222022222222222222122022222222222222100212222202222221202222212122022022022222022202222222022022222202221222122222022222222202222222222222222222222222222122222222222222022022022122222222012202222222202220202222222022122222222220122222222222122022222212222222122222022222222212222222202222222222222222222122222222222222022222122122222222020202222222222222222222202222222122022221222202222222212122222212221222222122022222222202222222222222222222222222222022222222222222222222022122222022212212222222222220222222222122122122022221222222222222102022222212222222022022222221222222222222222222222222222222222222222222222222022122222022222122202212222222212221222222202122122222122220022222222222202222222212221222022022222220222202222222202222222222222222222222222222222222222122122022222022000222222222202221222222212122122122022220222202222222002222222222220222122222022221222222222222222222222222222222222122222222222222022022022222222022111212222202202220202220222022022022222221222212222222112022222212222222222022022222222222222222212222222222222222222022222222222222222022222122022222011212222212212220201221212222222022222222222222222222202122222202220222222222022222222222222222212222222222222222220222222222222222122222122022122022122202222222222221222220222222122122122221222212222222222022222202220222222222022222222202222222012222222222222222220122222222222222122222222222022122202212222212212222212220222122022222222222122222222222002122222202222222222022222221222222222222012222222222222222220022222222222222022222122122022022101202222222212221210221212122122022222220222222222222202022222222220222122022222222222212222222012222222222222222222122222222222222022122222122122222222222222212212222210222212222122022022222222212222222012122222222222222222222022222222222222222002222222222222022220222222222222222222222022122122022222212222222222220212221222122122122122221222212222222222222222212220222022122122220222222222222102222222222222022220022222222222222022222022222122122200222222202222220222221202222122222022222022202222222202022222210222222122222022220222222222222212222222222222122221122222222222222022022122222022222200222222222222220222220212222122222222221122222220022202022222200220222022022022220222212222222002222222222222222220222222222222222022122122122222022110212222212212220220220202122222122122221222212221122022222222210221222022022222222222202222222222222222222222222220022222222222222122222122222222022101222222202202222201220212122022122122220222202220222112022222202221222122122222221222202222222122222222222222122222222222222222222022122122222222222222202222202202220202221202222222222022222222212220222012222222202220222222122222222222202222222212222222222222122221122222222222222022222022022222122121202222202212222221220212222122022022221122012222122202222222220221222222222222222222202222222112222222222222222220022222222222222222122022122122222021222222212202220211222202122122122222221022102122022102022222211221222022122222221222222222222102222222222222022220222222222222222122222222222222222210202222212222221201221212022222022222222222022222222122222222210220222022122020220222212222222112222222222222222222022222222222222222022122122122122110222222212222222221220212222122222022220221202020222122222222220220222022022020220222222222222202222222222222222222222222222222222222122122122122022112212222212202221201221212022222222122222022222122222102122222212222222122122222221222222222222112222222222222222222022222222222222222122022222022222101212222202222222211221212022122022022222020002121222202022222201221222122022220220222212222222222222222212222122222122222222222222122022122222222022200212222202202221211221222022022022122220120102022122022222222212222222222022122222222222222222222222222212222122222222222222222222022022222022222122212222222222202221211221212022222022222220221202121022022222222211220222122222020222222202222222012222222212222122222122222222222222220122222122022222000202222222222222220221212122022022222222220212120022202122222201220222222122020222222202222222102222222222222022221022222222222222122122222122022122210222222212202221202222212222122122222222122212022122002022222200222222022122222222222222222222202222222202222222221022222222222222220122022022122022100212222222222221200220202122022022122221020012021022012022222220220222222122122221222122222222112222222212222222221122222222022222221122022222022022211212222222212220201220212222122222222222020202122022022022222222220222222222222221222112222222222222202202222122221222222222112222020222222022022022201202222202212222221222222222022022122221021212122222212222222220221222022122221220222022222222002222222212222122221222222222022222020122022220222022222202222212212222201222212022122022222221221122020222112222222202220222222122022222222222222222222222212202222022220222222222202222022122122222122022102212222212222222212220212222222022222222221112120022212022222001220222222122121221222102222222202222222202222122220222222222102222020222122120222222202222222212222222211222212022022222022220122222220222212022222110221222122222221221222202222220122222212212222222221022222222122222120022222221122022210202222202222222222220212022022122022221021022121222112122222000220222122122222220222102222221002222222222222222220222222222112222022222022021222222212202222212222222202221202022022122022222120102121022012222222002221222022022020220222012222221112222202212222222221022222222012222120222222022222122202202222212212222221221212222022222122222022022022122212222222101222222222222020221222222222220022222222202222022220222222222100222222022122020102022002212222212212222201222212022222022222220222112020022002122222102222222222122122222222222222221212222212202222222222122222222222222221122122022101022112212222202212222200221202122122122122221021012121222002122222122220222222222120220222112222211102222202222222022220122222222100222122022222122000022200222222222212221200222222022122022022220221102022222102220222222220222222102122221222212222211222212212212222022220222222222211222022222122221020222111212222212212220221221222222222121122220121222120022022220222100222222222222022222222202222201012222212202222122220122222222221222121222022121022222201222222202202221201221202222122020022222021112022222002220222002220222122222122221222112222210112222202202222222221122222222211222120122222122210022002212222212202222220222202022122221222221120122221222002022222211220222222102122222222102222220212202212220222222220022222222220222021122021022102222122222222222202220221222202221122020022222222012022122112220222001222222022112120221222112222200202202222220222022220222222222202222120122021021121122220212222202222222212222212120022122022222221012021022112221222010220222122002220220222112222221002202202220222022220122212222220221020222221220020022211212222212202222202221222220122220022222120202020022012021222011222222022122022222222102222220222222222221222022222122202222100222221222121020110222010222222200212221220222222002022021222220020112220222012222222020222222222212022220222102222220102222202200222222222022222221022221222122022220201022001202222212212221212220202011222121022220120012220122212021222021020222222102022222222222222202212212212211222022221022202222101222222122020121010122211212222220212220221222202121022021122222121012022122222221222110220222122212122221222222222201122202222202222122220122222220101222121222021021111022001222222212212220211222212210022221022221120022122222212021222220221222022112122220222022222212002222220201222022220222202222210220122122021221221222122212222201202222211220222222222222122220222122222222122122222200221222022002022222222012222202212222221212222022221022222221020221120222021121101222110202222212202221211222202112222222122221001202222122122220222111120222022122220221222112222200022212210211222122221222202221200220021122221021122122020222222210202222212222202102022120022221112002121122022022222001122222022212220222222022222220202202211210222122220122212212001220121122221222010022121212222202212221210221212212122121222220112022021122212121222111120222022202220221222212222211112122221220222222222222212210021222121122020020210122101212212221212220221222202011022122122220111022022022202222222121222222222122221222212012222202122212220210222212221022222200000222020222122121201222201222202221212221222220222010222220222221212112122022002020222022120222122222022222212022222211202222222220122202221122222221102221122122021121221022120212222221222222201222202120222122022220200012220122022221222201022222022202122222202102222221222212210201122102222222212211200222122122222021002122101202202201212222220222202220222222022220001122121022012122222202022222122122222221212222222220122112220211122022222122202202101221220122122221122022122202212220202220201222212010022222122222212012121122222021222001020222222102022220202112222211012202202222122212222022212210001220121122121221002222210222222202202121210221222012102220022222020002022122012221222210220222222222222220212012222202002222210210122212220022202221222222121222222121200022010212202212202120200220212010112021222220000212220022222220222122122222122122020222222212222202122202220200022002222222212221220220120122222121101222110222212201202022210202202221012022222220020222020122202122222001221222222002220222222012222201212202211200022012220222221222201221121022120121110022222202222222202122200222212001022022022222110002120222102022222111121222122022121221222022220200202102221201122122222222202201110222221022122220102122222202202220212220201211202100212021222220011222222022102222222120020222122102121220122122220220022222201200022002221022212212012220121122120220120222221212222210212120222211212121022120022220102122222022002121222022122222222022220222122112222202202102222201012212222122201202212222220022122121121022011212202222202022212002202100002220122222020122221022102020222211121222022222122221012022222200012002221201022022222122222221121222221222120122021222210202202201222020202220202022012120022220011002020222102022222222222222022222120221012202220220022122212220112002220022201221010220122222222121110022201222212202202221211101222021221221222221011122121122012121222220020222022022220220202022220212212112200200202012220222212202120220022022122121111122221222202211212202210201222021112222222220210002021222202121222201021222222222222221002012221221202212200010202222222022212202010220120122221220021222001212222201202022211201222202022021122222111212220222202021222000020222022122121221112222220210102022212200102202221122210212211221001222222122011022120212222200202102200100222021021021222221002022221222202222222122021222222112021220012222222220022212201211212022220122210222110220022222121221001222012212222222222212211211222210000020222221010120022022002221222012121222022002211221212022220212012212222201112022220122221222012220201222021220011222020202202221222012200122212222012020122220100012120222122220202101022220122022000220202222221212012222222111012122222122201221101220220122022022210122201212212201202121211111222200121121222222000002120022102022112102221220222102210221202122222210122022210111112212221122221221112220012022120020010122120202202200212021201100212102220020222221000011122122222121022221120221022002122221022002222220122022210100102211222122201210200222110222122020202122110222212222200100210111202210211121212221122020022122120221212100022221222012222222222002221212002002200201112102220222200212011221022022121120200022022222222202220201222001202220200020122222011210201222120122122022022222222022022221112112221210102202201012212112222122211220202221122022222221202222012222222201201011210112212010200122022222211011101022011022022210022220122222221222222202221221202112211222222120220122212222211221102222021022122022201222212210201212201111222012021221112221210210202122110222102202220222022012020222022002220211002112202202112121221122200201100222002022222220110122021212222212212210211200202221212020102221202220121222210022212110000220222202101221212212221210002112211001012122222022220222202222122102221020011022012212222212201221202020202222101221122221001200112222222000222010212222022022200220122002220200022022212210222011122122202202110222122212122212212122221202222212210122212222222221210220112221001201112222212111222202112221122122102220212112220210002122220201102202122022200210100220212212021020221022022202202200222121210112222102000122012221100210012222222100202002110222122002020221202002221220012022222200102012020122211210120222220122121202100022221212222220212110211212202001000222002222101102211222000200111002020221122122001222202202222212222222222200122000221122212202201221202221222111100222000212212201220121222220202012222120112220201002012022022000010122211221122122222222002102220210211012212012102220222222220212100220010012222011110122112212212221202000220221202012002221222222201220222022120022012201110221002212122222112012221212010122200200002111220122201221022221122212221100100022111212212220202222200200202212121121122221101221010022201001001102120222102002020022222102220211202202221211122002022120201200120222120021020211211222100202212022222210201012222121101221212221210210122022111112200122210220002002212220020102222221220122221200112211001122202212220220011120022020011022222222212220201202221011202020020010102222211102121022111221010112012222102022100221002202222210021212200001102220022122220211002220112120220011222122111202222221212212211111202210100101212222212120120222102112010000002221022102022120022022221221002102221212212211201121210210021220110110120211111022222202020101201212210110202110000212102222211211012022210102222010220221102002201222000212221212002112220111012211201120221200100220121200020102201022101202001212202021211211222010001221212221011201121022202200221202022221212102221220022122220211222222200210122022121022212212111222220120022210200122120112102120210222200020212211022101202222110211101222022220112211000220212122010221211202222201200212200020012201021122220101201221021010121212212122111122001000220220211021212220011100022220201201002122221212100212220221112012020121101212220211122222220020012001100120022211221221222221020001222022202022122021221001220221222222201020002220212010001122202200101010022220212112210221122012221221010102222022122111101022200102200222120001022212022122110122101011212211200100112002222120022221001122000222010111122222111220102002012122011212220212120222221202222021001120011010221221010201020102001022112122000220220010212111012210120022022222001020202122101111001200211221202002110022010002221211111012211211202111221121110000122222100211020000021222002001121202220220211221012002121210002222101221201222010020200121110220022202202221001202220210111222122202010021122000202110212001210202001111110100002110101022112211120202020202112012221101110111110100221010122221202100220100221201012100101010100120"))));
        }

        [TestMethod]
        public void TestDay9()
        {
            //Here are some example programs that use these features:
            // takes no input and produces a copy of itself as output.
            //1102,34915192,34915192,7,4,7,99,0 should output a 16-digit number.
            var computer = new Computer("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            computer.SetMemory(computer.Memory.Length + 5, -2);
            Assert.AreEqual(109, computer.GetMemory(0));
            Assert.AreEqual(16, computer.GetMemory(10));
            Assert.AreEqual(0, computer.GetMemory(99));
            //Assert.ThrowsException<Exception>(() => computer.GetMemory(-1));

            CollectionAssert.AreEqual(new List<long>() { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99, 0, 0, 0, 0, 0, -2 }, computer.Memory);

            computer = new Computer("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, computer.DiagnosticOutput);
        
            computer = new Computer("104,1125899906842624,99");
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<long>() { 1125899906842624 }, computer.DiagnosticOutput);

            ////For example, given a relative base of 50, a relative mode parameter of -7 refers to memory address 50 + -7 = 43.
            //computer = new Computer("09,7,2208,-7,-6,0,99,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,88");
            //computer.RunIntCodeProgram();
            //CollectionAssert.AreEqual(new List<long>() { 0, 7, 2208, -7, -6, 0, 99, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 88 }
            //    , computer.Memory);
        }

        [TestMethod]
        public void TestDay10()
        {
            //Here are some larger examples:
            //Best is 5,8 with 33 other asteroids detected:
            //......#.#.
            //#..#.#....
            //..#######.
            //.#.#.###..
            //.#..#.....
            //..#....#.#
            //#..#....#.
            //.##.#..###
            //##...#..#.
            //.#....####

            //Best is 1,2 with 35 other asteroids detected:
            //#.#...#.#.
            //.###....#.
            //.#....#...
            //##.#.#.#.#
            //....#.#.#.
            //.##..###.#
            //..#...##..
            //..##....##
            //......#...
            //.####.###.

            //Best is 6,3 with 41 other asteroids detected:
            //.#..#..###
            //####.###.#
            //....###.#.
            //..###.##.#
            //##.##.#.#.
            //....###..#
            //..#.#..#.#
            //#..#.#.###
            //.##...##.#
            //.....#.#..

            //Best is 11,13 with 210 other asteroids detected:
            //.#..##.###...#######
            //##.############..##.
            //.#.######.########.#
            //.###.#######.####.#.
            //#####.##.#.##.###.##
            //..#####..#.#########
            //####################
            //#.####....###.#.#.##
            //##.#################
            //#####.##.###..####..
            //..######..##.#######
            //####.##.####...##..#
            //.#####..#.######.###
            //##...#.##########...
            //#.##########.#######
            //.####.#.###.###.#.##
            //....##.##.###..#####
            //.#.#.###########.###
            //#.#.#.#####.####.###
            //###.##.####.##.#..##

        }

        [TestMethod]
        public void TestDay11()
        {
        }

        [TestMethod]
        public void TestDay12()
        {
        }

        [TestMethod]
        public void TestDay13()
        {
        }

        [TestMethod]
        public void TestDay14()
        {
            //13312 ORE for 1 FUEL:
            //157 ORE => 5 NZVS
            //165 ORE => 6 DCFZ
            //44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL
            //12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ
            //179 ORE => 7 PSHF
            //177 ORE => 5 HKGWZ
            //7 DCFZ, 7 PSHF => 2 XJWVT
            //165 ORE => 2 GPVTF
            //3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT

            //180697 ORE for 1 FUEL:
            //2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
            //17 NVRVD, 3 JNWZP => 8 VPVL
            //53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
            //22 VJHF, 37 MNCFX => 5 FWMGM
            //139 ORE => 4 NVRVD
            //144 ORE => 7 JNWZP
            //5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
            //5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
            //145 ORE => 6 MNCFX
            //1 NVRVD => 8 CXFTF
            //1 VJHF, 6 MNCFX => 4 RFSQX
            //176 ORE => 6 VJHF

            //2210736 ORE for 1 FUEL:
            //171 ORE => 8 CNZTR
            //7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
            //114 ORE => 4 BHXH
            //14 VRPVC => 6 BMBT
            //6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
            //6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
            //15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
            //13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
            //5 BMBT => 4 WPTQ
            //189 ORE => 9 KTJDG
            //1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
            //12 VRPVC, 27 CNZTR => 2 XDBXC
            //15 KTJDG, 12 BHXH => 5 XCVML
            //3 BHXH, 2 VRPVC => 7 MZWV
            //121 ORE => 7 VRPVC
            //7 XCVML => 6 RJRHP
            //5 BHXH, 4 VRPVC => 5 LTCX
        }

        [TestMethod]
        public void TestDay15()
        {
        }

        [TestMethod]
        public void TestDay16()
        {
        }

        [TestMethod]
        public void TestDay17()
        {
        }

        [TestMethod]
        public void TestDay18()
        {
        }

        [TestMethod]
        public void TestDay19()
        {
            //For example, suppose you scan the 10x10 grid of points closest to the emitter:
            //         X
            //    0->      9
            // 0#.........
            // |.#........
            // v..##......
            //  ...###....
            //  ....###...
            //Y .....####.
            //  ......####
            //  ......####
            //  .......###
            // 9........##
            //In this example, the number of points affected by the tractor beam in the 10x10 area closest to the emitter is 27.
        }

        [TestMethod]
        public void TestDay20()
        {
        }

        [TestMethod]
        public void TestDay21()
        {
        }

        [TestMethod]
        public void TestDay22()
        {
        }

        [TestMethod]
        public void TestDay23()
        {
        }

        [TestMethod]
        public void TestDay24()
        {
        }

        [TestMethod]
        public void TestDay25()
        {
        }

        [TestMethod]
        public void TestFinalAnswers()
        {
            var appFolder = new FileInfo(Environment.CurrentDirectory).Directory.Parent.Parent.Parent;
            var day1 = new Day1(Path.Join(appFolder.FullName, "inputDay1.txt"));
            Assert.AreEqual(3346639, day1.TotalFuelNeeded);
            Assert.AreEqual(5017110, day1.TotalFuelNeededBetter);

            var day2 = new Day2(Path.Join(appFolder.FullName, "inputDay2.txt")); ;
            Assert.AreEqual(3760627, day2.RunChallengePart1());
            Assert.AreEqual(7195, day2.RunChallengePart2());

            var day3 = new Day3(Path.Join(appFolder.FullName, "inputDay3.txt"));
            Assert.AreEqual(1064, day3.RunChallengePart1());
            Assert.AreEqual(25676, day3.RunChallengePart2());

            var day4 = new Day4("158126-624574");
            Assert.AreEqual(1665, day4.RunChallengePart1());
            Assert.AreEqual(1131, day4.RunChallengePart2());

            var day5 = new Day5(Path.Join(appFolder.FullName, "inputDay5.txt"));
            Assert.AreEqual("0,0,0,0,0,0,0,0,0,9961446", string.Join(',', day5.RunChallengePart1()));
            Assert.AreEqual(742621, day5.RunChallengePart2()[0]);

            //var day6 = new Day6(Path.Join(appFolder.FullName, "inputDay6.txt"));
            //Assert.AreEqual(, day6.RunChallengePart1());

            var day7 = new Day7(Path.Join(appFolder.FullName, "inputDay7.txt"));
            Assert.AreEqual(21760, day7.RunChallengePart1());
            //Assert.AreEqual(, day7.RunChallengePart2());

            var day8 = new Day8(Path.Join(appFolder.FullName, "inputDay8.txt"), 25, 6);
            Assert.AreEqual(2684, day8.RunChallengePart1());
            var day9 = new Day9(Path.Join(appFolder.FullName, "inputDay9.txt"));
            Assert.AreEqual(3533056970, day9.RunChallengePart1());
            Assert.AreEqual(72852, day9.RunChallengePart2());

        }
    }
}