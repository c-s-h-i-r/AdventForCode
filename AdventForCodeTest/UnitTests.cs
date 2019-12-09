using AdventForCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            //For example, suppose you have the following program:
            //1,9,10,3,2,3,11,0,99,30,40,50
            //For the purposes of illustration, here is the same program split into multiple lines:
            //1,9,10,3,
            //2,3,11,0,
            //99,
            //30,40,50
            //The first four integers, 1,9,10,3, are at positions 0, 1, 2, and 3. Together, they represent the first opcode (1, addition), the positions of the two inputs(9 and 10), and the position of the output(3). To handle this opcode, you first need to get the values at the input positions: position 9 contains 30, and position 10 contains 40. Add these numbers together to get 70. Then, store this value at the output position; here, the output position (3) is at position 3, so it overwrites itself. Afterward, the program looks like this:
            //1,9,10,70,
            //2,3,11,0,
            //99,
            //30,40,50
            //Step forward 4 positions to reach the next opcode, 2. This opcode works just like the previous, but it multiplies instead of adding. The inputs are at positions 3 and 11; these positions contain 70 and 50 respectively. Multiplying these produces 3500; this is stored at position 0:
            //3500,9,10,70,
            //2,3,11,0,
            //99,
            //30,40,50
            //Stepping forward 4 more positions arrives at opcode 99, halting the program.

            var computer = new Computer();
            computer.Memory = new List<int>() { 1, 0, 0, 0, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<int>() { 2, 0, 0, 0, 99 }, computer.Memory);
            computer.Memory = new List<int>() { 2, 3, 0, 3, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<int>() { 2, 3, 0, 6, 99 }, computer.Memory);
            computer.Memory = new List<int>() { 2, 4, 4, 5, 99, 0 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<int>() { 2, 4, 4, 5, 99, 9801 }, computer.Memory);
            computer.Memory = new List<int>() { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<int>() { 30, 1, 1, 4, 2, 5, 6, 0, 99 }, computer.Memory);
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

            //What is the fewest combined steps the wires must take to reach an intersection?
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
            CollectionAssert.AreEqual(new List<int>() { 4 }, computer.DiagnosticOutput);
            computer = new Computer("1002,4,3,4,33");
            computer.RunIntCodeProgram();
            CollectionAssert.AreEqual(new List<int>() { 1002, 4, 3, 4, 99 }, computer.Memory);

            //For example, here are several programs that take one input, compare it to the value 8, and then produce one output:
            // - Using position mode, consider whether the input is equal to 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,9,8,9,10,9,4,9,99,-1,8");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 8 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -8 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 0 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);

            // - Using position mode, consider whether the input is less than 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,9,7,9,10,9,4,9,99,-1,8");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 8 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 7 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -8 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 9 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);

            // - Using immediate mode, consider whether the input is equal to 8; output 1 (if it is) or 0 (if it is not).
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 8 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 7 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -8 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1108,-1,8,3,4,3,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 9 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);

            //Here are some jump tests that take an input, then output 0 if the input was zero or 1 if the input was non-zero:
            // (using position mode)
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 0 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 1 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -8 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 9 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);

            // (using immediate mode)
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 0 }));
            CollectionAssert.AreEqual(new List<int>() { 0 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 1 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -8 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);
            computer = new Computer("3,3,1105,-1,9,1101,0,0,12,4,12,99,1");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 9 }));
            CollectionAssert.AreEqual(new List<int>() { 1 }, computer.DiagnosticOutput);

            //This example program uses an input instruction to ask for a single number.
            //The program will then 
                //output 999 if the input value is below 8, 
                //output 1000 if the input value is equal to 8, 
                //or output 1001 if the input value is greater than 8.
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 0 }));
            CollectionAssert.AreEqual(new List<int>() { 999 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 8 }));
            CollectionAssert.AreEqual(new List<int>() { 1000 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 9 }));
            CollectionAssert.AreEqual(new List<int>() { 1001 }, computer.DiagnosticOutput);
            computer = new Computer("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99");
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { -1 }));
            CollectionAssert.AreEqual(new List<int>() { 999 }, computer.DiagnosticOutput);
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
    }
}
