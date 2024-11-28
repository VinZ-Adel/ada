using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ada5
{
    public class RuleContainer
    {
        public string Input { get; set; }
        public string TextSide { get; set; }
        public Dictionary<char, int> InputCharCount { get; set; }
        public Dictionary<char, int> TextSideCharCount { get; set; }
        public Dictionary<char, char> Rule { get; set; }

        public RuleContainer(string input, string textSide)
        {
            Input = input;
            TextSide = textSide;
            InputCharCount = _charCount(Input);
            TextSideCharCount = _charCount(TextSide);

        }

        private Dictionary<char, int> _charCount(string input)
        {
            Dictionary<char, int> temp = new();
            foreach (char c in input)
            {
                temp.TryAdd(c, input.Count(o => o == c));
            }
            return temp;
        }

        private Dictionary<char, char> RuleMe(string input, string textside)
        {
            Dictionary<char, char> temp = new();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != textside[i])
                    temp.Add(textside[i], input[i]);
            }
            return temp;
        }
    }
}
