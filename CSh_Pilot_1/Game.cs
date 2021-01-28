using System;
using System.Linq;

namespace CSh_Pilot_1
{
	class Game
	{
		const byte PrimaryWordMinLength = 8;
		const byte PrimaryWordMaxLength = 30;
		const byte StepAttempts = 3;
		const byte PlayersNumber = 2;

		static void Main(string[] args)
		{
			Console.WriteLine("===========  Игра в слова  ===========");
			Console.Write("Введите первоначальное слово: ");
			string primaryWord = EnterWord(PrimaryWordMinLength, PrimaryWordMaxLength);

			Console.WriteLine("\nПравила:");
			Console.WriteLine($"\t1) кол-во символов не должно превышать {primaryWord.Length};");
			Console.WriteLine($"\t2) слова должны содержать буквы слова \"{primaryWord}\";");
			Console.WriteLine($"\t3) у каждого игрока по {StepAttempts} попытки.");

			Console.WriteLine("\n!!! Игра началась !!!");

			for (byte i = 1; i <= PlayersNumber; i++)
			{
				Console.WriteLine($"\nИгрок {i}:");
				if (PlayerStep(primaryWord, StepAttempts))
				{
					if (++i > PlayersNumber)
						Console.WriteLine("\nИгра окончена.\nИгрок 1 победил!");
					else
						Console.WriteLine("\nИгра окончена.\nИгрок 2 победил!");
					break;
				}
				else
				{
					if (i == PlayersNumber) i = 0;
				}
			}

			Console.ReadKey();
		}

		static bool PlayerStep(string primaryWord, byte attemptsNumber)
		{
			bool stopGame = false;

			for (byte i = 1; i <= attemptsNumber; i++)
			{
				Console.Write($"[{i}/3]: ");
				string newWord = EnterWord(1, (byte)primaryWord.Length);
				if (AreDifferentChars(newWord, primaryWord))
				{
					if (i < attemptsNumber)
						Console.WriteLine("Почти! Попробуй ещё раз ^_^");
					else
					{
						stopGame = true;
						break;
					}
				}
				else
				{
					break;
				}
			}

			return stopGame;
		}

		static string EnterWord(byte minLength, byte maxLength)
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

		static bool AreDifferentChars(string word, string primaryWord) => word.Any(t => !primaryWord.Contains(t));
	}
}
