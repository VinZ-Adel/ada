namespace ada
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> fibo = [0,1];
			List<int> add = [];

			int i = 1;
			while(true)
			{
				int p = fibo[i] + fibo[i - 1];
				if (p < 2000000)
				{
					fibo.Add(p);
					Console.WriteLine(p);
					i++;
				}
				else
					break;
			}

			foreach (int o in fibo)
			{
				if (o % 2 == 0)
					add.Add(o);
			}

			long result = 0;
			foreach (int o in add)
				result += o;
			Console.WriteLine();
			Console.WriteLine(result);
			Console.ReadLine();
		}
	}
}
