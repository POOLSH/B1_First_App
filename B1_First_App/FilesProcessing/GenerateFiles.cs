using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B1_First_App.Interfaces;

namespace B1_First_App.Generate
{
    public class GenerateFiles : IGenerateFiles
    {
        // Метод для генерации случайной строки заданной длины из указанных символов
        public string GenerateRandomString(Random random, int length, string symbs)
        {
            return new string(Enumerable.Repeat(symbs, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Метод для генерации случайной даты с 2019 года по текущую дату
        public string GenerateRandomDate(Random random)
        {
            DateTime startDate = new DateTime(2019, 1, 1);
            int range = (DateTime.Now - startDate).Days;
            return startDate.AddDays(random.Next(range)).ToString("dd.MM.yyyy");
        }

        // Метод для генерации файлов с случайными данными
        public void Generate(Random random)
        {
            // Генерация 100 файлов
            for (int i = 0; i < 100; i++)
            {
                string filePath = $"file_{i}.txt";

                // Запись случайных данных в файл
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int j = 0; j < 100000; j++)
                    {
                        // Генерация случайных данных для каждой строки
                        string randomDate = GenerateRandomDate(random);
                        string randomLatinSymb = GenerateRandomString(random, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
                        string randomRussianSymb = GenerateRandomString(random, 10, "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");
                        int randomEvenInt = random.Next(1, 50000000) * 2;

                        // Генерация случайного double и ограничение до 8 знаков после запятой
                        double randomDouble = Math.Round(random.NextDouble() * 20, 8);

                        // Формирование строки данных для записи в файл
                        string resultLine = $"{randomDate}||{randomLatinSymb}||{randomRussianSymb}||{randomEvenInt}||{randomDouble}\n";

                        // Запись строки в файл
                        writer.Write(resultLine);
                    }
                }
            }
            // Вывод сообщения об успешной генерации данных
            Console.WriteLine("Generated successfully");
        }

        
        
    }
}
