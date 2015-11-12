using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool
{
    static class CSV
    {
        /// <summary>
        /// Returns a 2D array of CSV data.
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>Data is stored by row, then column</returns>
        public static String[][] parseCSV(String filePath) {
            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(@filePath));
                List<String[]> allWords = new List<String[]>();
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    String[] words = line.Split(',');
                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();
                    }
                    allWords.Add(words);
                }
                return allWords.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
