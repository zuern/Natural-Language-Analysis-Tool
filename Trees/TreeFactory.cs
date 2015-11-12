using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool.Trees
{
    static class TreeFactory
    {
        /// <summary>
        /// Recursively builds a phrase structure tree from the given word.
        /// </summary>
        /// <param name="words">The array of words that make up the phrase</param>
        /// <returns>The sentence structured into a hierarchical tree</returns>
        public static Phrase buildTree(string InputPhrase, Lexicon d)
        {
            String[] words = processPhrase(InputPhrase);

            Terminal[] terminals = new Terminal[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                terminals[i] = new Terminal(words[i], d.lookupCategory(words[i]));
            }

            return merge(terminals);
        }

        /// <summary>
        /// Recursively creates a phrase structure tree with the given word set and Lexicon.
        /// </summary>
        /// <param name="d">The lexicon with definitions to use</param>
        /// <param name="terminals">The phrase to generate a tree with</param>
        /// <returns>A phrase structure tree representing the hierarchy of structures of the sentence.</returns>
        private static Phrase merge(Terminal[] terminals)
        {
            Terminal leftTerminal = terminals[0];

            // Base case
            if (terminals.Length == 1)
            {
                Intermediate i = new Intermediate(leftTerminal);
                return new Phrase(i);
            }
            else if (terminals.Length == 2)
            {
                switch (terminals[0].Category)
                {
                    case LexicalCategory.Determiner:
                        return new Phrase(new Phrase(new Intermediate(terminals[0])), new Intermediate(terminals[1]));
                }
            }
            Intermediate intermediate = new Intermediate(leftTerminal, merge(terminals.Skip(1).ToArray()));
            return new Phrase(intermediate);
        }

        /// <summary>
        /// Takes a sentence and returns an array of words
        /// </summary>
        /// <param name="InputPhrase">The sentence to process</param>
        /// <returns>Array of strings, each element is a single word</returns>
        private static string[] processPhrase(string InputPhrase)
        {
            String[] words = InputPhrase.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                formatWord(ref words[i]);
            }

            return words;
        }

        /// <summary>
        /// Removes leading and trailing whitespace and removes any punctuation marks.
        /// </summary>
        /// <param name="word">The word to format.</param>
        private static void formatWord(ref string word)
        {
            string newString = "";
            foreach (char c in word)
            {
                if (!Char.IsPunctuation(c))
                {
                    newString += c;
                }
            }
            word = newString.ToLower();
        }
    }
}
