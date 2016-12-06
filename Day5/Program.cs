using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day5
{
	class Program
	{
		const string input = "reyedfim";
		const int PasswordLength = 8;
		static SortedList<int, char> pass = new SortedList<int, char>();
		private static object _listLock = new object();


		//swap this to true to run part 1 of the test
		static bool Part1 = false;

		static void Main(string[] args)
		{

			int i = 0;

			while (pass.Count() < PasswordLength)
			{
				Task task1 = Task.Factory.StartNew(() => testMD5(i));
				Task task2 = Task.Factory.StartNew(() => testMD5(i + 1));
				Task task3 = Task.Factory.StartNew(() => testMD5(i + 2));
				Task task4 = Task.Factory.StartNew(() => testMD5(i + 3));
				Task task5 = Task.Factory.StartNew(() => testMD5(i + 4));
				Task task6 = Task.Factory.StartNew(() => testMD5(i + 5));
				Task task7 = Task.Factory.StartNew(() => testMD5(i + 6));
				Task task8 = Task.Factory.StartNew(() => testMD5(i + 7));
				Task task9 = Task.Factory.StartNew(() => testMD5(i + 8));
				Task task10 = Task.Factory.StartNew(() => testMD5(i + 9));
				Task task11 = Task.Factory.StartNew(() => testMD5(i + 10));
				Task task12 = Task.Factory.StartNew(() => testMD5(i + 11));
				Task task13 = Task.Factory.StartNew(() => testMD5(i + 12));
				Task task14 = Task.Factory.StartNew(() => testMD5(i + 13));
				Task task15 = Task.Factory.StartNew(() => testMD5(i + 14));
				Task task16 = Task.Factory.StartNew(() => testMD5(i + 15));
				Task task17 = Task.Factory.StartNew(() => testMD5(i + 16));
				Task task18 = Task.Factory.StartNew(() => testMD5(i + 17));
				Task task19 = Task.Factory.StartNew(() => testMD5(i + 18));
				Task task20 = Task.Factory.StartNew(() => testMD5(i + 19));
				Task task21 = Task.Factory.StartNew(() => testMD5(i + 20));
				Task task22 = Task.Factory.StartNew(() => testMD5(i + 21));
				Task task23 = Task.Factory.StartNew(() => testMD5(i + 22));
				Task task24 = Task.Factory.StartNew(() => testMD5(i + 23));
				Task task25 = Task.Factory.StartNew(() => testMD5(i + 24));
				Task task26 = Task.Factory.StartNew(() => testMD5(i + 25));
				Task task27 = Task.Factory.StartNew(() => testMD5(i + 26));
				Task task28 = Task.Factory.StartNew(() => testMD5(i + 27));
				Task task29 = Task.Factory.StartNew(() => testMD5(i + 28));
				Task task30 = Task.Factory.StartNew(() => testMD5(i + 29));
				Task task31 = Task.Factory.StartNew(() => testMD5(i + 30));
				Task task32 = Task.Factory.StartNew(() => testMD5(i + 31));

				i += 32;
			}

			Console.Write("Password is: ");
			foreach (KeyValuePair<int, char> x in pass)
			{
				Console.Write(x.Value);
			}

			Console.WriteLine();
			Console.WriteLine("Happy Hacking Santa, press any key to exit...:");

			Console.ReadKey();

		}


		private static void testMD5(int testVal)
		{
			string testString = input + testVal;
			byte[] encodedTest = new UTF8Encoding().GetBytes(testString);
			byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedTest);

			string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

			if (encoded.Substring(0, 5).Equals("00000"))
			{
				char fifth = encoded.ElementAt(5);
				char valToAdd = ' ';
				int keyToAdd = int.MinValue; //if this gets added to the list, something went wrong (set very low so as to be the first item in the list, and thus more visible)
				bool write = true;
				int LocInPass = int.MaxValue; //See previous comment (set very high for the conditional a ways down)

				if (Part1)
				{
					keyToAdd = testVal;
					valToAdd = fifth;
				}
				else
				{
					write = int.TryParse(fifth.ToString(), out LocInPass);
					if (write && LocInPass < PasswordLength)
					{
						lock (_listLock) //I'm aware this is probably uneccessary, but I'd rather be safe than colliding
						{                //I'm also aware my nested if statements are a bit deep, without the lock we could make it shallower, and I'd rather only lock if neccessary.
							if (!pass.ContainsKey(LocInPass))
							{
								keyToAdd = LocInPass;
								valToAdd = encoded.ElementAt(6);
							}
							else
							{
								write = false;
							}
						}
					}
					else
					{
						write = false;
					}

				}

				if (write)
				{
					lock (_listLock)
					{
						pass.Add(keyToAdd, valToAdd);
						Console.Write("GOT ONE! Index: " + testVal + " Password Part: " + valToAdd);
						if (Part1)
						{
							Console.WriteLine();
						}
						else
						{
							Console.WriteLine(" Position in Password: " + LocInPass);
						}
					}
				}
			}
		}
	}
}
