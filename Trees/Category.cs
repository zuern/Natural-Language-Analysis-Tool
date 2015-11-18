// ReSharper disable InconsistentNaming
namespace NLA_Tool.Trees
{
    /// <summary>
    /// All of the possible types of <see cref="Phrase"/>
    /// </summary>
    public enum PhraseCategory
    {
        NP, VP, AP, AdvP, DP, PP, IP, CP
    }

    /// <summary>
    /// All of the supported lexical categories of words.
    /// </summary>
    public enum LexicalCategory
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Determiner,
        Auxilliary,
        Conjunction,
        Pronoun,
        Punctuation,
        Tense
    }
}
