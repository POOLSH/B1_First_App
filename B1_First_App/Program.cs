using System;
using System.IO;

class Program
{
    static void Main()
    {
        Random random = new Random();

        for (int i = 0; i < 100; i++)
        {
            string filePath = $"file_{i}.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int j = 0; j < 100000; j++)
                {
                    string randomDate = GenerateRandomDate(random);
                    string randomLatinSymb = GenerateRandomString(random,10, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
                    string randomRussianSymb = GenerateRandomString(random, 10, "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");
                    int randomEvenInt = random.Next(1,50000000)*2;
                    double randomDouble = random.NextDouble()*20;
                    string resultLine = $"{randomDate}||{randomLatinSymb}||{randomRussianSymb}||{randomEvenInt}||{randomDouble}\n"; 
                    writer.Write(resultLine);
                }
            }
        }
        Console.WriteLine("Generated Successfully");
    }
    static string GenerateRandomString(Random random, int length, string symbs)
    {
        return new string(Enumerable.Repeat(symbs, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static string GenerateRandomDate(Random random)
    {
        DateTime startDate = new DateTime(2019,1,1);
        int range=(DateTime.Now-startDate).Days;
        return startDate.AddDays(random.Next(range)).ToString("dd.MM.yyyy");
    }
}

    