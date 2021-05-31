using System;
using System.IO;
using System.Collections.Generic;

namespace Analizer
{
    class Program
    {
        private static SortedList<string, int> data = new SortedList<string, int>();
        private static double N = 0, n = 0, V = 0;
        public static void Main(string[] args)
        {
            string path = @"C:\Users\7igor\source\repos\Lab2Metrology\Lab2Metrology\Program.cs";

            HashSet<string> blackList = new HashSet<string>();
            blackList.Add("using");
            blackList.Add("namespace");
            blackList.Add("class");
            blackList.Add("public");
            blackList.Add("private");
            blackList.Add("static");

            string[] lines = File.ReadAllLines(path);
            
            foreach (string line in lines)
            {
                string[] input = line.Trim().Split(new Char[] { ' ' });
               
                if (blackList.Contains(input[0]) || input.Length < 2)
                {
                    continue;
                }

                if (input[0].Contains("Console"))
                {   
                    string s;
                    int countT = (line.Length - line.Replace("\"", "").Length) / 2;
                    if (!data.ContainsKey("\"*\""))
                    {
                        data.Add("\"*\"", 0);
                        n++;
                    }
                    data["\"*\""] = data["\"*\""] + countT;
                    N = N + countT;
                    
                    foreach (string word in input)
                    {
                        s = word.Trim(new Char[] { ';', '(', ')' });
                        if (s == "+")
                        {
                            continue;
                        }

                        if (data.ContainsKey(s))
                        {
                            AddValue(s);
                        }
                    }

                    AddValue(";");
                    continue;
                }

                foreach (string word in input)
                {   
                    if (word.Equals("int") || word.Equals("double"))
                    {
                        continue;
                    }

                    string s = word;

                    if (word.Contains(";"))
                    {
                        s = word.Trim(new Char[] { ';' });
                        AddValue(";");
                    }

                    if (word.Length > 1)
                    {
                        s = word.Trim(new Char[] { ',', '(', ')', ';' });
                    }

                    if (word.Contains("Math"))
                    {
                        s = word.Split(new Char[] { '.', '(' })[1];
                    }

                    AddValue(s);
                }
                
            }

            V = N * Math.Log2(data.Count);
            Console.WriteLine("n = " + n + ";\nN = " + N + ";\nОбъем программы равен = " + V + ".");
        }

        private static void AddValue(string s)
        {
            if (!data.ContainsKey(s))
            {
                data.Add(s, 0);
                n++;
                
            }
            data[s] = data[s] + 1;
            N++;
        }
    }
}
