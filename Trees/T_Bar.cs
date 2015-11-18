namespace NLA_Tool.Trees
{
    class T_Bar : Intermediate
    {
        /// <summary>
        /// Creates a T' object. NOTE: Does not handle tense yet i.e. doesn't have a head.
        /// </summary>
        /// <param name="complement">The "tensed" part of the sentence</param>
        public T_Bar(Phrase complement) : base(null, complement) { }

        public T_Bar(Terminal head, Phrase complement) : base(head, complement) { }

        public override PhraseCategory PhraseCategory => PhraseCategory.IP;
    }
}