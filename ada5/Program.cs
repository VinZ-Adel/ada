using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ada5
{
	internal class Program
	{
		static char[] alfabettett = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();
		static readonly char?[] Alfabetet = new char?[alfabettett.Length];
		static char?[] nyckel = new char?[alfabettett.Length];
		static readonly string text = "SVZPCZ DZ VOV QN HDLO ÖOVÅ ON? GÄ, KÄL KGDMAEF XVXXVÅF VEE XJÅÅV OFL VLVMJEÖQHV RVQHÖLFL. OFE KÄL ÄUHQN ÅGÄZOF SVZ VEE PVHEÖQHE AZÄÅZVRRFZV RVQHÖLFL EÖMM VEE ÅCZV MCQV AZÄXMFR. KÄL SVZ VMMEQN OFL PCZQEV AZÄÅZVRRFZVZFL DSFL ÄR RVQHÖLFL ÖLEF FBÖQEFZVOF ZFLE PJQÖQHE.\r\n\r\nKÄL EÖMMQHZÖSQ ÄUHQN OFL PCZQEV XTÅÅ-ZDEELÖLÅFL ON KÄL HÄZZÖÅFZVOF PFM QÄR XVXXVÅF KVOF ÅGÄZE. KÄL ÖLQNÅ ÄUHQN VEE AÄEFLEÖVMFL KÄQ FL VLVMJEÖQH RVQHÖL MNÅ MNLÅE XÄZEÄR ZFLE QÖPPFZETÅÅVLOF.\r\n\r\nVLEV, PCZ FEE CÅÄLXMÖUH, VEE OF PTLOVRFLEVMV ZFMVEÖÄLFZLV RFMMVL QAFUÖPÖHV MGTO Ö OFL SFEFLQHVAMÖÅV KVZRÄLÖL ÄUK RTQÖHQHVAVLOFE SVZ RÄEEVÅMÖÅV PCZ FEE QNOVLE TEEZJUH ÄUK QNOVLV PCZDLOZÖLÅVZ. Ö QN PVMM QHTMMF RVQHÖLFL HTLLV HÄRAÄLFZV ÖLSFUHMVOF ÄUK SFEFLQHVAMÖÅV RTQÖHQEJUHFL VS SÖMHFL HÄRAMFBÖEFE FMMFZ MDLÅO QÄR KFMQE. -VOV MÄSFMVUF, 1842";
		static readonly string pureText = RemovePunctuation(text);

		static readonly Regex word = new(@"\b[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]+\W");

		static void Main(string[] args)
        {
			for (int i = 0; i < alfabettett.Length; i++)
			{
				Alfabetet[i] = alfabettett[i];
			}
             Regex alfa = new(@"[ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ]");
             Regex num = new(@"\d");

			List<RuleContainer> actualRules = new();


			while (true)
            {
                List<RuleContainer> supposedRuleList = new();
				string? inp = Console.ReadLine();
				if (inp == null || inp == "")
					continue;
				else if (inp[0] == '/')
				{
					var sol = DecoderKey(nyckel, Alfabetet);
					var los = DecoderKey(Alfabetet, nyckel);
					switch (inp.ToLower())
					{
						case "/text":
							char[] textTemp = text.ToCharArray();
							for (int i = 0; i < textTemp.Length; i++)
							{
								if (sol.Keys.Contains(textTemp[i]))
								{
									Console.Write(sol[textTemp[i]]);
								}
								else
                                {
									bool isAlfabetisk = Alfabetet.Contains(textTemp[i]);

                                    if (isAlfabetisk)
										Console.BackgroundColor = ConsoleColor.DarkRed;
                                    Console.Write(textTemp[i]);
									if (isAlfabetisk)
										Console.BackgroundColor = ConsoleColor.Black;
                                }
							}
							Console.WriteLine();
							break;
						case "/decode":
							Console.Write("String to decode: ");
                            string uInput = Console.ReadLine() ?? "";
                            char[] encoded = uInput.ToUpper().ToCharArray();
							for (int i = 0; i < encoded.Length; i++)
							{
								if (sol.Keys.Contains(encoded[i]))
								{
									encoded[i] = sol[encoded[i]];
								}
							}
							Console.WriteLine(encoded);
							break;
						case "/encode":
							Console.Write("String to encode: ");
							string usrInput = Console.ReadLine() ?? "";
							char[] decoded = usrInput.ToUpper().ToCharArray();
							for (int i = 0; i < decoded.Length; i++)
							{
								if (los.Keys.Contains(decoded[i]))
								{
									decoded[i] = los[decoded[i]];
								}
							}
							Console.WriteLine(decoded);
							break;
					}
				}
				else
				{
					RegPhrase current = new(inp);
					MatchCollection matches = current.regex.Matches(pureText);

					for (int i = 0; i < matches.Count(); i++)
					{
						bool add = true;
						RuleContainer supposedRule = new(current.Phrase, matches[i].Value);

						// följande förhindrar dubbletter mellan suppRules och nyckel
						var q = Intersects(nyckel, supposedRule.Rule.Keys);
						if (q.Count != 0)
						{
							foreach (char c in q)
							{
								if (supposedRule.Rule[c] == Alfabetet[Array.IndexOf(nyckel, c)])
								{
									supposedRule.Rule.Remove(c);
								}
								else
									add = false;
							}
						}
						foreach (var f in supposedRule.Rule)
						{
							char? temmp = nyckel[Array.IndexOf(Alfabetet, f.Value)];
							if (temmp != null && temmp != f.Key)
								add = false;
						}
						if (add)
						{
							supposedRuleList.Add(supposedRule); //storing each word and a character-count- dictionary so that they can be processed later.
						}
					}

					List<RuleContainer> rules1 = new List<RuleContainer>();
					foreach (var _rule in supposedRuleList)
					{
						if (Enumerable.SequenceEqual(_rule.InputCharCount.Values, _rule.TextSideCharCount.Values) && !rules1.Any(c => c.TextSide == _rule.TextSide))
						{
							rules1.Add(_rule);
						}
					}
					if (rules1.Count == 0)
						Console.WriteLine("No match.");
					else
					{
						Console.WriteLine("Number of matches: " + rules1.Count);
						foreach (var _rule in rules1)
						{
							foreach (char c in _rule.Input)
							{
								Console.Write(c + " ");
							}
							Console.WriteLine();
							foreach (char c in _rule.TextSide)
							{
								if (nyckel.Contains(c))
									Console.BackgroundColor = ConsoleColor.DarkBlue;
								Console.Write(c);
								Console.BackgroundColor = ConsoleColor.Black;
								Console.Write(" ");
							}
							Console.WriteLine();

							if (rules1.Count != 1)
							{
								bool breakk = false;
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
										Console.WriteLine();
										breakk = true;
										break;
									}
									else if (input.KeyChar == 'n')
									{
										Console.WriteLine("\nRule skipped.");
										Console.WriteLine();
										break;
									}
								}
								if (breakk)
									break;
							}
							else
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
                                Console.WriteLine();
                            }
						}
						if (!nyckel.Any(c => c == null))
						{
							Console.WriteLine("Supposed solution reached:");
							foreach (char c in Alfabetet)
							{
								Console.Write(c + " ");
								Console.WriteLine();
							}
							foreach (char c in nyckel)
							{
								Console.Write(c + " ");
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
						else
						{
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
							Console.WriteLine();
						}
					}
				}
			}
		}

		public static void TextWrite(char[] text)
		{
			bool skip = false;
			foreach (char c in text)
			{
				if (skip)
				{
					skip = false;
				}
				else
				{
					if (c == '\\')
					{
						skip = true;
					}
					else if (!Char.IsWhiteSpace(c))
					{
						bool decrypted = (nyckel[Array.IndexOf(Alfabetet, c)] != null);
						if (decrypted)
							Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(c);
                        if (decrypted)
                            Console.BackgroundColor = ConsoleColor.Black;
                    }
					else
					{
						Console.Write(c);
					}
				}
			}
			Console.WriteLine();
		}


		public static Dictionary<char, char> DecoderKey(char?[] key, char?[] alphabet)
		{
			Dictionary<char, char> output = new();
			for (int i = 0; i < alphabet.Length; i++)
			{
				if (key[i] != null && alphabet[i] != null)
					output.Add((char)key[i], (char)alphabet[i]);
			}
			return output;
		}


		public static List<char> Intersects(char?[] arr, Dictionary<char,char>.KeyCollection keys)
		{
			List<char> result = new List<char>();
			foreach (char k in keys)
			{
				if (arr.Contains(k))
					result.Add(k);
			}
			return result;
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
