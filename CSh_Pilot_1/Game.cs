using System;
using System.Linq;

namespace CSh_Pilot_1
{
	class Game
	{
		static void Main(string[] args)
		{
			Console.WriteLine("===========  Игра в слова  ===========");
			Console.Write("Введите первоначальное слово: ");
			string primaryWord = EnterWord();

			Console.WriteLine("\nПравила:");
			Console.WriteLine($"\t1) кол-во символов не должно превышать {primaryWord.Length};");
			Console.WriteLine($"\t2) слова должны содержать буквы слова \"{primaryWord}\";");
			Console.WriteLine("\t3) у каждого игрока по 3 попытки.");

			Console.WriteLine("\n!!! Игра началась !!!");
			byte player = 1;
			bool stopGame = false;
			while (!stopGame)
			{
				Console.WriteLine($"\nИгрок {player}:");
				PlayerStep(ref primaryWord, out stopGame);
				if (!stopGame)
				{
					player++;
					if (player > 2) player = 1;
				}
			}

			if (player == 1) player = 2;
			else player = 1;

			Console.WriteLine($"\nИгра окончена.\nИгрок {player} победил!");
			Console.ReadKey();
		}

		static void PlayerStep(ref string primaryWord, out bool stopGame)
		{
			string newWord = "";
			stopGame = false;
			bool endStep = false;
			byte attempt = 1;
			while(!endStep)
			{
				Console.Write($"[{attempt}/3]: ");
				newWord = EnterWord(1, (byte)primaryWord.Length);
				if (AreDifferentChars(ref newWord, primaryWord))
				{
					if (attempt < 3)
					{
						Console.WriteLine("Почти! Попробуй ещё раз ^_^");
						endStep = false;
						attempt++;
					}
					else
					{
						endStep = true;
						stopGame = true;
					}
				}
				else
				{
					endStep = true;
				}
			}
		}

		static string EnterWord(byte minLength = 8, byte maxLength = 30)
		{
			string newWord = "";
			bool endReading = false;
			while (!endReading)
			{
				newWord = Console.ReadLine();
				if (newWord.Length < minLength || newWord.Length > maxLength)
				{
					Console.WriteLine("Неверная длина слова!");
				}
				else if (newWord.Any(char.IsDigit))
				{
					Console.WriteLine("Это не слово!");
				}
				else
				{
					endReading = true;
				}
			}
			return newWord;
		}

		static bool AreDifferentChars(ref string word, string primaryWord)
		{
			word.ToLower();
			primaryWord.ToLower();
			var differentСhars = from character in word
								 where !primaryWord.Contains(character)
								 select character;
			if (differentСhars.Count() > 0)
				return true;
			else
				return false;
		}
	}
}
