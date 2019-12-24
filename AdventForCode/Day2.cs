namespace AdventForCode
{
    //        --- Day 2: 1202 Program Alarm ---
    //On the way to your gravity assist around the Moon, your ship computer beeps angrily about a "1202 program alarm".
    //On the radio, an Elf is already explaining how to handle the situation: "Don't worry, that's perfectly norma--" The ship computer bursts into flames.
    //You notify the Elves that the computer's magic smoke seems to have escaped.
    //"That computer ran Intcode programs like the gravity assist program it was working on; surely there are enough spare parts up there to build a new Intcode computer!"

    // --- Part Two ---
    //"Good, the new computer seems to be working correctly! Keep it nearby during this mission - you'll probably use it again.
    //Real Intcode computers support many more features than your new one, but we'll let you know what they are as you need them."
    //"However, your current priority should be to complete your gravity assist around the Moon.

    //For this mission to succeed, we should settle on some terminology for the parts you've already built."
    //
    //  To complete the gravity assist, you need to determine what pair of inputs produces the output 19690720
    //  The inputs should still be provided to the program by replacing the values at addresses 1 and 2, just like before.
    //  In this program, the value placed in address 1 is called the noun, and the value placed in address 2 is called the verb.
    //      Each of the two input values will be between 0 and 99, inclusive.
    //  Once the program has halted, its output is available at address 0, also just like before.
    //  Each time you try a pair of inputs, make sure you first reset the computer's memory to the values in the program (your puzzle input) -
    //      in other words, don't reuse memory from a previous attempt.

    public class Day2 : BaseChallenge
    {
        //Part 1: Once you have a working computer, the first step is to restore the gravity assist program(your puzzle input) to the "1202 program alarm" state it had just before the last computer caught fire.
        //What value is left at position 0 after the program halts?
        public Day2(string filePath) : base(filePath) { }

        public long RunChallengePart1()
        {
            var input = Util.ReadInput(this.filePath);
            var computer = new Computer(input[0]);
            //To do this, before running the program, replace position 1 with the value 12 and replace position 2 with the value 2.
            computer.SetInputs(12, 2);
            computer.RunIntCodeProgram();
            return computer.Output;
        }

        public int RunChallengePart2()
        {
            // Find the input noun and verb that cause the program to produce the output 19690720.
            // What is 100 * noun + verb ? (For example, if noun = 12 and verb = 2, the answer would be 1202.)
            var initialMemory = Util.ReadInput(this.filePath)[0];
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var computer = new Computer(initialMemory);
                    computer.SetInputs(noun, verb);
                    computer.RunIntCodeProgram();
                    if (computer.Output == 19690720)
                    {
                        return 100 * noun + verb;
                    }
                }
            }
            return -1;
        }
    }
}