using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLA_Tool.Trees
{
    class Phrase
    {
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
    }
}
