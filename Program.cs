using NLA_Tool.Trees;
using System;
using NLA_Tool.Lexical;

namespace NLA_Tool
{
    class Program
    {
        private static float    _version         = 1.1F;
        private static string   _inputPhrase     = "Mark drank some sour milk and now he feels very sick.";
        private static string   _lexiconPath     = "../../dict.csv";
        private static bool      VerboseOutput;

        /// <summary>
        /// Main point of entry for the program
        /// </summary>
        /// <param name="args">Arguments passed to the program from the terminal</param>
        static void Main(string[] args)
        {
            // Set class variables with data from args
            ProcessArgs(args);

            // Print header info to console
            Init();

            // Load the dictionary
            var dictionary = new Lexicon(_lexiconPath);

            // Generate the phrase structure tree.
            var phraseTree = TreeFactory.BuildTree(_inputPhrase,dictionary);

            phraseTree.PrintTreeStructureToConsole();

            // Wait for further instructions.
            Console.ReadKey();

        }
        
        /// <summary>
        /// Process the arguments supplied to the program from the console
        /// </summary>
        /// <param name="args">The arguments from the console</param>
        private static void ProcessArgs(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (arg == "-V")
                        VerboseOutput = true;
                    else
                        _inputPhrase = arg;
                }
            }
            if (_inputPhrase.Length < 1)
                throw new Exception("Need an input phrase to run!");
        }
        
        /// <summary>
        /// Initializes the program
        /// </summary>
        private static void Init()
        {
            Console.WriteLine("**************************************\n* Natural Language Processing Script *\n*               V" + _version.ToString() + "                 *\n*            Kevin Zuern             *\n**************************************");
            Console.WriteLine($"Processing the following phrase now: \"{_inputPhrase}\"");
        }
    }
}
