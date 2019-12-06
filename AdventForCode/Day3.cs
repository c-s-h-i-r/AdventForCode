using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace AdventForCode
{
    public class Day3
    {
        private string filePath { get; set; }
        public Dictionary<Point, int> Wire1Path { get; set; }
        public Dictionary<Point, int> Wire2Path { get; set; }
        public string[] Wire1 { get; private set; }
        public string[] Wire2 { get; private set; }

        public Day3(string filePath)
        {
            this.filePath = filePath;
        }
        //        --- Day 3: Crossed Wires ---
        //The gravity assist was successful, and you're well on your way to the Venus refuelling station. 
        //During the rush back on Earth, the fuel management system wasn't completely installed, so that's next on the priority list.
        //Opening the front panel reveals a jumble of wires.Specifically, two wires are connected to a central port and extend outward on a grid.
        //You trace the path each wire takes as it leaves the central port, one wire per line of text (your puzzle input).
        //The wires twist and turn, but the two wires occasionally cross paths.
        //To fix the circuit, you need to find the intersection point closest to the central port.
        //Because the wires are on a grid, use the Manhattan distance for this measurement.
        //While the wires do technically cross right at the central port where they both start, this point does not count, nor does a wire count as crossing with itself.

        //--- Part Two ---
        //It turns out that this circuit is very timing-sensitive; you actually need to minimize the signal delay.
        //To do this, calculate the number of steps each wire takes to reach each intersection; choose the intersection where the sum of both wires' steps is lowest. 
        //If a wire visits a position on the grid multiple times, use the steps value from the first time it visits that position when calculating the total value of a specific intersection.
        //The number of steps a wire takes is the total number of grid squares the wire has entered to get to that location, including the intersection being considered.
        //Again consider the example from above:
        //...........
        //.+-----+...
        //.|.....|...
        //.|..+--X-+.
        //.|..|..|.|.
        //.|.-X--+.|.
        //.|..|....|.
        //.|.......|.
        //.o-------+.
        //...........
        //In the above example, the intersection closest to the central port is reached after 8+5+5+2 = 20 steps by the first wire and 7+6+4+3 = 20 steps by the second wire for a total of 20+20 = 40 steps.
        //However, the top-right intersection is better: the first wire takes only 8+5+2 = 15 and the second wire takes only 7+6+2 = 15, a total of 15+15 = 30 steps.
        //What is the fewest combined steps the wires must take to reach an intersection?


        public int FindClosestDistanceToOrigin(string[] wire1, string[] wire2)
        {
            this.Wire1Path = TracePathCountingSteps(wire1);
            this.Wire2Path = TracePathCountingSteps(wire2);
            var crossings = FindIntersectionPoints();
            var closestPoint = crossings.Select(p => new { p.Key.X, p.Key.Y, Distance = Math.Abs(p.Key.X) + Math.Abs(p.Key.Y) }).OrderBy(x=>x.Distance).FirstOrDefault();

            //What is the Manhattan distance from the central port to the closest intersection?
            return closestPoint?.Distance ?? 0;
        }

        private Dictionary<Point, int> FindIntersectionPoints()
        {
            var result = new Dictionary<Point, int>();
            foreach(var x in Wire1Path)
            {
                if (Wire2Path.ContainsKey(x.Key))
                {
                    result.Add(x.Key, x.Value + Wire2Path[x.Key]);
                }
            }
            return result;
            //return Wire1Path.Intersect(Wire2Path).ToDictionary(x=>x.Key, x=>x.Value);//  ()).Distinct().ToList();
        }

        public int FindIntersectionWithLeastSteps(string[] wire1, string[] wire2)
        {
            this.Wire1Path = TracePathCountingSteps(wire1);
            this.Wire2Path = TracePathCountingSteps(wire2);

            var crossings = FindIntersectionPoints();

            var closestPoint = crossings.Min(x => x.Value);
            return closestPoint;
        }
        private Dictionary<Point,int> TracePathCountingSteps(string[] wireDirections)
        {
            // Ignore the origin
            var path = new Dictionary<Point, int>();
            var previousLocation = new Point();
            var stepCount = 0;
            foreach (var step in wireDirections)
            {
                var direction = step.Substring(0, 1);
                var distance = int.Parse(step.Substring(1));
                for (int i = 1; i <= distance; i++)
                {
                    Point p = new Point();
                    switch (direction)
                    {
                        case "R":
                            p = new Point(previousLocation.X + i, previousLocation.Y);
                            break;
                        case "L":
                            p = new Point(previousLocation.X - i, previousLocation.Y);
                            break;
                        case "U":
                            p = new Point(previousLocation.X, previousLocation.Y + i);
                            break;
                        case "D":
                            p = new Point(previousLocation.X, previousLocation.Y - i);
                            break;
                    }
                    if (path.ContainsKey(p))
                    {
                        stepCount++;//stepCount = path[p];
                    }
                    else
                    {
                        stepCount++;
                        path.Add(p, stepCount);
                    }
                }
                switch (direction)
                {
                    case "R":
                        previousLocation = new Point(previousLocation.X + distance, previousLocation.Y);
                        break;
                    case "L":
                        previousLocation = new Point(previousLocation.X - distance, previousLocation.Y);
                        break;
                    case "U":
                        previousLocation = new Point(previousLocation.X, previousLocation.Y + distance);
                        break;
                    case "D":
                        previousLocation = new Point(previousLocation.X, previousLocation.Y - distance);
                        break;
                }
            }
            return path;
        }

        public int RunChallenge3Part1()
        {
            var input = Program.ReadInput(this.filePath);
            this.Wire1 = input[0].Split(",");
            this.Wire2 = input[1].Split(",");
            return FindClosestDistanceToOrigin(Wire1, Wire2);
        }
        public int RunChallenge3Part2()
        {
            var input = Program.ReadInput(this.filePath);
            this.Wire1 = input[0].Split(",");

            this.Wire2 = input[1].Split(",");
            return FindIntersectionWithLeastSteps(Wire1, Wire2);
        }
    }
}
