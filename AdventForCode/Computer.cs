using System;
using System.Collections.Generic;

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
    //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
    //For example, the instruction 3,50 would take an input value and store it at address 50.
    //Opcode 4 outputs the value of its only parameter.
    //For example, the instruction 4,50 would output the value at address 50.
    //Programs that use these instructions will come with documentation that explains what should be connected to the input and output.
    //The program 3,0,4,0,99 outputs whatever it gets as input, then halts.

    public class Computer
    {
        //public enum Phase
        //{
        //    0, 1, 2, 3, 4
        //}

        public enum OpCodes
        {
            FinishAndImmediatelyHalt = 99,
            AddValues = 1,
            MultiplyValues = 2,
            Input = 3,
            Output = 4,
            JumpIfTrue = 5,
            JumpIfFalse = 6,
            LessThan = 7,
            Equals = 8
        }

        private enum ParameterMode
        {
            //position mode, which causes the parameter to be interpreted as a position - if the parameter is 50, its value is the value stored at address 50 in memory.
            PositionMode = '0',

            //In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.
            ImmediateMode = '1'
        };

        public List<int> Memory { get; set; }
        public int Output => Memory[0];
        public List<int> DiagnosticOutput { get; set; } = new List<int>();

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

        public void RunIntCodeProgram(Stack<int> programInput = null)
        {
            DiagnosticOutput.Clear();
            var instructionPointer = 0;
            while (true)
            {
                var modeAndOpCode = Memory[instructionPointer];

                //Parameter modes are stored in the same value as the instruction's opcode.
                //The opcode is a two-digit number based only on the ones and tens digit of the value,
                //that is, the opcode is the rightmost two digits of the first value in an instruction.
                var modeAndOpCodeString = modeAndOpCode.ToString().PadLeft(5, '0');

                int opCode;
                Stack<char> modes;

                //Parameter modes are single digits, one per parameter, read right-to-left from the opcode:
                //the first parameter's mode is in the hundreds digit,
                //the second parameter's mode is in the thousands digit,
                //the third parameter's mode is in the ten-thousands digit, and so on.
                //Any missing modes are 0.
                if (modeAndOpCodeString.Length < 2)
                {
                    opCode = int.Parse(modeAndOpCodeString.Substring(0, 1));
                    modes = new Stack<char>(new char[] { '0', '0', '0', '0', '0' });
                }
                else
                {
                    opCode = int.Parse(modeAndOpCodeString.Substring(modeAndOpCodeString.Length - 2, 2));
                    modes = new Stack<char>(modeAndOpCodeString[0..^2]);
                }

                int parameter1Address;
                int parameter2Address;
                int parameter3Address;
                int instructionParameter1;
                int instructionParameter2;
                int instructionCount = 0;
                switch ((OpCodes)opCode)
                {
                    case OpCodes.FinishAndImmediatelyHalt:
                        //for example, 99 means that the program is finished and should immediately halt.
                        return;

                    case OpCodes.AddValues:
                        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        parameter3Address = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? instructionPointer + 3 : Memory[instructionPointer + 3];
                        Memory[parameter3Address] = instructionParameter1 + instructionParameter2;
                        instructionCount = 4;
                        break;

                    case OpCodes.MultiplyValues:
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        parameter3Address = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? instructionPointer + 3 : Memory[instructionPointer + 3];
                        Memory[parameter3Address] = instructionParameter1 * instructionParameter2;
                        instructionCount = 4;
                        break;

                    case OpCodes.Input:
                        //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
                        if ((ParameterMode)modes.Pop() == ParameterMode.ImmediateMode)
                        {
                            Memory[instructionPointer + 1] = programInput.Pop();
                        }
                        else
                        {
                            instructionParameter1 = Memory[instructionPointer + 1];
                            Memory[instructionParameter1] = programInput.Pop();
                        }
                        instructionCount = 2;
                        break;

                    case OpCodes.Output:
                        //Opcode 4 outputs the value of its only parameter.
                        //For example, the instruction 4,50 would output the value at address 50.
                        if ((ParameterMode)modes.Pop() == ParameterMode.ImmediateMode)
                        {
                            DiagnosticOutput.Add(Memory[instructionPointer + 1]);
                        }
                        else
                        {
                            DiagnosticOutput.Add(Memory[Memory[instructionPointer + 1]]);
                        }
                        instructionCount = 2;
                        break;

                    case OpCodes.JumpIfTrue:
                        //Opcode 5 is jump-if-true:
                        //if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter.
                        //Otherwise, it does nothing.
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        if (instructionParameter1 != 0)
                        {
                            instructionPointer = instructionParameter2;
                        }
                        else
                        {
                            instructionCount = 3;
                        }
                        break;

                    case OpCodes.JumpIfFalse:
                        //Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.
                        //Otherwise, it does nothing.
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        if (instructionParameter1 == 0)
                        {
                            instructionPointer = instructionParameter2;
                        }
                        else
                        {
                            instructionCount = 3;
                        }
                        break;

                    case OpCodes.LessThan:
                        //Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter.
                        //Otherwise, it stores 0.
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        parameter3Address = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? instructionPointer + 3 : Memory[instructionPointer + 3];
                        Memory[parameter3Address] = instructionParameter1 < instructionParameter2 ? 1 : 0;
                        instructionCount = 4;
                        break;

                    case OpCodes.Equals:
                        //Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
                        //Otherwise, it stores 0.
                        parameter1Address = Memory[instructionPointer + 1];
                        parameter2Address = Memory[instructionPointer + 2];
                        instructionParameter1 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter1Address : Memory[parameter1Address];
                        instructionParameter2 = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? parameter2Address : Memory[parameter2Address];
                        parameter3Address = (ParameterMode)modes.Pop() == ParameterMode.ImmediateMode ? instructionPointer + 3 : Memory[instructionPointer + 3];
                        Memory[parameter3Address] = instructionParameter1 == instructionParameter2 ? 1 : 0;
                        instructionCount = 4;
                        break;

                    default:
                        //The opcode indicates what to do; Encountering an unknown opcode means something went wrong.
                        throw new Exception("Something went wrong");
                }

                //Instruction pointer should increase by the number of values in the instruction after the instruction finishes.
                //Normally, after an instruction is finished, the instruction pointer increases by the number of values in that instruction.
                //However, if the instruction modifies the instruction pointer, that value is used and the instruction pointer is not automatically increased.
                instructionPointer += instructionCount;
            }
        }
    }
}