using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool.Trees
{
    /// <summary>
    /// Represents a terminal branch of a phrase structure tree.
    /// </summary>
    class Terminal
    {
        /// <summary>
        /// Represents a word in the phrase
        /// </summary>
        public String Word { get; private set; }
        /// <summary>
        /// The lexical category of the word in the phrase.
        /// </summary>
        public LexicalCategory Category { get; private set; }

        public Terminal(String word, LexicalCategory category)
        {
            Word = word;
            Category = category;
        }
    }
}
