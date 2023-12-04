using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;
using System.Text.RegularExpressions;
using System.Globalization;

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
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data.ToList());
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<string> LoadData(string filename)
        {
            return Utils.Utils.ReadAllLines(filename);
        }

        static long DoMagic(IEnumerable<string> input)
        {
            var res = 0;
            foreach (var line in input)
            {
                var cardRes = 0;
                var numbers = line.Split(':')[1].Trim();
                var winningNumbers = numbers.Split("|")[0].Trim().Split(' ').Where(s => { return s.Trim() != "";}).Select(Int32.Parse);
                var myNumbers = numbers.Split("|")[1].Trim().Split(' ').Where(s => { return s.Trim() != "";}).Select(Int32.Parse);
                var wonNumbers = winningNumbers.Intersect(myNumbers).Count();
                if (wonNumbers > 0){
                    cardRes = 1;
                    while (wonNumbers > 1){
                        cardRes *= 2;
                        wonNumbers--;
                    }
                }
                res += cardRes;
            }
            return res;
        }

        static long DoMagic2(List<string> input)
        {
            int[] copies = new int[input.Count];
            Array.Fill(copies,1);
            for (int i = 0; i < input.Count; i++)
            {
                var numbers = input[i].Split(':')[1].Trim();
                var winningNumbers = numbers.Split("|")[0].Trim().Split(' ').Where(s => { return s.Trim() != "";}).Select(Int32.Parse);
                var myNumbers = numbers.Split("|")[1].Trim().Split(' ').Where(s => { return s.Trim() != "";}).Select(Int32.Parse);
                var wonNumbers = winningNumbers.Intersect(myNumbers).Count();
                for (int j = 0; j < copies[i]; j++)
                {
                    for (int k = 1; k <= wonNumbers; k++)
                    {
                        copies[i+k]++;
                    }
                }               
            }
            return copies.Sum();
        }
    }
}
