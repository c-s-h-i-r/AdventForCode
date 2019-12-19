using System.Collections.Generic;

namespace AdventForCode
{
    public class Day5 : BaseChallenge
    {
        //        --- Day 5: Sunny with a Chance of Asteroids ---
        //You're starting to sweat as the ship makes its way toward Mercury.
        //The Elves suggest that you get the air conditioner working by upgrading your ship computer to support the Thermal Environment Supervision Terminal.
        //The Thermal Environment Supervision Terminal(TEST) starts by running a diagnostic program(your puzzle input).

        //The TEST diagnostic program will run on your existing Intcode computer after a few modifications:
        //First, you'll need to add two new instructions
        //Second, you'll need to add support for parameter modes.

        //        --- Part Two ---
        //The air conditioner comes online! Its cold air feels good for a while, but then the TEST alarms start to go off.
        //Since the air conditioner can't vent its heat anywhere but back into the spacecraft, it's actually making the air inside the ship warmer.
        //Instead, you'll need to use the TEST to extend the thermal radiators.
        //Fortunately, the diagnostic program (your puzzle input) is already equipped for this. Unfortunately, your Intcode computer is not.
        //Your computer is only missing a few opcodes


        public Day5(string filePath) : base(filePath) { }

        public List<int> RunChallengePart1()
        {
            var computer = new Computer(Util.ReadInput(this.filePath)[0]);
            //The TEST diagnostic program will start by requesting from the user the ID of the system to test by running an input instruction
            //- provide it 1, the ID for the ship's air conditioner unit.
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 1 }));
            //It will then perform a series of diagnostic tests confirming that various parts of the Intcode computer, like parameter modes, function correctly.
            //For each test, it will run an output instruction indicating how far the result of the test was from the expected value, where 0 means the test was successful.
            //Non-zero outputs mean that a function is not working correctly; check the instructions that were run before the output instruction to see which one failed.
            //Finally, the program will output a diagnostic code and immediately halt.
            //This final output isn't an error; an output followed immediately by a halt means the program finished.
            //If all outputs were zero except the diagnostic code, the diagnostic program ran successfully.
            //After providing 1 to the only input instruction and passing all the tests, what diagnostic code does the program produce?
            return computer.DiagnosticOutput;
        }

        public List<int> RunChallengePart2()
        {
            //This time, when the TEST diagnostic program runs its input instruction to get the ID of the system to test,
            var computer = new Computer(Util.ReadInput(this.filePath)[0]);
            //provide it 5, the ID for the ship's thermal radiator controller.
            //This diagnostic test suite only outputs one number, the diagnostic code.
            computer.RunIntCodeProgram(new Stack<int>(new List<int>() { 5 }));
            //What is the diagnostic code for system ID 5?
            return computer.DiagnosticOutput;
        }
    }
}