using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLA_Tool.Trees
{
    /// <summary>
    /// All of the possible types of <see cref="Phrase"/>
    /// </summary>
    enum PhraseCategory
    {
        NP, VP, AP, AdvP, DP, PP, TP, CP
    }

    /// <summary>
    /// All of the supported lexical categories of words.
    /// </summary>
    enum LexicalCategory
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Determiner,
        Auxilliary,
        Pronoun,
        Punctuation
    }

}
