using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ada5
{
	public class RegPhrase
	{
		static readonly string alfaUpper = @"[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]";
		static readonly string alfaLower = @"[abcdefghijklmnopqrstuvwxyzåäö]";
		static readonly string alfaAny = @"[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö]";
		static readonly string alfaNot = @"[^ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖabcdefghijklmnopqrstuvwxyzåäö]";
		static readonly Regex regAlfaUpper = new(alfaUpper);
		static readonly Regex regAlfaLower = new(alfaLower);
		static readonly Regex regNotAlfa = new(alfaNot);
		static readonly Regex regNum = new(@"\d");
		public Regex regex { get; set; }
		public string Phrase { get; set; }
		public string[] words { get; set; }

		public RegPhrase(string phrase)
		{
			Phrase = phrase.ToUpper();
			regex = StringToRegexUpper(phrase);
			words = Regex.Matches(phrase, alfaUpper).Cast<Match>().Select(m => m.Value).ToArray();
		}


		static readonly Regex word = new(@"\b" + alfaUpper + @"+" + alfaNot);

		public static Regex StringToRegexUpper(string inputPhrase)
		{
			string temp = inputPhrase.ToUpper();
			MatchCollection matches = word.Matches(temp);

			string regg = @"";
			for (int i = 0; i < matches.Count(); i++)
			{
				regg += alfaUpper + @"{" + matches[i].Length + @"}";
				if (i != matches.Count() - 1)
					regg += @" ";
			}
			return new(regg);
		}
	}
}
