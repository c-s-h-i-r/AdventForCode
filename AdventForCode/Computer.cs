using System;
using System.Collections.Generic;
using System.Text;

namespace AdventForCode
{
    //  Intcode programs are given as a list of integers; 
    //      these values are used as the initial state for the computer's memory. 
    //  When you run an Intcode program, make sure to start by initializing memory to the program's values.
    //  A position in memory is called an address (for example, the first value in memory is at "address 0").
    //  Opcodes(like 1, 2, or 99) mark the beginning of an instruction.
    //      The values used immediately after an opcode, if any, are called the instruction's parameters. 
    //          For example, in the instruction 1,2,3,4, 1 is the opcode; 2, 3, and 4 are the parameters. 
    //          The instruction 99 contains only an opcode and has no parameters.
    //  The address of the current instruction is called the instruction pointer; it starts at 0. 
    //  After an instruction finishes, the instruction pointer increases by the number of values in the instruction; until you add more instructions to the computer, 
    //  this is always 4 (1 opcode + 3 parameters) for the add and multiply instructions. 
    //      (The halt instruction would increase the instruction pointer by 1, but it halts the program instead.)

    public class Computer
    {
        public enum OpCodes
        {
            FinishAndImmediatelyHalt = 99,
            AddValues = 1,
            MultiplyValues = 2
        }

        public List<int> Memory { get; set; }
        public int Output => Memory[0];

        public Computer()
        {

        }
        public Computer(List<int> memory)
        {
            Memory = memory;
        }
        public Computer(string initialMemory)
        {
            Memory = GenerateIntCodeProgram(initialMemory);
        }

        public static List<int> GenerateIntCodeProgram(string input)
        {
            var IntCode = new List<int>();
            var result = input.Split(",");
            foreach (var x in result)
            {
                IntCode.Add(int.Parse(x));
            }
            return IntCode;
        }

        public void SetInputs(int noun, int verb)
        {
            if (Memory.Count > 1)
            {
                Memory[1] = noun;
                Memory[2] = verb;
            }
        }

        public void RunIntCodeProgram()
        {
            var instructionPointer = 0;
            while (true)
            {
                //To run one, start by looking at the first integer(called position 0). 
                //Here, you will find an opcode - either 1, 2, or 99. 
                var opCode = Memory[instructionPointer];
                int parameter1Address;
                int parameter2Address;
                int outputPosition;
                int instructionParameter1;
                int instructionParameter2;

                switch ((OpCodes)opCode)
                {
                    case OpCodes.FinishAndImmediatelyHalt:
                        //for example, 99 means that the program is finished and should immediately halt.
                        return;
                    case OpCodes.AddValues:
                        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
                        //The three integers immediately after the opcode tell you these three positions - 
                        //the first two indicate the positions from which you should read the input values, 
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        //and the third indicates the position at which the output should be stored.
                        outputPosition = Memory[instructionPointer + 3];
                        instructionParameter1 = Memory[parameter1Address];
                        instructionParameter2 = Memory[parameter2Address];
                        Memory[outputPosition] = instructionParameter1 + instructionParameter2;
                        break;
                    case OpCodes.MultiplyValues:
                        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them. 
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        // the third indicates the position at which the output should be stored.
                        outputPosition = Memory[instructionPointer + 3];
                        // For example, if your Intcode computer encounters 1,10,20,30, it should read the values at positions 10 and 20, add those values, and then overwrite the value at position 30 with their sum.
                        Memory[outputPosition] = Memory[parameter1Address] * Memory[parameter2Address];
                        break;
                    default:
                        //The opcode indicates what to do; Encountering an unknown opcode means something went wrong.
                        throw new Exception("Something went wrong");
                }
                //Once you're done processing an opcode, move to the next one by stepping forward 4 positions.
                instructionPointer += 4;
            }
        }
    }
}
