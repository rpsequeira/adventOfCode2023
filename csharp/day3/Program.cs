using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;

namespace adventofcode2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 3 of AoC");
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
                var res = DoMagic(data.ToList());
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

        static long DoMagic(List<string> input)
        {
            int res = 0;
            for (int i = 0; i < input.Count; i++)
            {
                string temp = "";
                bool notPart = true;
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (!char.IsNumber(input[i][j]) || input[i][j] == '.')
                    {

                        if (!string.IsNullOrEmpty(temp) && !notPart)
                        {
                            res += Int32.Parse(temp);
                        }
                        temp = "";
                        notPart = true;
                        continue;
                    }
                    temp += input[i][j];
                    if ((i > 0 && IsSymbol(input[i - 1][j])) || (i < input.Count - 1 && IsSymbol(input[i + 1][j])))
                    {
                        notPart = false;
                    }
                    if ((j > 0 && IsSymbol(input[i][j - 1])) || (j < input[i].Length - 1 && IsSymbol(input[i][j + 1])))
                    {
                        notPart = false;
                    }
                    if (j > 0 && ((i > 0 && IsSymbol(input[i - 1][j - 1])) || (i < input.Count - 1 && IsSymbol(input[i + 1][j - 1]))))
                    {
                        notPart = false;
                    }
                    if (j < input[i].Length - 1 && ((i > 0 && IsSymbol(input[i - 1][j + 1])) || (i < input.Count - 1 && IsSymbol(input[i + 1][j + 1]))))
                    {
                        notPart = false;
                    }

                    if (j + 1 == input[i].Length)
                    {
                        if (!string.IsNullOrEmpty(temp) && !notPart)
                        {
                            res += Int32.Parse(temp);
                        }
                    }
                }
            }
            return res;
        }

        static long DoMagic2(List<string> input)
        {
            int res = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '*')
                    {
                        Dictionary<Tuple<int, int>, int> numbers = new Dictionary<Tuple<int, int>, int>();
                        // top row
                        if (i > 0)
                        {
                            if (char.IsNumber(input[i - 1][j]))
                            {
                                var num = GetNumber(input, i - 1, j);
                                numbers.TryAdd(num.Item1, num.Item2);
                            }

                            if (j > 0 && char.IsNumber(input[i - 1][j - 1]))
                            {
                                var num = GetNumber(input, i - 1, j - 1);
                                numbers.TryAdd(num.Item1, num.Item2); ;
                            }

                            if (j < input[i - 1].Length - 1 && char.IsNumber(input[i - 1][j + 1]))
                            {
                                var num = GetNumber(input, i - 1, j + 1);
                                numbers.TryAdd(num.Item1, num.Item2);
                            }

                        }
                        // Middle row
                        if (j > 0 && char.IsNumber(input[i][j - 1]))
                        {
                            var num = GetNumber(input, i, j - 1);
                            numbers.TryAdd(num.Item1, num.Item2);
                        }
                        if (j < input[i].Length - 1 && char.IsNumber(input[i][j + 1]))
                        {
                            var num = GetNumber(input, i, j + 1);
                            numbers.TryAdd(num.Item1, num.Item2);
                        }
                        // bottom row
                        if (i + 1 < input.Count)
                        {
                            if (char.IsNumber(input[i + 1][j]))
                            {
                                var num = GetNumber(input, i + 1, j);
                                numbers.TryAdd(num.Item1, num.Item2);
                            }

                            if (j > 0 && char.IsNumber(input[i + 1][j - 1]))
                            {
                                var num = GetNumber(input, i + 1, j - 1);
                                numbers.TryAdd(num.Item1, num.Item2);
                            }

                            if (j < input[i + 1].Length - 1 && char.IsNumber(input[i + 1][j + 1]))
                            {
                                var num = GetNumber(input, i + 1, j + 1);
                                numbers.TryAdd(num.Item1, num.Item2);
                            }
                        }


                        if (numbers.Count == 2)
                        {
                            var x = 1;
                            foreach (var item in numbers)
                            {
                                x *= item.Value;
                            }
                            res += x;
                        }
                    }
                }
            }
            return res;
        }

        static bool IsSymbol(char i)
        {
            return !char.IsNumber(i) && i != '.';
        }

        static (Tuple<int, int>, int) GetNumber(List<string> input, int i, int j)
        {
            // Console.WriteLine($"getting {i},{j}");
            int start = j;
            while (start - 1 >= 0)
            {
                if (char.IsNumber(input[i][start - 1]) && input[i][start - 1] != '.')
                {
                    start--;
                }
                else
                {
                    break;
                }
            }
            var temp = "";
            var realStart = start;
            while (start < input[i].Length && char.IsNumber(input[i][start]) && input[i][start] != '.')
            {
                temp += input[i][start++];
            }
            // Console.WriteLine($"Number {temp}");
            return (new Tuple<int, int>(i, realStart), Int32.Parse(temp));

        }
    }
}
