using System;

namespace NLA_Tool.Trees
{
    /// <summary>
    /// Represents a terminal branch of a phrase structure tree.
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// Represents a word in the phrase
        /// </summary>
        public String Word { get; private set; }
        /// <summary>
        /// The lexical category of the word in the phrase.
        /// </summary>
        public LexicalCategory Category { get; }

        public Terminal(String word, LexicalCategory category)
        {
            Word = word;
            Category = category;
        }

        public override string ToString()
        {
            switch (Category)
            {
                case LexicalCategory.Noun:
                    return "N";
                case LexicalCategory.Verb:
                    return "V";
                case LexicalCategory.Adjective:
                    return "Adj";
                case LexicalCategory.Adverb:
                    return "Adv";
                case LexicalCategory.Preposition:
                    return "P";
                case LexicalCategory.Determiner:
                    return "det";
                case LexicalCategory.Auxilliary:
                    return "aux";
                case LexicalCategory.Pronoun:
                    return "N";
                case LexicalCategory.Punctuation:
                    return "";
                case LexicalCategory.Tense:
                    return "I";
                case LexicalCategory.Conjunction:
                    return "C";
                default:
                    throw new Exception($"Couldn't find a way to print {Category}.");
            }
        }
    }
}
