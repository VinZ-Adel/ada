using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ada3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(Kaprekar(1917) + "\t");
			Console.WriteLine(Kaprekar(7430) + "\t");
			Console.WriteLine(Kaprekar(8833) + "\t");
			Console.WriteLine(Kaprekar(9998) + "\t");
			Console.ReadLine();
		}

		static int Kaprekar(int input)
		{
			if (input.ToString().Count() != 4)
			{
				Console.WriteLine("Talet måste vara endast 4 siffror.");
				return -1;
			}

			int[] inp = Int2IntArr(input);


			Console.Write("Count: ");
			int count = 1;
			while (true)
			{
				if (inp.Count() < 4)
				{

					List<int> list = [..inp];
					while (list.Count() < 4)
						list.Add(0);
					inp = list.ToArray();
				}

				Console.Write(count);
				Console.CursorLeft = Console.CursorLeft - count.ToString().Length;
				int newint = KapIter(inp);
				if (newint == IntArr2Int(inp))
				{
					return count + 1;
				}
				else
				{
					inp = Int2IntArr(newint);
				}
				count++;
			}
		}

		static int KapIter(int[] fourDigitNum)
		{
			int big = IntArr2Int(SortDesc(fourDigitNum));
			int lil = IntArr2Int(SortAsc(fourDigitNum));

			int result = big - lil;
			return result;
		}

		static int IntArr2Int(int[] arr)
		{
			List<char> list = new();
			foreach (int i in arr)
				list.Add((char)(i + 48));
			string outp = new(list.ToArray());
			int result = Convert.ToInt32(outp);
			return result;
		}

		static int[] Int2IntArr(int input)
		{
			string temp = input.ToString();
			char[] chars = temp.ToCharArray();
			int[] inp = new int[chars.Length];

			try
			{
				for (int i = 0; i < chars.Length; i++)
				{
					inp[i] = (int)Char.GetNumericValue(chars[i]);
				}
			}
			catch { Console.WriteLine("Error converting to int[]"); }
			return inp;
		}

		static int[] SortAsc(int[] arr) //copypasta, modifierad
		{
			int[] data = new List<int>(arr).ToArray();
			bool needsSorting = true;
			//Gör en loop för varje tal som skall sorteras, avbryt om talen är sorterade
			for (int i = 0; i < data.Length - 1 && needsSorting; i++)
			{
				//Signalera att vi börjar om en ny vända med sortering
				needsSorting = false;
				//Gå igenom alla tal fram till och med de tal som ev. 
				//redan är sorterade (variabeln i)
				for (int j = 0; j < data.Length - 1 - i; j++)
				{
					//Flytta större tal fram i vektorn
					if (data[j] > data[j + 1])
					{
						//Signalera att vi kommer att behöva fortsätta sortera
						needsSorting = true;
						//Byt plats på tal
						int tmp = data[j + 1];
						data[j + 1] = data[j];
						data[j] = tmp;
					}
				}
				//Har vi nu inte behövt sortera några tal så är 
				//needsSorting == false och loop'en kommer att avbrytas
			}
			return data;
		}

		static int[] SortDesc(int[] arr)
		{
			int[] data = new List<int>(arr).ToArray();
			bool needsSorting = true;
			//Gör en loop för varje tal som skall sorteras, avbryt om talen är sorterade
			for (int i = 0; i < data.Length - 1 && needsSorting; i++)
			{
				//Signalera att vi börjar om en ny vända med sortering
				needsSorting = false;
				//Gå igenom alla tal fram till och med de tal som ev. 
				//redan är sorterade (variabeln i)
				for (int j = 0; j < data.Length - 1 - i; j++)
				{
					//Flytta större tal fram i vektorn
					if (data[j] < data[j + 1])
					{
						//Signalera att vi kommer att behöva fortsätta sortera
						needsSorting = true;
						//Byt plats på tal
						int tmp = data[j + 1];
						data[j + 1] = data[j];
						data[j] = tmp;
					}
				}
				//Har vi nu inte behövt sortera några tal så är 
				//needsSorting == false och loop'en kommer att avbrytas
			}
			return data;
		}
	}
}
