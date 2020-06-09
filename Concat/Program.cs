using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Concat
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\tma\source\repos\Coding game\Skynet";

            if (Directory.Exists(path))
            {
                ProcessDirectory(path);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.cs", SearchOption.AllDirectories);
            List<String> use = new List<string>();
            List<String> content = new List<string>();
            foreach (string fileName in fileEntries)
            {
                if (Path.GetExtension(fileName) == ".cs" && !fileName.Contains("\\bin\\") && !fileName.Contains("\\obj\\") && !fileName.Contains("\\Properties\\"))
                {
                    var lines = File.ReadAllLines(fileName);
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("using"))
                        {
                            if (!use.Contains(line))
                            {
                                use.Add(line);
                            }
                        }
                        else
                        {
                            content.Add(line);
                        }
                    }
                }
            }

            TextWriter tw = new StreamWriter("SavedList.txt");

            foreach (String s in use) tw.WriteLine(s);
            foreach (String s in content) tw.WriteLine(s);

            tw.Close();
        }
    }
}
