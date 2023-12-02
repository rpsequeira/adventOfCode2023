using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;

namespace adventofcode2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 2 of AoC");
            IEnumerable<Tuple<int, IEnumerable<Tuple<int, string>>>> data = null;
            Utils.Utils.MeasureActionTime("Load", () =>
            {
                string input = args[0];
                Console.WriteLine($"Loading data...");
                data = LoadData(input);
                Console.WriteLine($"Length -> {data.Count()}");
            });
            Utils.Utils.MeasureActionTime("Part 1", () =>
            {
                var res = DoMagic(data);
                Console.WriteLine($"Result -> {res}");
            });
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<Tuple<int, IEnumerable<Tuple<int, string>>>> LoadData(string filename)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            List<Tuple<int, IEnumerable<Tuple<int, string>>>> res = new List<Tuple<int, IEnumerable<Tuple<int, string>>>>();
            while ((line = file.ReadLine()) != null)
            {
                int game = Int32.Parse(line.Split(':')[0].Split(' ')[1]);
                IEnumerable<Tuple<int, string>> cubes = line.Split(':')[1].Split(";").SelectMany((s, i) => s.Split(',')).Select((game, i) => new Tuple<int, string>(Int32.Parse(game.Trim().Split(' ')[0]), game.Trim().Split(' ')[1]));
                res.Add(new Tuple<int, IEnumerable<Tuple<int, string>>>(game, cubes));
            }
            file.Close();
            return res;
        }

        static long DoMagic(IEnumerable<Tuple<int, IEnumerable<Tuple<int, string>>>> input)
        {
            int red = 12;
            int green = 13;
            int blue = 14;

            int res = 0;
            foreach (var game in input)
            {
                bool isValid = true;
                foreach (var cubes in game.Item2)
                {
                    if (cubes.Item2 == "red" && cubes.Item1 > red)
                    {
                        isValid = false;
                        break;
                    }
                    if (cubes.Item2 == "blue" && cubes.Item1 > blue)
                    {
                        isValid = false;
                        break;
                    }
                    if (cubes.Item2 == "green" && cubes.Item1 > green)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    res += game.Item1;
                }
            }
            return res;
        }

        static long DoMagic2(IEnumerable<Tuple<int, IEnumerable<Tuple<int, string>>>> input)
        {
            int res = 0;
            foreach (var game in input)
            {
                int red = 0;
                int green = 0;
                int blue = 0;
                foreach (var cubes in game.Item2)
                {
                    if (cubes.Item2 == "red" && cubes.Item1 > red)
                    {
                        red = cubes.Item1;
                        continue;
                    }
                    if (cubes.Item2 == "blue" && cubes.Item1 > blue)
                    {
                        blue = cubes.Item1;
                        continue;
                    }
                    if (cubes.Item2 == "green" && cubes.Item1 > green)
                    {
                        green = cubes.Item1;
                        continue;
                    }
                }
                res += red * blue * green;

            }
            return res;
        }
    }
}
