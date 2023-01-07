
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Day_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

            List<bool> Passes = new List<bool>();

            for (int i = 0; i < input.Length; i = i + 3)
            {
                var line1 = JsonConvert.DeserializeObject<ArrayList>(input[i]);
                var line2 = JsonConvert.DeserializeObject<ArrayList>(input[i + 1]);

                var pair = ParseLists(line1, line2);

                Passes.Add(DoComparison((ArrayList)pair[0], (ArrayList)pair[1]));
            }
        }

        public static bool DoComparison(ArrayList item1, ArrayList item2)
        {
            var smallest = Math.Min(item1.Count, item2.Count);
            for (int i = 0; i < smallest; i++)
            {
                if (item1[i].GetType() == item2[i].GetType())
                {
                    if (item1[i].GetType() == typeof(ArrayList))
                    {
                        if (!DoComparison((ArrayList)item1[i], (ArrayList)item2[i]) || item1.Count > item2.Count) return false;
                    }
                    else if (item1[i].GetType() == typeof(long))
                    {
                        if ((long)item1[i] > (long)item2[i]) return false;
                    }
                }
                else if (item1[i].GetType() != item2[i].GetType())
                {
                    if (item1[i].GetType() == typeof(long))
                    {
                        var arr = new ArrayList();
                        arr.Add(item1[i]);
                        item1[i] = arr;
                    }
                    else if (item2[i].GetType() == typeof(long))
                    {
                        var arr = new ArrayList();
                        arr.Add(item2[i]);
                        item2[i] = arr;
                    }
                    if (!DoComparison((ArrayList)item1[i], (ArrayList)item2[i]) || item1.Count > item2.Count) return false;
                }
            }
            return true;
        }


        public static ArrayList ParseLists(ArrayList array1, ArrayList array2)
        {
            var list1 = ParseList(array1);
            var list2 = ParseList(array2);
            var smallest = Math.Min(list1.Count, list2.Count);
            var swap = new ArrayList();

            if (list1.Count != list2.Count)
            {
                if (list2.Count == smallest)
                {
                    swap = new ArrayList(list2);
                    list2 = new ArrayList(list1);
                    list1 = new ArrayList(swap);
                }
            }
            var output = new ArrayList();
            output.Add(list1);
            output.Add(list2);
            return output;

        }

        public static ArrayList ParseList(ArrayList array)
        {
            var output = new ArrayList();
            foreach (var item in array)
            {
                if (item.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
                {
                    output.Add(ParseList(((JArray)item).ToObject<ArrayList>()));
                }
                else
                {
                    output.Add(item);
                }
            }
            return output;
        }
    }


}