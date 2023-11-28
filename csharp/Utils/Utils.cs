using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Utils
{
    public static class Utils
    {
        public static string GetHelloWorld()
        {
            return "Hello world!(Tools)";
        }

        public static IEnumerable<string> ReadAllLines(string filename)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
            file.Close();
        }

        public static IEnumerable<int> ReadAlllLinesAndSeparateComas(string filename)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            List<int> res = new List<int>();
            while ((line = file.ReadLine()) != null)
            {
                res.AddRange(line.Split(',').Select(Int32.Parse));
            }
            file.Close();
            return res;
        }

        public static void PrintArray<T>(IEnumerable<T> input)
        {
            Console.Write("Printing array:");
            foreach (var item in input)
            {
                Console.Write(item.ToString() + ",");
            }
            Console.WriteLine("|END");
        }

        public static void ParallelFor<T>(IEnumerable<T> input, Action<int> action)
        {
            Parallel.For(0, input.Count(), new ParallelOptions { MaxDegreeOfParallelism = 4 }, (i, state) => action(i));
        }

        public static void ParallelForEach<T>(IEnumerable<T> input, Action<T> action)
        {
            Parallel.ForEach(input, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (item, state) => action(item));
        }

        public static void MeasureActionTime(string desc, Action action)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            action();
            Console.WriteLine($"{desc} took {stopwatch.ElapsedMilliseconds} ms");
        }

        public static IEnumerable<List<PointInfo<int>>> GetMatrix(IEnumerable<string> input, char separator = ' ')
        {
            for (int i = 0; i < input.Count(); i++)
            {
                if (input.ToList()[i].Trim() == string.Empty)
                {
                    continue;
                }
                yield return input.ToList()[i].Split(separator).Where(s => s.Trim() != string.Empty).Select(s => new PointInfo<int>(Int32.Parse(s.Trim()))).ToList();
            }
        }

        public static IEnumerable<List<PointInfo<int>>> GetMatrix(IEnumerable<string> input)
        {
            for (int i = 0; i < input.Count(); i++)
            {
                if (input.ToList()[i].Trim() == string.Empty)
                {
                    continue;
                }
                yield return input.ToList()[i].ToCharArray().Select(s => new PointInfo<int>(Int32.Parse(s.ToString()))).ToList();
            }
        }


        public static void ResetMatrix(List<List<PointInfo<int>>> matrix)
        {
            for (int j = 0; j < matrix.Count(); j++)
            {
                for (int i = 0; i < matrix[j].Count(); i++)
                {
                    matrix[j][i].flag = false;
                }
            }
        }

        public static int SumMatrix(List<List<PointInfo<int>>> matrix)
        {
            int sum = 0;
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    if (!col.flag)
                    {
                        sum += col.value;
                    }
                }
            }
            return sum;
        }

        [Obsolete]
        public static IEnumerable<List<Tuple<int, bool>>> GetMatrix(IEnumerable<string> input, bool optional = true, char separator = ' ')
        {
            for (int i = 0; i < input.Count(); i++)
            {
                if (input.ToList()[i].Trim() == string.Empty)
                {
                    continue;
                }
                yield return input.ToList()[i].Split(separator).Where(s => s.Trim() != string.Empty).Select(s => new Tuple<int, bool>(Int32.Parse(s.Trim()), false)).ToList();
            }
        }

        [Obsolete]
        public static int SumMatrix(List<List<Tuple<int, bool>>> matrix)
        {
            int sum = 0;
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    if (!col.Item2)
                    {
                        sum += col.Item1;
                    }
                }
            }
            return sum;
        }

        public static void AddRange<T>(this ConcurrentBag<T> @this, IEnumerable<T> toAdd)
        {
            foreach (var element in toAdd)
            {
                @this.Add(element);
            }
        }
    }

    public struct Point
    {
        public int X { get { return _x; } }
        private int _x;
        public int Y { get { return _y; } }
        private int _y;
        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (_x == p.X) && (_y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return (_x << 2) ^ _y;
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }

    public class PointInfo<T>
    {
        public T value { get; set; }

        public bool flag { get; set; }

        public PointInfo(T value, bool flag = false)
        {
            this.value = value;
            this.flag = flag;
        }
    }
}
