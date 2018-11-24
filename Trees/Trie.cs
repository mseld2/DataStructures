using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public class Trie
    {
        private readonly TrieNode _root;

        public Trie()
        {
            _root = new TrieNode('*');
        }

        public void Add(string word)
        {
            TrieNode node = _root;
            foreach(char c in word)
            {
                bool found = false;
                foreach(TrieNode child in node.Children)
                {
                    if (child.Char == c)
                    {
                        ++child.Counter;
                        node = child;
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    TrieNode newNode = new TrieNode(c);
                    node.Children.Add(newNode);
                    node = newNode;
                }
            }
            node.EndOfWord = true;
        }

        public bool Find(string str)
        {
            TrieNode node = _root;
            if(node.Children.Count == 0)
            {
                return false;
            }

            foreach(char c in str)
            {
                bool found = false;
                foreach(TrieNode child in node.Children)
                {
                    if(child.Char == c)
                    {
                        found = true;
                        node = child;
                        break;
                    }
                }

                if(!found)
                {
                    return false;
                }
            }

            return true;
        }

        class TrieNode
        {
            internal char Char { get; set; }
            internal List<TrieNode> Children { get; private set; }
            internal bool EndOfWord { get; set; }
            internal int Counter { get; set; }

            internal TrieNode(char c)
            {
                Char = c;
                Children = new List<TrieNode>();
                EndOfWord = false;
                Counter = 1;

            }
        }
    }
}
