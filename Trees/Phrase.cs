using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool.Trees
{
    public class Phrase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <c>Phrase</c> and sets the <see cref="Intermediate"/>
        /// </summary>
        /// <param name="i">The Intermediate phrase.</param>
        public Phrase(Intermediate i)
        {
            Intermediate = i;
        }

        /// <summary>
        /// Creates a new instance of <c>Phrase</c> and sets the <see cref="Intermediate"/> and <see cref="Specifier"/>.
        /// </summary>
        /// <param name="specifier">The speicifer phrase.</param>
        /// <param name="i">The Intermediate phrase.</param>
        public Phrase(Phrase specifier, Intermediate i)
        {
            Specifier = specifier;
            Intermediate = i;
        }

        #endregion

        #region Private Properties

        private Intermediate intermediate;

        #endregion

        #region Public Properties

        /// <summary>
        /// Optional: A specifier phrase
        /// </summary>
        public Phrase Specifier { get; set; }

        /// <summary>
        /// Required: An intermediate phrase
        /// </summary>
        public Intermediate Intermediate {
            get { return intermediate; }
            set {
                if (value == null)
                    throw new ArgumentNullException("Intermediate cannot be null!");
                else
                    intermediate = value;
            } 
        }

        /// <summary>
        /// Returns true if <see cref="Specifier"/> is not null.
        /// </summary>
        public bool hasSpecifier { get { return (Specifier != null); } }

        public PhraseCategory PhraseCategory
        {
            get { return Intermediate.PhraseCategory; }
        }

        #endregion

        #region Private Methods
        private void printTree(int indentLevel)
        {
            string diagonal = "{0}\\-{1}"; // Will produce a string like this "    \-NP" Note that the indents and the NP part are variable
            string straight = "{0}|-{1}";  // Will produce a string like this "    |-NP" Note that the indents and the NP part are variable

            // Print the (XP)
            p(diagonal, indentLevel, PhraseCategory.ToString());

            // Print the specifier's stuff if specifier is not null.
            if (hasSpecifier) Specifier.PrintTreeStructureToConsole(indentLevel+1);
            // Print the (X')
            p(diagonal, indentLevel + 1, Intermediate.ToString());
            // Print the (X')'s stuff
            p(diagonal, indentLevel + 2, Intermediate.Head.ToString());
            p(straight, indentLevel + 3, Intermediate.Head.Word);
            // Print the complement of X' if not null
            if (Intermediate.hasComplement) Intermediate.Complement.PrintTreeStructureToConsole(indentLevel + 2);
        }

        /// <summary>
        /// Quick console print function to help in printing trees.
        /// </summary>
        /// <param name="formatStr">The format string (use like <c>String.Format</c>)</param>
        /// <param name="indentLevel">The number of indents to put before the text</param>
        /// <param name="text">The information to print</param>
        private void p(string formatStr, int indentLevel, String text)
        {
            Console.WriteLine(String.Format(formatStr, new String(' ', indentLevel), text));
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Prints this Phrase Structure Tree to console.
        /// </summary>
        public void PrintTreeStructureToConsole(int level = 0)
        {
            printTree(level);
        }
        #endregion
    }
}
