using System;
using System.Collections.Generic;
using System.IO;
using NLA_Tool.Exceptions;
using NLA_Tool.Trees;

namespace NLA_Tool.Lexical
{
    public class Lexicon
    {
        private Terminal[] _entries;
        private readonly string _filePath;

        public Terminal this[long index]
        {
            get { return _entries[index]; }
            set { _entries[index] = value; }
        }

        public Lexicon(string filePath)
        {
            _filePath = filePath;

            LoadDictionary(filePath);
        }

        private void LoadDictionary(string filePath)
        {
            String[][] csvData = Csv.ParseCsv(filePath);

            List<Terminal> definitions = new List<Terminal>();
            foreach (var line in csvData)
            {
                string word = line[0];
                string definition = line[1];

                Terminal d = new Terminal(word, StringtoLexicalCategory(definition));

                definitions.Add(d);
            }

            _entries = definitions.ToArray();
        }

        /// <summary>
        /// Takes a string category <example>"noun"</example> and returns the equivalent <c>LexicalCategory</c>
        /// </summary>
        /// <param name="category">the string category name <example>"verb"</example></param>
        /// <returns>A <c>LexicalCategory</c> enum.</returns>
        public LexicalCategory StringtoLexicalCategory(string category)
        {
            if (category == "definite")
                category = "determiner";
            return (LexicalCategory)Enum.Parse(typeof(LexicalCategory), category, true);
        }

        /// <summary>
        /// Looks up the lexical category of the specified word in the dictionary
        /// </summary>
        /// <param name="word">The word to look up the category for.</param>
        /// <returns><c>LexicalCategory</c> to which the word belongs.</returns>
        public LexicalCategory LookupCategory(string word)
        {
            foreach (Terminal t in _entries)
                if (t.Word == word) return t.Category;
            throw new WordNotFoundException("Could not find the word in the dictionary: \"" + word + "\"", word);
        }

        public void AddToDictionary(string word, LexicalCategory category)
        {
            AddToDictionary($"{word}, {category.ToString().ToLower()}");
        }

        /// <summary>
        /// Writes a new entry into the dictionary
        /// </summary>
        /// <param name="definition">This will be written into the dictionary verbatim. Must be in format <c>"[Word], [Category]"</c></param>
        public void AddToDictionary(string definition)
        {
            StreamWriter writer = new StreamWriter(_filePath, true); // Will append to the file.
            writer.WriteLine(definition);
            writer.Flush();
            writer.Close();

            // Reload our copy of the dictionary.
            LoadDictionary(_filePath);
        }
    }
}
