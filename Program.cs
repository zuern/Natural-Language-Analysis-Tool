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
        private static String   InputPhrase     = "";
        private static String   LexiconPath     = "../../dict.csv";
        private static String   RulesPath       = "../../PhraseRules.csv";
        public static bool      verboseOutput   = false;

        static void Main(string[] args)
        {
            // Set class variables with data from args
            processArgs(args);

            // Print header info to console
            init();

            // Load the dictionary
            Lexicon dictionary = new Lexicon(LexiconPath);

            // Generate the phrase structure tree.
            Phrase phraseTree = Trees.TreeFactory.buildTree(InputPhrase,dictionary);

            printTree(phraseTree,0);

            // Wait for further instructions.
            Console.ReadKey();

        }

        private static void printTree(Phrase phraseTree, int level)
        {
            p(phraseTree.Intermediate.PhraseCategory.ToString(), level);
            p(phraseTree.Intermediate.Head.Word, level);

            if (phraseTree.Intermediate.hasComplement)
            {
                printTree(phraseTree.Intermediate.Complement, level + 1);
            }
        }

        private static void p(String text,int level)
        {
            string p1 = "=> ";
            p1 = new String(' ', level * 2) + p1;

            Console.WriteLine(p1 + text);
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
