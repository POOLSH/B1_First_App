using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B1_First_App.Interfaces;

namespace B1_First_App.FilesProcessing
{
    public class MergingFiles : IMergingFiles
    {
        // Метод для объединения файлов
        public void Merge()
        {
            // Имя файла, в который будут объединены данные из других файлов
            string outputFileName = "merged.txt"; 

            Console.WriteLine("Select an option");
            Console.WriteLine("1. Merge files without deleting lines.");
            Console.WriteLine("2. Merge files and delete string with a specific substring");

            // Считывание выбора пользователя
            int option = int.Parse(Console.ReadLine());
            // Подстрока для удаления строк
            string subString = ""; 

            // Если выбрана опция 2, пользователь вводит подстроку для удаления
            if (option == 2)
            {
                Console.WriteLine("Enter a substring to delete");
                subString = Console.ReadLine();
            }

            // Счетчик удаленных строк
            int deletedCount = 0; 

            // Запись объединенных данных в выходной файл
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                for (int i = 0; i < 100; i++) 
                {
                    // Имя текущего файла
                    string fileName = $"file_{i}.txt"; 
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        // Чтение строки из текущего файла
                        while ((line = reader.ReadLine()) != null) 
                        {
                            // Если выбрана опция 2 и строка содержит подстроку
                            if (option == 2 && line.Contains(subString)) 
                            {
                                deletedCount++; 
                            }
                            else
                            {
                                writer.WriteLine(line); 
                            }
                        }
                    }
                }
            }

            // Вывод информации о завершении объединения файлов
            if (option == 2)
            {
                Console.WriteLine($"File merging is completed. {deletedCount} lines have been deleted");
            }
            else
            {
                Console.WriteLine("File merging is completed.");
            }
        }
    }
}
