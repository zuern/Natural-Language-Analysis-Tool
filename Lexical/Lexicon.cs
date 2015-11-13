using NLA_Tool.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLA_Tool
{
    class Lexicon
    {
        private Terminal[] entries;

        public Terminal this[long index]
        {
            get { return entries[index]; }
            set { entries[index] = value; }
        }
        
        public Lexicon(string filePath)
        {
            String[][] csvData = CSV.parseCSV(filePath);

            List<Terminal> definitions = new List<Terminal>();
            foreach (var line in csvData)
            {
                string word = line[0];
                string definition = line[1];

                Terminal d = new Terminal(word, stringtoLexicalCategory(definition));

                definitions.Add(d);
            }

            entries = definitions.ToArray();
        }

        /// <summary>
        /// Takes a string category <example>"noun"</example> and returns the equivalent <c>LexicalCategory</c>
        /// </summary>
        /// <param name="category">the string category name <example>"verb"</example></param>
        /// <returns>A <c>LexicalCategory</c> enum.</returns>
        public LexicalCategory stringtoLexicalCategory(string category)
        {
            return (LexicalCategory) Enum.Parse(typeof(LexicalCategory),category,true);
        }

        /// <summary>
        /// Looks up the lexical category of the specified word in the dictionary
        /// </summary>
        /// <param name="word">The word to look up the category for.</param>
        /// <returns><c>LexicalCategory</c> to which the word belongs.</returns>
        public LexicalCategory lookupCategory(string word)
        {
            foreach (Terminal t in entries)
                if (t.Word == word) return t.Category;
            throw new KeyNotFoundException("Could not find the word in the dictionary: \"" + word + "\"");
        }
    }
}
