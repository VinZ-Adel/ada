using System.Text.RegularExpressions;

namespace ada5
{
	internal class Program
	{
		static readonly char[] Alfabetet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();
		static char?[] nyckel = new char?[Alfabetet.Length];
		static readonly string text = "SVZPCZ DZ VOV QN HDLO ÖOVÅ ON? GÄ, KÄL KGDMAEF XVXXVÅF VEE XJÅÅV OFL VLVMJEÖQHV RVQHÖLFL. OFE KÄL ÄUHQN ÅGÄZOF SVZ VEE PVHEÖQHE AZÄÅZVRRFZV RVQHÖLFL EÖMM VEE ÅCZV MCQV AZÄXMFR. KÄL SVZ VMMEQN OFL PCZQEV AZÄÅZVRRFZVZFL DSFL ÄR RVQHÖLFL ÖLEF FBÖQEFZVOF ZFLE PJQÖQHE.\r\n\r\nKÄL EÖMMQHZÖSQ ÄUHQN OFL PCZQEV XTÅÅ-ZDEELÖLÅFL ON KÄL HÄZZÖÅFZVOF PFM QÄR XVXXVÅF KVOF ÅGÄZE. KÄL ÖLQNÅ ÄUHQN VEE AÄEFLEÖVMFL KÄQ FL VLVMJEÖQH RVQHÖL MNÅ MNLÅE XÄZEÄR ZFLE QÖPPFZETÅÅVLOF.\r\n\r\nVLEV, PCZ FEE CÅÄLXMÖUH, VEE OF PTLOVRFLEVMV ZFMVEÖÄLFZLV RFMMVL QAFUÖPÖHV MGTO Ö OFL SFEFLQHVAMÖÅV KVZRÄLÖL ÄUK RTQÖHQHVAVLOFE SVZ RÄEEVÅMÖÅV PCZ FEE QNOVLE TEEZJUH ÄUK QNOVLV PCZDLOZÖLÅVZ. Ö QN PVMM QHTMMF RVQHÖLFL HTLLV HÄRAÄLFZV ÖLSFUHMVOF ÄUK SFEFLQHVAMÖÅV RTQÖHQEJUHFL VS SÖMHFL HÄRAMFBÖEFE FMMFZ MDLÅO QÄR KFMQE. -VOV MÄSFMVUF, 1842";
		static readonly string pureText = RemovePunctuation(text);

		static readonly Regex word = new(@"\b[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]+\W");

		static void Main(string[] args)
        {
             Regex alfa = new(@"[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]");
             Regex num = new(@"\d");

			List<RuleContainer> actualRules = new();


			while (true)
            {
                List<RuleContainer> supposedRuleList = new();
				string? inp = Console.ReadLine();
				if (inp == null || inp == "")
					continue;
				else
				{
					RegPhrase current = new(inp);
					MatchCollection matches = current.regex.Matches(pureText);

					for (int i = 0; i < matches.Count(); i++)
					{
						RuleContainer supposedRule = new(current.Phrase, matches[i].Value);
						// lägg till något som förhindrar dubbletter mellan suppRules och nyckel
						if()
							supposedRuleList.Add(supposedRule); //storing each word and a character-count- dictionary so that they can be processed later.
					}
					foreach (var _rule in supposedRuleList)
					{
						if (Enumerable.SequenceEqual(_rule.InputCharCount.Values, _rule.TextSideCharCount.Values))
						{
							foreach (char c in _rule.Input)
							{
								Console.Write(c + "\t");
							}
							Console.WriteLine();
							foreach (char c in _rule.TextSide)
							{
								Console.Write(c + "\t");
							}
							Console.WriteLine();
							//ask user whether to add rule according, store rules in a list or something and allow the user to delete rules if needed (like if user chooses or there is a discrepancy in the rules)
							Console.WriteLine("Add this rule to key? (y/n)");
							while (true)
							{
								var input = Console.ReadKey();
								if (input.KeyChar == 'y')
								{
									actualRules.Add(_rule);
									for (int i = 0; i < _rule.Input.Count(); i++)
									{
										try
										{
											nyckel[Array.IndexOf(Alfabetet, _rule.Input[i])] = _rule.TextSide[i];
										}
										catch { }
									}
									Console.WriteLine("\nRule added.");
									break;
								}
								else if (input.KeyChar == 'n')
								{
									Console.WriteLine("\nRule skipped.");
									break;
								}
							}
							Console.WriteLine("Current progress:");
							for (int i = 0; i < 2; i++)
							{
								if (i == 0)
								{
                                    for (int j = 0; j < Alfabetet.Count(); j++)
                                    {
										Console.Write(Alfabetet[j]);
                                    }
                                }
								else
                                {
                                    for (int j = 0; j < nyckel.Count(); j++)
                                    {
										if (nyckel[j] != null)
											Console.Write(nyckel[j]);
										else
											Console.Write("_");
                                    }
                                }
								Console.WriteLine();
                            }
						}

					}
					if (!nyckel.Any(c => c == null))
					{
						Console.WriteLine("Supposed solution reached:");
						foreach (char c in Alfabetet)
						{
							Console.Write(c + "\t");
							Console.WriteLine();
						}
						foreach (char c in nyckel)
						{
							Console.Write(c + "\t");
							Console.WriteLine();
						}
						Console.WriteLine("Which makes the text:");
						string solution = text;
						foreach (char c in text)
						{
							if (nyckel.Contains(c))
							{
								Regex.Replace(solution, c.ToString(), Alfabetet[Array.IndexOf(nyckel, c)].ToString());
							}
						}
					}
				}
			}
		}
		// I will try to convert user input to a regex, then I will search for matches in pureText, and compare the number of occurrences of each letter to see if it is a possible match.
		// Maybe I can get it to remember the position index of each letter to see if the letters match, not just the numbers...
		// If there is more than one possible match, it should say that and let the user choose to guess on one or skip.
		// It should also check if any of the stored key letters are in the regex match, and display those to the user, maybe underneath with formatting to see the missing letters.
		

		public static string RemovePunctuation(string text)
        {
            Regex notAlfa = new("[^ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]");
            string[] temp = text.Split(" ");
			for (int i = 0; i < temp.Count(); i++)
			{
				string tempp = temp[i];
				notAlfa.Replace(tempp, "");
				temp[i] = tempp;
			}
			string output = String.Join(" ", temp.Where(s => !String.IsNullOrEmpty(s)));
			return output;
		}
	}
}
