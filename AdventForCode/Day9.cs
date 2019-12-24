using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventForCode
{
    public class Day9:BaseChallenge
    {
        //        --- Day 9: Sensor Boost ---
        //You've just said goodbye to the rebooted rover and left Mars when you receive a faint distress signal coming from the asteroid belt.
        //It must be the Ceres monitoring station!
        //In order to lock on to the signal, you'll need to boost your sensors. 
        //The Elves send up the latest BOOST program - Basic Operation Of System Test.
        //While BOOST(your puzzle input) is capable of boosting your sensors, for tenuous safety reasons,
        //it refuses to do so until the computer it runs on passes some checks to demonstrate it is a complete Intcode computer.
        //Your existing Intcode computer is missing one key feature: it needs support for parameters in relative mode.
        
        
        public Day9(string filePath):base(filePath){}

        public long RunChallengePart1()
        {
            //The BOOST program will ask for a single input; run it in test mode by providing it the value 1.
            //It will perform a series of checks on each opcode, output any opcodes(and the associated parameter modes) that seem to be functioning incorrectly,
            //and finally output a BOOST keycode.
            //Once your Intcode computer is fully functional, the BOOST program should report no malfunctioning opcodes when run in test mode;
            //it should only output a single value, the BOOST keycode.
            var computer = new Computer(Util.ReadInput(this.filePath)[0]);
            computer.RunIntCodeProgram(new Stack<long>(new List<long>() { 1 }));

            //What BOOST keycode does it produce?
            Console.WriteLine(string.Join(",",computer.DiagnosticOutput));
            return computer.DiagnosticOutput.Last();
        }

        //public List<int> RunChallengePart2()
        //{
        //    //What BOOST keycode does it produce?
        //    return 0;
        //}
    }
}