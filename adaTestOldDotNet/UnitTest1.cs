using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;

namespace adaTestOldDotNet
{
	[TestClass]
	public class UnitTest1
	{
		[TestClass]
		public sealed class ComputeHashEncodingGetString
		{
			static string input = "password1234";//"Blinka lilla stjärna…";
			static string expectedOutput = "BDC87B9C894DA5168059E00EBFFB9077";//"5DC6655675E6683CCA3938B5615ED766";

			[TestMethod]
			public void DefaultEncodingAndComputeHashToString()
			{
				byte[] inp = Encoding.Default.GetBytes(input);
				byte[] outp = new MD5CryptoServiceProvider().ComputeHash(inp);
				string output = Encoding.Default.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void ASCIIEncodingAndComputeHashToString()
			{
				byte[] inp = ASCIIEncoding.ASCII.GetBytes(input);
				byte[] outp = new MD5CryptoServiceProvider().ComputeHash(inp);
				string output = ASCIIEncoding.ASCII.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void BigEndianUnicodeEncodingAndComputeHashToString()
			{
				MD5 mD5 = MD5.Create();
				byte[] inp = Encoding.BigEndianUnicode.GetBytes(input);
				byte[] outp = mD5.ComputeHash(inp);
				string output = Encoding.BigEndianUnicode.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void UnicodeEncodingAndComputeHashToString()
			{
				MD5 mD5 = MD5.Create();
				byte[] inp = Encoding.Unicode.GetBytes(input);
				byte[] outp = mD5.ComputeHash(inp);
				string output = Encoding.Unicode.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void UTF32EncodingAndComputeHashToString()
			{
				MD5 mD5 = MD5.Create();
				byte[] inp = Encoding.UTF32.GetBytes(input);
				byte[] outp = mD5.ComputeHash(inp);
				string output = Encoding.UTF32.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void UTF7EncodingAndComputeHashToString()
			{
				MD5 mD5 = MD5.Create();
				byte[] inp = Encoding.UTF7.GetBytes(input);
				byte[] outp = mD5.ComputeHash(inp);
				string output = Encoding.UTF7.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}

			[TestMethod]
			public void UTF8EncodingAndComputeHashToString()
			{
				MD5 mD5 = MD5.Create();
				byte[] inp = Encoding.UTF8.GetBytes(input);
				byte[] outp = mD5.ComputeHash(inp);
				string output = Encoding.UTF8.GetString(outp);

				Assert.AreEqual(expectedOutput, output);
			}
		}
	}
}
