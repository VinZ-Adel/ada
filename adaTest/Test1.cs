using ada4;
using System.Security.Cryptography;
using System.Text;

namespace adaTest
{
	[TestClass]
	public sealed class ComputeHashToHextString
	{
		static string i1 = "password1234";
		static string e1 = "BDC87B9C894DA5168059E00EBFFB9077";

		static string i2 = "Blinka lilla stjärna...";
		static string e2 = "5DC6655675E6683CCA3938B5615ED766";

		static string input = i2;
		static string expectedOutput = e2;

		[TestMethod]
		public void DefaultEncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.Default.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void ASCIIEncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.ASCII.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void BigEndianUnicodeEncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.BigEndianUnicode.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void Latin1EncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.Latin1.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void UnicodeEncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.Unicode.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void UTF32EncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.UTF32.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void UTF7EncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.UTF7.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}

		[TestMethod]
		public void UTF8EncodingAndComputeHashToHexString()
		{
			MD5 mD5 = MD5.Create();
			byte[] inp = Encoding.UTF8.GetBytes(input);
			byte[] outp = mD5.ComputeHash(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}
	}

	[TestClass]
	public sealed class DefaultEncoding
	{
		[TestMethod]
		public void DefaultEncodingAndHashDataToHexString()
		{
			string input = "Blinka lilla stjärna…";
			string expectedOutput = "5DC6655675E6683CCA3938B5615ED766";

			byte[] inp = Encoding.Default.GetBytes(input);
			byte[] outp= MD5.HashData(inp);
			string output = Convert.ToHexString(outp);

			Assert.AreEqual(expectedOutput, output);
		}
	}
}
