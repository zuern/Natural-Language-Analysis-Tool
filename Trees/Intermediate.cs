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
        /// TEMPORARY FIX - Head should never be null, however I am doing this for now to enable me to use IP type phrases without specifying tense.
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
                PhraseCategory = PhraseCategory.IP; // TODO: Temporary hack to avoid dealing with tense markers in sentences.
            else
            {
                switch (Head.Category)
                {
                    case LexicalCategory.Noun:
                        PhraseCategory = PhraseCategory.NP;
                        break;
                    case LexicalCategory.Verb:
                        PhraseCategory = PhraseCategory.VP;
                        break;
                    case LexicalCategory.Adjective:
                        PhraseCategory = PhraseCategory.AP;
                        break;
                    case LexicalCategory.Adverb:
                        PhraseCategory = PhraseCategory.AdvP;
                        break;
                    case LexicalCategory.Preposition:
                        PhraseCategory = PhraseCategory.PP;
                        break;
                    case LexicalCategory.Determiner:
                        PhraseCategory = PhraseCategory.DP;
                        break;
                    case LexicalCategory.Auxilliary:
                        PhraseCategory = PhraseCategory.AP;
                        break;
                    case LexicalCategory.Pronoun:
                        PhraseCategory = PhraseCategory.NP;
                        break;
                    case LexicalCategory.Punctuation:
                        throw new Exception("Punctuation cannot be the head of an intermediate phrase.");
                    case LexicalCategory.Conjunction:
                        PhraseCategory = PhraseCategory.CP;
                        break;
                    case LexicalCategory.Tense:
                        PhraseCategory = PhraseCategory.IP;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
                case PhraseCategory.NP:
                    type = "N";
                    break;
                case PhraseCategory.VP:
                    type = "V";
                    break;
                case PhraseCategory.AP:
                    type = "A";
                    break;
                case PhraseCategory.AdvP:
                    type = "Adv";
                    break;
                case PhraseCategory.DP:
                    type = "D";
                    break;
                case PhraseCategory.PP:
                    type = "P";
                    break;
                case PhraseCategory.IP:
                    type = "I";
                    break;
                case PhraseCategory.CP:
                    type = "C";
                    break;
                default:
                    throw new Exception($"No way was found how to print the category for '{PhraseCategory}'");
            }
            return type + "'";
        }

        #endregion
    }
}
