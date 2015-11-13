namespace NLA_Tool.Trees
{
    class T_Bar : Intermediate
    {
        /// <summary>
        /// Creates a T' object. NOTE: Does not handle tense yet i.e. doesn't have a head.
        /// </summary>
        /// <param name="Complement">The "tensed" part of the sentence</param>
        public T_Bar(Phrase Complement) : base(null, Complement) { }

        public override PhraseCategory PhraseCategory { get { return PhraseCategory.TP; } }
    }
}