using System;

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

        private Intermediate _intermediate;

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
            get { return _intermediate; }
            set
            {
                if (value == null)
                    throw new Exception("Intermediate cannot be null.");
                else
                    _intermediate = value;
            } 
        }

        /// <summary>
        /// Returns true if <see cref="Specifier"/> is not null.
        /// </summary>
        public bool HasSpecifier => Specifier != null;

        public PhraseCategory PhraseCategory => Intermediate.PhraseCategory;

        #endregion

        #region Private Methods
        private void PrintTree(int indentLevel)
        {
            string diagonal = "{0}|-{1}"; // Will produce a string like this "    \-NP" Note that the indents and the NP part are variable
            string straight = "{0}|-{1}";  // Will produce a string like this "    |-NP" Note that the indents and the NP part are variable

            // Print the (XP)
            P(diagonal, indentLevel, PhraseCategory.ToString());

            // Print the specifier's stuff if specifier is not null.
            if (HasSpecifier) Specifier.PrintTree(indentLevel+1);
            // Print the (X')
            P(diagonal, indentLevel + 1, Intermediate.ToString());
            // Print the (X')'s stuff
            P(diagonal, indentLevel + 2, Intermediate.Head.ToString());
            P(straight, indentLevel + 3, Intermediate.Head.Word);
            // Print the complement of X' if not null
            if (Intermediate.HasComplement) Intermediate.Complement.PrintTree(indentLevel + 2);
        }

        /// <summary>
        /// Quick console print function to help in printing trees.
        /// </summary>
        /// <param name="formatStr">The format string (use like <c>String.Format</c>)</param>
        /// <param name="indentLevel">The number of indents to put before the text</param>
        /// <param name="text">The information to print</param>
        private static void P(string formatStr, int indentLevel, string text)
        {
            var message = string.Format(formatStr, new string('-', indentLevel), text);
            Console.WriteLine(message);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Prints this Phrase Structure Tree to console.
        /// </summary>
        public void PrintTreeStructureToConsole()
        {
            PrintTree(0);
        }
        #endregion
    }
}
