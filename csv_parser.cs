using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSV_Parser
{
    static class Program
    {   // regular expression creating
        static Regex regex = new Regex(",(?=([^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the file path:");
            string path;
            try
            {
                path = Console.ReadLine();
                //Opens a text file, 
                //reads all lines of the file, 
                //and then closes the file
                string[] lines = File.ReadAllLines(path);
                Console.WriteLine("Enter the column number to sort:");
                int sort;
                try
                {   
                    sort = Convert.ToInt32(Console.ReadLine());
                }
                //handling a case with invalid column number
                catch
                {   //warning
                    Console.WriteLine("The sorting column is incorrect.
                         \n Sort by default on the 1st column");
                    //sort by first column
                    sort = 1;
                }
                //splitting lines by ',' character
                // & line sorting
                var data = lines.Skip(0);
                var sorted = data.Select(line => new
                {
                    SortKey = (line.Split(',')[sort]),
                    Line = line
                })
                      .OrderBy(x => x.SortKey)
                      .Select(x => x.Line);
                //character replacement
                foreach (string s in sorted)
                {
                    Console.WriteLine(Foo(s).Replace('"', ' '));
                }
            }
            //handling a case with missing file or incorrectly specified path
            catch
            {
                Console.WriteLine("File not found or path was incorrectly specified");
            }
            Console.ReadKey();
        }
        static string Foo(string s)
        {   //replacing a separators for correct output
            string a = regex.Replace(s, " |");
            string b = a.Replace("  ", " |");
            return b;
        }
    }
}