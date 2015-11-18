using System;
using System.Collections.Generic;
using System.Linq;
using NLA_Tool.Exceptions;
using NLA_Tool.Lexical;
using NLP_DictionaryGenerator;
using static NLA_Tool.Trees.LexicalCategory;
using static NLA_Tool.Trees.PhraseCategory;

namespace NLA_Tool.Trees
{
    internal static class TreeFactory
    {
        /// <summary>
        ///     Recursively builds a phrase structure tree from the given word.
        /// </summary>
        /// <param name="inputPhrase">The phrase to make a tree with</param>
        /// <param name="dictionary">The dictionary to use when building the tree with</param>
        /// <returns>The sentence structured into a hierarchical tree</returns>
        public static Phrase BuildTree(string inputPhrase, Lexicon dictionary)
        {
            var words = ProcessPhrase(inputPhrase);

            Terminal[] terminals = new Terminal[words.Length];

            // In case we don't find the words in the dictionary collect all the error messages
            var wordsNotFound = new List<WordNotFoundException>();

            for (var i = 0; i < words.Length; i++)
            {
                try
                {
                    terminals[i] = new Terminal(words[i], dictionary.LookupCategory(words[i]));
                }
                catch (WordNotFoundException e)
                {
                    wordsNotFound.Add(e);
                }
            }

            if (wordsNotFound.Count > 0)
            {
                foreach (var wordNotFoundException in wordsNotFound)
                {
                    try
                    {
                        var definition = DictionaryGenerator
                            .GetDictionaryEntry(wordNotFoundException.Word);
                        dictionary.AddToDictionary(definition);
                    }
                    catch (DictionaryLookupException) // Couldn't find the word in the Merriam-Webster dictionary.
                    {
                        var category = GetCategoryFromUser(wordNotFoundException.Word, dictionary);
                        dictionary.AddToDictionary(wordNotFoundException.Word, category);
                    }
                }

                return BuildTree(inputPhrase, dictionary);
            }

            return Merge(terminals);
        }

        /// <summary>
        ///     Removes leading and trailing whitespace and removes any punctuation marks.
        /// </summary>
        /// <param name="word">The word to format.</param>
        private static void FormatWord(ref string word)
        {
            var newString = "";
            foreach (var c in word)
            {
                if (!char.IsPunctuation(c))
                {
                    newString += c;
                }
            }
            word = newString.ToLower();
        }

        /// <summary>
        ///     Get the user to manually define the lexical category of the specified word and save it to the dictionary.
        /// </summary>
        /// <param name="word">The word that the user should define.</param>
        /// <param name="dictionary">The dictionary to use</param>
        /// <returns>The <see cref="LexicalCategory" /> of the Word or null if user's definition is not a valid category</returns>
        private static LexicalCategory GetCategoryFromUser(string word, Lexicon dictionary)
        {
            Console.WriteLine("Couldn't look up the category for \"" + word + "\"");
            Console.WriteLine("Please type in the lexical category of \"" + word + "\" or just press enter to exit.");
            var category = Console.ReadLine();

            if (category == "")
            {
                Environment.Exit(1);
            }
            try
            {
                return dictionary.StringtoLexicalCategory(category);
            }
            catch (Exception)
            {
                GetCategoryFromUser(word, dictionary);
            }

            return Noun; // Hack to make sure all code paths return a value.
        }

        /// <summary>
        ///     Recursively creates a phrase structure tree with the given word set and Lexicon.
        /// </summary>
        /// <param name="terminals">The phrase to generate a tree with</param>
        /// <returns>A phrase structure tree representing the hierarchy of structures of the sentence.</returns>
        private static Phrase Merge(Terminal[] terminals)
        {
            if (terminals == null) throw new ArgumentNullException(nameof(terminals));
            // Base case
            if (terminals.Length == 1)
                return new Phrase(new Intermediate(terminals[0]));

            // If we get here, then there are more than 1 word in the array.
            var rightPhrase = Merge(terminals.Skip(1).ToArray());
            var c = terminals[0]; // For quick reference.
            var currentIntermediate = new Intermediate(terminals[0]);
            var currentPhrase = new Phrase(currentIntermediate);

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
            if (rightPhrase.PhraseCategory == NP && c.Category == Adjective)
            {
                rightPhrase.Specifier = currentPhrase;
                return rightPhrase;
            }

            // SPECIAL CASE: if currentPhrase is a NP and rightPhrase is a VP, this is a IP (i.e. sentence)!
            if (currentPhrase.PhraseCategory == NP && rightPhrase.PhraseCategory == VP)
            {
                var tp = new Phrase(currentPhrase, new T_Bar(new Terminal("{Tense Marker}", Tense), rightPhrase));
                return tp;
            }

            // If we get this far, just set the currentPhrase as the parent phrase with rightPhrase as its complement
            currentPhrase.Intermediate.Complement = rightPhrase;
            return currentPhrase;
        }

        /// <summary>
        ///     Takes a sentence and returns an array of words
        /// </summary>
        /// <param name="inputPhrase">The sentence to process</param>
        /// <returns>Array of strings, each element is a single word</returns>
        private static string[] ProcessPhrase(string inputPhrase)
        {
            var words = inputPhrase.Split(' ');

            for (var i = 0; i < words.Length; i++)
            {
                FormatWord(ref words[i]);
            }

            return words;
        }
    }
}