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
            Console.WriteLine("Day 1 of AoC");
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
                var res = DoMagic2(data);
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
            int total = 0;
            foreach (var line in input)
            {
                string number = "";
                foreach (var item in line)
                {
                    if (char.IsNumber(item)) {
                        number += item;
                    }
                }
                string finalNumber = "";
                finalNumber += number[0];
                finalNumber += number[number.Length-1];
                total += Int32.Parse(finalNumber);
            }
            return total;
        }

        static long DoMagic2(IEnumerable<string> input)
        {
            int total = 0;
            foreach (var line in input)
            {
                string number = "";
                string tempLine = line;
                tempLine = tempLine.Replace("one","one1one");
                tempLine = tempLine.Replace("two","two2two");
                tempLine = tempLine.Replace("three","three3three");
                tempLine = tempLine.Replace("four","four4four");
                tempLine = tempLine.Replace("five","five5five");
                tempLine = tempLine.Replace("six","six6six");
                tempLine = tempLine.Replace("seven","seven7seven");
                tempLine = tempLine.Replace("eight","eight8eight");
                tempLine = tempLine.Replace("nine","nine9nine");
                foreach (var item in tempLine)
                {
                    if (char.IsNumber(item)) {
                        number += item;
                    }
                }
                string finalNumber = "";
                finalNumber += number[0];
                finalNumber += number[number.Length-1];
                total += Int32.Parse(finalNumber);
            }
            return total;
        }
    }
}
