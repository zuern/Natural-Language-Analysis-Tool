using NLA_Tool.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool
{
    class Program
    {
        private static float    version         = 1.1F;
        private static String   InputPhrase     = "Mark likes the dog.";
        private static String   LexiconPath     = "../../dict.csv";
        public static bool      verboseOutput   = false;

        /// <summary>
        /// Main point of entry for the program
        /// </summary>
        /// <param name="args">Arguments passed to the program from the terminal</param>
        static void Main(string[] args)
        {
            // Set class variables with data from args
            //processArgs(args);

            // Print header info to console
            init();

            // Load the dictionary
            Lexicon dictionary = new Lexicon(LexiconPath);

            // Generate the phrase structure tree.
            Phrase phraseTree = TreeFactory.buildTree(InputPhrase,dictionary);

            phraseTree.PrintTreeStructureToConsole();

            // Wait for further instructions.
            Console.ReadKey();

        }
        
        /// <summary>
        /// Process the arguments supplied to the program from the console
        /// </summary>
        /// <param name="args">The arguments from the console</param>
        private static void processArgs(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    if (arg == "-V")
                        verboseOutput = true;
                    else
                        InputPhrase = arg;
                }
            }
            if (InputPhrase.Length < 1)
                throw new Exception("Need an input phrase to run!");
        }
        
        /// <summary>
        /// Initializes the program
        /// </summary>
        private static void init()
        {
            Console.WriteLine("**************************************\n* Natural Language Processing Script *\n*               V" + NLA_Tool.Program.version.ToString() + "                 *\n*            Kevin Zuern             *\n**************************************");
            Console.WriteLine(String.Format("Processing the following phrase now: \"{0}\"", InputPhrase));
        }
    }
}
