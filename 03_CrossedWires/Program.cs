using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace _03_CrossedWires
{
    class Program
    {
        static void Main()
        {
            bool test = false;

            string[] path = GetData(test);

            int answer;
            
            answer = Part1(path);
            Console.WriteLine(answer);

            answer = Part2(path);
            Console.WriteLine(answer);
        }

        static int Part1(string[] path)
        {
            List<Point> One = new List<Point>();
            List<Point> Two = new List<Point>();

            Size offset;
            Point current;

            current = new Point(0, 0);
            foreach (string s in path[0].Split(new char[] { ',' }))
            {
                offset = GetOffset(s[0]);
                for (int i = 1; i <= int.Parse(s.TrimStart(s[0])); i++)
                {
                    current = Point.Add(current, offset);
                    One.Add(current);
                }
            }

            current = new Point(0, 0);
            foreach (string s in path[1].Split(new char[] { ',' }))
            {
                offset = GetOffset(s[0]);
                for (int i = 1; i <= int.Parse(s.TrimStart(s[0])); i++)
                {
                    current = Point.Add(current, offset);
                    if (One.Contains(current))
                        Two.Add(current);
                }
            }

            int min = Math.Abs(Two[0].X) + Math.Abs(Two[0].Y);
            foreach (var v in Two)
            {
                int dist = Math.Abs(v.X) + Math.Abs(v.Y);
                if (dist < min)
                {
                    min = dist;
                }
            }
            return min;
        }
        static int Part2(string[] path)
        {
            List<Point> One = new List<Point>();
            List<Point> Two = new List<Point>();
            List<Point> found = new List<Point>();

            Size offset;
            Point current;

            current = new Point(0, 0);
            foreach (string s in path[0].Split(new char[] { ',' }))
            {
                offset = GetOffset(s[0]);
                for (int i = 1; i <= int.Parse(s.TrimStart(s[0])); i++)
                {
                    current = Point.Add(current, offset);
                    One.Add(current);
                }
            }

            current = new Point(0, 0);
            foreach (string s in path[1].Split(new char[] { ',' }))
            {
                offset = GetOffset(s[0]);
                for (int i = 1; i <= int.Parse(s.TrimStart(s[0])); i++)
                {
                    current = Point.Add(current, offset);
                    Two.Add(current);
                    if (One.Contains(current))
                        found.Add(current);
                }
            }

            int? min = null;

            foreach (var v in found)
            {
                int dist;

                dist = One.IndexOf(v) + Two.IndexOf(v);

                if (!min.HasValue || dist < min)
                {
                    min = dist;
                }
            }
            return min.Value + 2;
        }

        static Size GetOffset(char s)
        {
            return (s) switch
            {
                'U' => new Size(0, 1),
                'R' => new Size(1, 0),
                'D' => new Size(0, -1),
                'L' => new Size(-1, 0),
                _ => new Size(0, 0),
            };
        }

        static string[] GetData(bool test)
        {
            if (test)
            {
                string[] theData = { "R8,U5,L5,D3", "U7,R6,D4,L4" };  //--- should be 6/30
                //string[] theData = { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" }; //--- Should be 159/610
                //string[] theData = { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" }; //--- should be 135/410


                return theData;
            }
            else
            {
                return File.ReadAllLines("data.txt");
            }
        }

    }
}
