using System;

namespace NLA_Tool.Exceptions
{
    /// <summary>
    /// This error is thrown when a word was not found.
    /// </summary>
    class WordNotFoundException : Exception
    {
        public string Word { get; private set; }

        /// <summary>
        /// Create a new <c>WordNotFoundException</c> object
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="word">The word that was not found</param>
        public WordNotFoundException(string message, string word) : base(message)
        {
            Word = word;
        }
    }
}
