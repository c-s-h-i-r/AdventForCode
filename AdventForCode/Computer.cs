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
    //Opcode 4 outputs the value of its only parameter.

    // Computer supports long values
    //Memory beyond the initial program starts with the value 0 and can be read or written like any other memory.
    //(It is invalid to try to access memory at a negative address, though.)

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
            Equals = 8,
            AdjustRelativeBase = 9
        }

        private enum ParameterMode
        {
            //position mode, which causes the parameter to be interpreted as a position - if the parameter is 50, its value is the value stored at address 50 in memory.
            PositionMode = '0',

            //In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.
            ImmediateMode = '1',

            //Parameters in relative mode, behave very similarly to parameters in position mode:the parameter is interpreted as a position. 
            //Like position mode, parameters in relative mode can be read from or written to.
            //The important difference is that relative mode parameters don't count from address 0.
            //Instead, they count from a value called the relative base. The relative base starts at 0.
            //The address a relative mode parameter refers to is itself plus the current relative base.
            //When the relative base is 0, relative mode parameters and position mode parameters with the same value refer to the same address.
            RelativeMode = '2'
        };

        public long[] Memory { get; set; }
        public long Output => Memory[0];
        public List<long> DiagnosticOutput { get; set; } = new List<long>();

        public Computer() { }

        public Computer(List<long> memory)
        {
            Memory = memory.ToArray();
        }

        public Computer(string initialMemory)
        {
            Memory = GenerateIntCodeProgram(initialMemory);
        }

        public static long[] GenerateIntCodeProgram(string input)
        {
            var intCodeProgram = new long[100];
            var result = input.Split(",");
            long i = 0;
            foreach (var x in result)
            {
                if (i == intCodeProgram.Length)
                {
                    intCodeProgram = Util.ResizeArray(intCodeProgram, intCodeProgram.Length + 100);
                }
                intCodeProgram[i] = long.Parse(x);
                i++;
            }
            // Shrink array to the used portion
            return Util.ResizeArray(intCodeProgram, i-1);
        }

        public void SetInputs(long noun, long verb)
        {
            if (Memory.Length > 1)
            {
                Memory[1] = noun;
                Memory[2] = verb;
            }
        }

        public void RunIntCodeProgram(Stack<long> programInput = null)
        {
            DiagnosticOutput.Clear();
            long instructionPointer = 0;
            long relativeBase = 0;
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

                long parameter3Address;
                long instructionParameter1;
                long instructionParameter2;
                int instructionCount = 0;
                switch ((OpCodes)opCode)
                {
                    case OpCodes.FinishAndImmediatelyHalt:
                        //99 means that the program is finished and should immediately halt.
                        return;

                    case OpCodes.AddValues:
                        //Opcode 1 adds together numbers read from two positions and stores the result in a third position.
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
                        parameter3Address = GetParameter(instructionPointer + 3, modes.Pop(), relativeBase, false);
                        SetMemory(parameter3Address, instructionParameter1 + instructionParameter2);
                        instructionCount = 4;
                        break;

                    case OpCodes.MultiplyValues:
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
                        parameter3Address = GetParameter(instructionPointer + 3, modes.Pop(), relativeBase, false); 
                        SetMemory(parameter3Address, instructionParameter1 * instructionParameter2);
                        instructionCount = 4;
                        break;

                    case OpCodes.Input:
                        //Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
                        if ((ParameterMode)modes.Pop() == ParameterMode.ImmediateMode)
                        {
                            SetMemory(instructionPointer + 1, programInput.Pop());
                        }
                        else
                        {
                            instructionParameter1 = Memory[instructionPointer + 1];
                            SetMemory(instructionParameter1, programInput.Pop());
                        }
                        instructionCount = 2;
                        break;

                    case OpCodes.Output:
                        //Opcode 4 outputs the value of its only parameter.
                        DiagnosticOutput.Add(GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true));
                        instructionCount = 2;
                        break;

                    case OpCodes.JumpIfTrue:
                        //if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter.
                        //Otherwise, it does nothing.
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
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
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
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
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
                        parameter3Address = GetParameter(instructionPointer + 3, modes.Pop(), relativeBase, false);
                        SetMemory(parameter3Address, instructionParameter1 < instructionParameter2 ? 1 : 0);
                        instructionCount = 4;
                        break;

                    case OpCodes.Equals:
                        //Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
                        //Otherwise, it stores 0.
                        instructionParameter1 = GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionParameter2 = GetParameter(instructionPointer + 2, modes.Pop(), relativeBase, true);
                        parameter3Address = GetParameter(instructionPointer + 3, modes.Pop(), relativeBase, false);
                        SetMemory(parameter3Address, instructionParameter1 == instructionParameter2 ? 1 : 0);
                        instructionCount = 4;
                        break;
                    case OpCodes.AdjustRelativeBase:
                        //The relative base is modified with the relative base offset instruction:
                        //Opcode 9 adjusts the relative base by the value of its only parameter.
                        //The relative base increases (or decreases, if the value is negative) by the value of the parameter.
                        //For example, if the relative base is 2000, then after the instruction 109,19, the relative base would be 2019.
                        //If the next instruction were 204,-34, then the value at address 1985 would be output.
                        relativeBase += GetParameter(instructionPointer + 1, modes.Pop(), relativeBase, true);
                        instructionCount = 2;
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

        private long GetParameter(long MemoryLocation, char mode, long relativeBase, bool inputParameter)
        {
            //position mode, which causes the parameter to be interpreted as a position - if the parameter is 50, its value is the value stored at address 50 in memory.
            //In immediate mode, a parameter is interpreted as a value - if the parameter is 50, its value is simply 50.
            //Parameters in relative mode, behave very similarly to parameters in position mode:the parameter is interpreted as a position. 
            //Like position mode, parameters in relative mode can be read from or written to.
            //The important difference is that relative mode parameters don't count from address 0.
            //Instead, they count from a value called the relative base. The relative base starts at 0.
            //The address a relative mode parameter refers to is itself plus the current relative base.
            //When the relative base is 0, relative mode parameters and position mode parameters with the same value refer to the same address.
            return ((ParameterMode)mode) switch
            {
                ParameterMode.ImmediateMode => inputParameter ? GetMemory(MemoryLocation) : MemoryLocation,
                ParameterMode.PositionMode => inputParameter ? GetMemory(GetMemory(MemoryLocation)) : GetMemory(MemoryLocation),
                ParameterMode.RelativeMode => inputParameter ? GetMemory(GetMemory(MemoryLocation) + relativeBase) : GetMemory(MemoryLocation + relativeBase),
                _ => throw new Exception("Invalid parameter mode"),
            };
        }

        /// <summary>
        /// Assign a value in the computer's memory a value. Will automatically allocate more memory if needed.
        /// </summary>
        /// <param name="index">address in memory</param>
        /// TODO: could be improved?
        public void SetMemory(long index, long value)
        {
            if (index < 0)
            {
                throw new Exception("Invalid memory address" + index);
            }
            if (index >= Memory.Length)
            {
                Memory = Util.ResizeArray(Memory, index);
            }
            Memory[index] = value;
        }


        /// <summary>
        /// Retrieve a value from the computer's memory a value. Will return 0 if outside of the memory range of addresses.
        /// </summary>
        /// <param name="index">address in memory</param>
        public long GetMemory(long index)
        {
            if (index < 0)
            {
                throw new Exception("Invalid memory address" + index);
            }
            if (index >= Memory.Length)
            {
                return 0;
            }
            return Memory[index];
        }
    }
}