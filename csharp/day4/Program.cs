using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;
using System.Text.RegularExpressions;

namespace adventofcode2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 4 of AoC");
            IEnumerable<string> data = null;
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
            // Utils.Utils.MeasureActionTime("Part 2", () =>
            // {
            //     var res = DoMagic2(data);
            //     Console.WriteLine($"Result -> {res}");
            // });
            Console.WriteLine("END");
        }

        static IEnumerable<string> LoadData(string filename)
        {
            return Utils.Utils.ReadAllLines(filename);
        }

        static long DoMagic(IEnumerable<string> input)
        {
            return -1;
        }

        static long DoMagic2(IEnumerable<string> input)
        {
            return -1;
        }
    }
}
