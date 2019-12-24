using System;
using System.Collections.Generic;

namespace AdventForCode
{
    public class Day7 : BaseChallenge
    {
        //        --- Day 7: Amplification Circuit ---
        //Based on the navigational maps, you're going to need to send more power to your ship's thrusters to reach Santa in time.
        //To do this, you'll need to configure a series of amplifiers already installed on the ship.
        //There are five amplifiers connected in series; each one receives an input signal and produces an output signal.
        //They are connected such that the first amplifier's output leads to the second amplifier's input,
        // the second amplifier's output leads to the third amplifier's input, and so on.
        // The first amplifier's input value is 0, and the last amplifier's output leads to your ship's thrusters.
        //   O-------O O-------O O-------O O-------O O-------O
        //0 ->| Amp A |->| Amp B |->| Amp C |->| Amp D |->| Amp E |-> (to thrusters)
        //   O-------O O-------O O-------O O-------O O-------O
        //The Elves have sent you some Amplifier Controller Software (your puzzle input), a program that should run on your existing Intcode computer.

        //Each amplifier will need to run a copy of the program.
        //When a copy of the program starts running on an amplifier, it will first use an input instruction to ask the amplifier
        //for its current phase setting (an integer from 0 to 4). Each phase setting is used exactly once, but the Elves can't remember which amplifier needs which phase setting.
        //The program will then call another input instruction to get the amplifier's input signal, compute the correct output signal,
        //and supply it back to the amplifier with an output instruction. (If the amplifier has not yet received an input signal, it waits until one arrives.)
        //Your job is to find the largest output signal that can be sent to the thrusters by trying every possible combination of phase settings on the amplifiers.
        //Make sure that memory is not shared or reused between copies of the program.

        //For example, suppose you want to try the phase setting sequence 3,1,2,4,0, which would mean setting amplifier A to phase setting 3, amplifier B to setting 1, C to 2, D to 4,
        //and E to 0. Then, you could determine the output signal that gets sent from amplifier E to the thrusters with the following steps:
        //Start the copy of the amplifier controller software that will run on amplifier A. At its first input instruction, provide it the amplifier's phase setting, 3.
        //At its second input instruction, provide it the input signal, 0. After some calculations, it will use an output instruction to indicate the amplifier's output signal.
        //Start the software for amplifier B. Provide it the phase setting (1) and then whatever output signal was produced from amplifier A.
        //It will then produce a new output signal destined for amplifier C.
        //Start the software for amplifier C, provide the phase setting (2) and the value from amplifier B, then collect its output signal.
        //Run amplifier D's software, provide the phase setting (4) and input value, and collect its output signal.
        //Run amplifier E's software, provide the phase setting (0) and input value, and collect its output signal.
        //The final output signal from amplifier E would be sent to the thrusters.
        //However, this phase setting sequence may not have been the best one; another sequence might have sent a higher signal to the thrusters.

        private readonly Computer[] amplifiers = new Computer[5];

        public Day7(string filePath): base(filePath){}

        public long RunChallengePart1()
        {
            var amplifierControllerSoftware = Util.ReadInput(this.filePath)[0];

            //What is the highest signal that can be sent to the thrusters?
            return FindMaxSignal(amplifierControllerSoftware, new int[] { 0, 1, 2, 3, 4 });
        }

        public long FindMaxSignal(string amplifierControllerSoftware, int[] phaseSettingSequence)
        {
            //Try every combination of phase settings on the amplifiers.
            var maxSignal = long.MinValue;

            var permutations = new List<int[]>();
            Util.FindPermutations(phaseSettingSequence, permutations);
            foreach (var permute in permutations)
            {
                long lastOutputSignal = 0;
                maxSignal = RunProgramThroughAmplifiers(permute, maxSignal, ref lastOutputSignal, amplifierControllerSoftware);
            }
            return maxSignal;
        }

        public long RunProgramThroughAmplifiers(int[] amplifierInput, long maxSignal, ref long lastOutputSignal, string amplifierControllerSoftware)
        {
            for (var i = 0; i < this.amplifiers.Length; i++)
            {
                this.amplifiers[i] = new Computer(amplifierControllerSoftware);
                this.amplifiers[i].RunIntCodeProgram(new Stack<long>(new List<long>() { lastOutputSignal, amplifierInput[i] }));
                lastOutputSignal = this.amplifiers[i].DiagnosticOutput[0];
                maxSignal = Math.Max(maxSignal, lastOutputSignal);
            }
            return maxSignal;
        }

        //        --- Part Two ---
        //It's no good - in this configuration, the amplifiers can't generate a large enough output signal to produce the thrust you'll need.
        //The Elves quickly talk you through rewiring the amplifiers into a feedback loop:
        //         O-------O  O-------O  O-------O  O-------O  O-------O
        //   0 -+->| Amp A |->| Amp B |->| Amp C |->| Amp D |->| Amp E |-.
        //      |  O-------O  O-------O  O-------O  O-------O  O-------O |
        //      |                                                        |
        //      '--------------------------------------------------------+
        //                                                            |
        //                                                            v
        //                                                     (to thrusters)
        //Most of the amplifiers are connected as they were before; amplifier A's output is connected to amplifier B's input, and so on.
        //However, the output from amplifier E is now connected into amplifier A's input.
        //This creates the feedback loop: the signal will be sent through the amplifiers many times.

        //In feedback loop mode, the amplifiers need totally different phase settings: integers from 5 to 9, again each used exactly once.
        //These settings will cause the Amplifier Controller Software to repeatedly take input and produce output many times before halting.
        //Provide each amplifier its phase setting at its first input instruction; all further input/output instructions are for signals.
        //Don't restart the Amplifier Controller Software on any amplifier during this process.
        //Each one should continue receiving and sending signals until it halts.
        //All signals sent or received in this process will be between pairs of amplifiers except the very first signal and the very last signal.
        //To start the process, a 0 signal is sent to amplifier A's input exactly once.

        //Eventually, the software on the amplifiers will halt after they have processed the final loop.
        //When this happens, the last output signal from amplifier E is sent to the thrusters.
        //Your job is to find the largest output signal that can be sent to the thrusters using the new phase settings and feedback loop arrangement.
        //Try every combination of the new phase settings on the amplifier feedback loop.What is the highest signal that can be sent to the thrusters?

        public int RunChallengePart2()
        {
            return 0;
        }
    }
}