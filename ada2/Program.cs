namespace ada2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int sum = 950;

			Console.WriteLine(Check(sum));
			Console.ReadLine();
		}

		static long Check(int sum)
		{
			int a;
			int b;
			int c;


			for (int i = 1; i < sum; i++)
			{
				a = i;
				for (int j = a; j < sum; j++)
				{
					b = j;
					c = sum - (a + b);
					Console.WriteLine();
					Console.WriteLine(a + "  " + b + "  " + c);
					Console.Write("Triple:\t\t");
					if (a < b && b < c)
						Console.WriteLine("Yes");
					else { Console.WriteLine("No"); }
					Console.Write("Pythagorian:\t");
					if (Pythagorian(a, b, c))
					{
						Console.WriteLine("Yes");
						return a * b * c;
					}
					else { Console.WriteLine("No"); }
				}
			}
			return -1;
		}

		static bool Pythagorian(int a, int b, int c)
		{
			if (a*a + b*b == c*c)
				return true;
			else
				return false;
		}
	}
}
