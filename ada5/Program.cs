using System.Text.RegularExpressions;

namespace ada5
{
	internal class Program
	{
		static readonly char[] Alfabetet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();
		static char[] nyckel = new char[Alfabetet.Length];
		static readonly string text = "SVZPCZ DZ VOV QN HDLO ÖOVÅ ON? GÄ, KÄL KGDMAEF XVXXVÅF VEE XJÅÅV OFL VLVMJEÖQHV RVQHÖLFL. OFE KÄL ÄUHQN ÅGÄZOF SVZ VEE PVHEÖQHE AZÄÅZVRRFZV RVQHÖLFL EÖMM VEE ÅCZV MCQV AZÄXMFR. KÄL SVZ VMMEQN OFL PCZQEV AZÄÅZVRRFZVZFL DSFL ÄR RVQHÖLFL ÖLEF FBÖQEFZVOF ZFLE PJQÖQHE.\r\n\r\nKÄL EÖMMQHZÖSQ ÄUHQN OFL PCZQEV XTÅÅ-ZDEELÖLÅFL ON KÄL HÄZZÖÅFZVOF PFM QÄR XVXXVÅF KVOF ÅGÄZE. KÄL ÖLQNÅ ÄUHQN VEE AÄEFLEÖVMFL KÄQ FL VLVMJEÖQH RVQHÖL MNÅ MNLÅE XÄZEÄR ZFLE QÖPPFZETÅÅVLOF.\r\n\r\nVLEV, PCZ FEE CÅÄLXMÖUH, VEE OF PTLOVRFLEVMV ZFMVEÖÄLFZLV RFMMVL QAFUÖPÖHV MGTO Ö OFL SFEFLQHVAMÖÅV KVZRÄLÖL ÄUK RTQÖHQHVAVLOFE SVZ RÄEEVÅMÖÅV PCZ FEE QNOVLE TEEZJUH ÄUK QNOVLV PCZDLOZÖLÅVZ. Ö QN PVMM QHTMMF RVQHÖLFL HTLLV HÄRAÄLFZV ÖLSFUHMVOF ÄUK SFEFLQHVAMÖÅV RTQÖHQEJUHFL VS SÖMHFL HÄRAMFBÖEFE FMMFZ MDLÅO QÄR KFMQE. -VOV MÄSFMVUF, 1842";
		static readonly string pureText = RemovePunctuation(text);
		static readonly Regex alfa = new(@"[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]");
		static readonly Regex notAlfa = new("[^ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]");
		static readonly Regex num = new(@"\d");

		static readonly Regex word = new(@"\b[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]+\W");

		static void Main(string[] args)
		{
			List<Dictionary<char, int>> TextSideList = new List<Dictionary<char, int>>();
			List<Dictionary<char, int>> InputSideList = new List<Dictionary<char, int>>();

			RegPhrase current = new(Console.ReadLine() ?? "");
			MatchCollection matches = current.regex.Matches(pureText);
			for (int i = 0; i < matches.Count(); i++)
			{
				string m = matches[i].ToString();
				Dictionary<char, int> TextSide = new();
				Dictionary<char, int> InputSide = new();
				foreach (char c in m)
				{
					TextSide.TryAdd(c, m.Count(o => o == c));
				}
				string n = current.words[i];
				foreach (char v in n)
				{
					InputSide.TryAdd(v, n.Count(p => p == v));
				}
				TextSideList.Add(TextSide);
				InputSideList.Add(InputSide); //storing each word as a dictionary so that they can be processed later.
			}
		}
		// I will try to convert user input to a regex, then I will search for matches in pureText, and compare the number of occurrences of each letter to see if it is a possible match.
		// Maybe I can get it to remember the position index of each letter to see if the letters match, not just the numbers...
		// If there is more than one possible match, it should say that and let the user choose to guess on one or skip.
		// It should also check if any of the stored key letters are in the regex match, and display those to the user, maybe underneath with formatting to see the missing letters.
		

		public static string RemovePunctuation(string text)
		{
			string[] temp = text.Split(" ");
			foreach (string _word in temp)
			{
				notAlfa.Replace(_word, "");
			}
			string output = String.Join(" ", temp.Where(s => !String.IsNullOrEmpty(s)));
			return output;
		}
	}
}
