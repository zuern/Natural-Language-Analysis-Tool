using System;
using System.Collections.Generic;
using System.IO;

namespace NLA_Tool
{
    internal static class Csv
    {
        /// <summary>
        /// Returns a 2D array of CSV data.
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>Data is stored by row, then column</returns>
        public static string[][] ParseCsv(string filePath) {
            try
            {
                var reader = new StreamReader(File.OpenRead(@filePath));
                var allWords = new List<string[]>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var words = line.Split(',');
                    for (var i = 0; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();
                    }
                    allWords.Add(words);
                }
                reader.Close();
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
