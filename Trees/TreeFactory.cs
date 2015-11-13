using System;
using System.Collections.Generic;
using System.Linq;

using static NLA_Tool.Trees.LexicalCategory;
using static NLA_Tool.Trees.PhraseCategory;

namespace NLA_Tool.Trees
{
    internal static class TreeFactory
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

            // In case we don't find the words in the dictionary collect all the error messages
            List<KeyNotFoundException> wordsNotFound = new List<KeyNotFoundException>();

            for (int i = 0; i < words.Length; i++)
            {
                try
                {
                    terminals[i] = new Terminal(words[i], d.lookupCategory(words[i]));
                }
                catch (KeyNotFoundException e)
                {
                    wordsNotFound.Add(e);
                    continue;
                }
            }

            if (wordsNotFound.Count > 0)
            {
                foreach (KeyNotFoundException e in wordsNotFound) Console.WriteLine(e.Message);
                Console.Write("\nPress any key to exit");
                Console.ReadKey();
                System.Environment.Exit(-1); // Completed with errors.
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
            // Base case
            if (terminals.Length == 1)
                return new Phrase(new Intermediate(terminals[0]));

            // If we get here, then there are more than 1 word in the array.
            Phrase rightPhrase = merge(terminals.Skip(1).ToArray());
            Terminal c = terminals[0]; // For quick reference.
            Intermediate currentIntermediate = new Intermediate(terminals[0]);
            Phrase currentPhrase = new Phrase(currentIntermediate);

            //
            // Try and find ways to assign currentIntermediate as a specifier for rightPhrase. If not, add it as a head of a new parent phrase
            //

            // NP with DP specifier
            if (rightPhrase.PhraseCategory == NP && c.Category == Determiner)
            {
                rightPhrase.Specifier = currentPhrase; // Set the current word as a specifier for the phrase
                return rightPhrase;
            }
            // NP with AP specifier
            else if (rightPhrase.PhraseCategory == NP && c.Category == Adjective)
            {
                rightPhrase.Specifier = currentPhrase;
                return rightPhrase;
            }

            // SPECIAL CASE: if currentPhrase is a NP and rightPhrase is a VP, this is a TP (i.e. sentence)!
            else if ((currentPhrase.PhraseCategory == NP && rightPhrase.PhraseCategory == VP))
            {
                Phrase TP = new Phrase(currentPhrase, new Intermediate(new Terminal("T",LexicalCategory.Tense),rightPhrase));
                return TP;
            }

            // If we get this far, just set the currentPhrase as the parent phrase with rightPhrase as its complement
            currentPhrase.Intermediate.Complement = rightPhrase;
            return currentPhrase;
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
