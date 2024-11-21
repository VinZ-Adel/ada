using System.Security.Cryptography;
using System.Text;

namespace ada4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			MD5 mD5 = MD5.Create();
			char[] code = ['a', 'a', 'a', 'a', 'a'];
			bool cont = true;
			bool slow = false;
			while (cont)
			{
				string codeNum = string.Join("",code);
				string usrIn = "AdaLovelace-" + codeNum;
				Console.Clear();
				Console.WriteLine(usrIn);
				if (slow)
				{
					Thread.Sleep(400);
				}
				string result = HashMe(usrIn);
				if (result == "0CF2FCF4598769F395B7CC0528C09C0E")
				{
					Console.WriteLine(usrIn + " Is correct!");
					break;
				}
				else
				{
					bool iterate = true;
					for (int i = 0; i < code.Length; i++) //checks for 9s and and 
					{
						if (code[i] == 'z')
						{
							if (code.All(c => c == 'z'))
							{
								cont = false;
								Console.WriteLine();
								Console.WriteLine("Hash not found.");
								break;
							}
							else if (i != code.Length - 1)
							{
								if (!code.Skip(i).All(c => c == 'z'))
								{
									code[i] = 'a';
									code[i + 1]++;
									iterate = false;
								}
							}
						}
					}
					if (iterate)
						code[0]++;
				}
			}
			Console.ReadLine();
		}

		static string HashMe(string input)
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.UTF8.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);
			return output;
		}

		static char[] IntArr2CharArr(int[] arr)
		{
			List<char> list = new();
			foreach (int i in arr)
				list.Add((char)(i + 48));
			char[] outp = list.ToArray();
			return outp;
		}

		static double PercentileMatch(string s1, string s2)
		{
			if (s1.Length != s2.Length)
				Console.WriteLine("Caution: strings are of different length.");
			int length = s1.Length < s2.Length ? s1.Length : s2.Length;
			char[] ss1 = s1.ToCharArray();
			char[] ss2 = s2.ToCharArray();
			int matchesCount = 0;
			for (int i = 0; i < length; i++)
			{
				if (ss1[i] == ss2[i])
					matchesCount++;
			}
			double result = matchesCount / length;
			return result * 100;
		}
	}
}
