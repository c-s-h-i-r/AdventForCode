using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventForCode
{
    public class Day6
    {
        // --- Day 6: Universal Orbit Map ---
        //You've landed at the Universal Orbit Map facility on Mercury.
        //Because navigation in space often involves transferring between orbits, the orbit maps here are useful for
        //finding efficient routes between, for example, you and Santa. You download a map of the local orbits (your puzzle input).
        //Except for the universal Center of Mass(COM), every object in space is in orbit around exactly one other object.
        //An orbit looks roughly like this:
        //                  \
        //                   \
        //                    |
        //                    |
        //AAA--> o            o <--BBB
        //                    |
        //                    |
        //                   /
        //                  /
        //In this diagram, the object BBB is in orbit around AAA.The path that BBB takes around AAA (drawn with lines) is only partly shown.

        //Before you use your map data to plot a course, you need to make sure it wasn't corrupted during the download.
        private readonly string filePath;
        private const string COM = "COM";

        public Day6(string filePath="")
        {
            this.filePath = filePath;
        }

        public int RunChallengePart1()
        {
            //What is the total number of direct and indirect orbits in your map data?
            LoadOrbits(Program.ReadInput(this.filePath, "")[0]);
            return CalcOrbits();
        }

        public struct ObjectNode
        {
            public string Name;
            public LinkedList<ObjectNode> Orbitting;
        }
        public Dictionary<string, ObjectNode> allObjects = new Dictionary<string, ObjectNode>();

        public void LoadOrbits(string allOrbits)
        {
            //In the map data, this orbital relationship is written AAA)BBB, which means "BBB is in orbit around AAA".
            var orbitInstructions = allOrbits.Split(Environment.NewLine);
            foreach(var oi in orbitInstructions.Where(x=>x.Count() > 0))
            {
                var objects = oi.Split(')');
                var name = objects[1];
                var orbittingObjectName = objects[0];

                ObjectNode on;
                if (allObjects.ContainsKey(name))
                {
                    on = allObjects[name];
                }
                else
                {
                    on = new ObjectNode() { Name = name };
                    on.Orbitting = new LinkedList<ObjectNode>();
                    allObjects.Add(name, on);
                }

                ObjectNode orbitting;
                if (allObjects.ContainsKey(orbittingObjectName))
                {
                    orbitting = allObjects[orbittingObjectName];
                }
                else
                {
                    orbitting = new ObjectNode() { Name = orbittingObjectName };
                    orbitting.Orbitting = new LinkedList<ObjectNode>();
                    allObjects.Add(orbittingObjectName, orbitting);
                }
                on.Orbitting.AddLast(orbitting);

            }
        }

        //To verify maps, the Universal Orbit Map facility uses orbit count checksums -
        //the total number of direct orbits (like the one shown above) and indirect orbits.
        public int CalcOrbits()
        {
            //Whenever A orbits B and B orbits C, then A indirectly orbits C.
            //This chain can be any number of objects long: if A orbits B, B orbits C, and C orbits D, then A indirectly orbits D.
            return DepthFirstTraverse(this.allObjects);
        }

        private int DepthFirstTraverse(Dictionary<string, ObjectNode> allObjects)
        {
            throw new NotImplementedException();
        }

        public void RunChallengePart2()
        {
        }
    }
}
