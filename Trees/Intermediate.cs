using System;

namespace NLA_Tool.Trees
{
    /// <summary>
    /// Represents the intermediate (X') node of a phrase structure.
    /// </summary>
    public class Intermediate
    {
        #region Public Properties
        /// <summary>
        /// Required - The head of the intermediate phrase
        /// </summary>
        public Terminal Head { get; set; }

        /// <summary>
        /// Optional - A nested phrase
        /// </summary>
        public Phrase Complement { get; set; }

        /// <summary>
        /// True if <see cref="Complement"/> is not null.
        /// </summary>
        public bool HasComplement => Complement != null;

        /// <summary>
        /// TEMPORARY FIX - Head should never be null, however I am doing this for now to enable me to use TP type phrases without specifying tense.
        /// </summary>
        public bool HasHead => Head != null;

        public virtual PhraseCategory PhraseCategory { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of Intermediate
        /// </summary>
        /// <param name="head">The head of the intermediate phrase</param>
        public Intermediate(Terminal head)
        {
            Head = head;
            SetPhraseCategory();
        }

        /// <summary>
        /// Creates a new instance of Intermediate
        /// </summary>
        /// <param name="head">The head of the intermediate phrase</param>
        /// <param name="complement">The complement phrase to attach</param>
        public Intermediate(Terminal head, Phrase complement)
            : this(head)  // Constructor chaining see http://stackoverflow.com/questions/829870/calling-constructor-from-other-constructor-in-same-class
        {
            Complement = complement;
        } 
        #endregion

        #region Private Methods

        /// <summary>
        /// Figures out what the value of X' should be based on the lexical category of the head.
        /// <remarks>Note: <c>Head</c> must be initialized before using this method</remarks>
        /// </summary>
        /// <returns>The appropriate <c>PhraseCategory</c> of the Intermediate Phrase</returns>
        private void SetPhraseCategory()
        {
            if (Head == null)
                PhraseCategory = PhraseCategory.Tp; // TODO: Temporary hack to avoid dealing with tense markers in sentences.
            else
            {
                switch (Head.Category)
                {
                    case LexicalCategory.Noun:
                        PhraseCategory = PhraseCategory.Np;
                        break;
                    case LexicalCategory.Verb:
                        PhraseCategory = PhraseCategory.Vp;
                        break;
                    case LexicalCategory.Adjective:
                        PhraseCategory = PhraseCategory.Ap;
                        break;
                    case LexicalCategory.Adverb:
                        PhraseCategory = PhraseCategory.AdvP;
                        break;
                    case LexicalCategory.Preposition:
                        PhraseCategory = PhraseCategory.Pp;
                        break;
                    case LexicalCategory.Determiner:
                        PhraseCategory = PhraseCategory.Dp;
                        break;
                    case LexicalCategory.Auxilliary:
                        PhraseCategory = PhraseCategory.Ap;
                        break;
                    case LexicalCategory.Pronoun:
                        PhraseCategory = PhraseCategory.Np;
                        break;
                    case LexicalCategory.Punctuation:
                        throw new Exception("Punctuation cannot be the head of an intermediate phrase.");
                }
            }
        }

        /// <summary>
        /// Prints the Category of (X') that this is. Example, if the Head is a noun, returns (N') without the parenthesis.
        /// </summary>
        public override string ToString()
        {
            string type;
            switch (PhraseCategory)
            {
                case PhraseCategory.Np:
                    type = "N";
                    break;
                case PhraseCategory.Vp:
                    type = "V";
                    break;
                case PhraseCategory.Ap:
                    type = "A";
                    break;
                case PhraseCategory.AdvP:
                    type = "Adv";
                    break;
                case PhraseCategory.Dp:
                    type = "D";
                    break;
                case PhraseCategory.Pp:
                    type = "P";
                    break;
                case PhraseCategory.Tp:
                    type = "T";
                    break;
                case PhraseCategory.Cp:
                    type = "C";
                    break;
                default:
                    type = "SOMETHING WENT HORRIBLY WRONG";
                    break;
            }
            return type + "'";
        }

        #endregion
    }
}
