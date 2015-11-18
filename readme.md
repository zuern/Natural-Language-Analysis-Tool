#Natural Language Analysis Tool
A phrase analysis tool that can recursively generate phrase structure trees for English phrases using the [X' schema](https://en.wikipedia.org/wiki/X-bar_theory).
##About
This tool is being developed to explore Natural Language and it's hierarchical structure. Drawing on computational linguistics, syntax, and computer science, this tool will take any English phrase and try to generate a phrase structure tree to represent that phrase using a recursive algorithm.
###Example
Input Phrase: `John went to the store.`
Output:
```
\-TP
 \-NP
  \-N'
   \-N
    |-John
 \-T'
  \-T
   |-{Past Tense}
  \-VP
   \-V'
    \-V
     |-went {+Pst}
    \-PP
     \-P'
      \-P
       |-to
      \-NP
       \-DP
        \-D'
         \-det
          |-the
       \-N'
        \-N
         |-store
```
*Note: as of now, identifying phrase tense is not yet implemented however is a planned feature. At the moment, it simply displays a placeholder where the {Past Tense} node is.*
#How it Works
The program works as follows:

1. The phrase is split into an array of words
2. Each word is analysed to determine it's lexical category (noun, verb, adjective, etc.) Currently this process is done via looking up the category from a dictionary, then online via [Merriam-Webster Inc.](http://merriam-webster.com/) if no definition is found locally. In future revisions contextual analysis will be added to determine the category of words based on their neighbours (this is because some words can be nouns or verbs, etc. depending on the context in which they are used).
3. Beginning at the right-most word the words are combined together two at a time into small trees - with each word to the left being added into the tree as a parent node or specifier - one at a time. This process continues until the algorithm reaches the beginning of the phrase. 
