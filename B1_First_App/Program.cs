using System;
using System.IO;
using B1_First_App.FilesProcessing;
using B1_First_App.Generate;

class Program
{
    static void Main()
    {
        GenerateFiles generateFiles = new GenerateFiles();
        MergingFiles mergingFiles = new MergingFiles();
        TxtToDB txtToDB = new TxtToDB();
        Random random = new Random();
        while (true)
        {
            Console.WriteLine("Select:");
            Console.WriteLine("1. Generate files");
            Console.WriteLine("2. Merge files");
            Console.WriteLine("3. Import files to DB");
            Console.WriteLine("4. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    generateFiles.Generate(random);
                    break;
                case "2":
                    mergingFiles.Merge();
                    break;
                case "3":
                    txtToDB.Transaction();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
   
}

    