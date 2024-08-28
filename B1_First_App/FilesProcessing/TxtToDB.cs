using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B1_First_App.Interfaces;
using Npgsql;

namespace B1_First_App.FilesProcessing
{
    public class TxtToDB:ITxtToDB
    {
        public void Transaction()
        {
            // Строка подключения к базе данных
            string connectionString = "Host=127.0.0.1;Port=5432;Database=B1;Username=postgres;Password=123;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                // Установление соединения с базой данных
                connection.Open();
                // Общее количество импортированных строк
                int totalImportedCount = 0;
                // Транзакция для обеспечения атомарности операций
                NpgsqlTransaction transaction = null;

                // Цикл по всем файлам для импорта данных
                for (int i = 0; i < 100; i++) 
                {
                    string filePath = $"file_{i}.txt"; 
                    int importedCount = 0; 

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Начало транзакции
                        transaction = connection.BeginTransaction(); 

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            // Разделение строки на значения
                            string[] values = line.Split(new string[] { "||" }, StringSplitOptions.None); 

                            // SQL запрос для вставки данных
                            string sql = "INSERT INTO Random (RDate, RLatinSymb, RRussianSymb, RInteger, RDouble) " +
                                        "VALUES (@Date, @Latin, @Russian, @Integer, @Double)";

                            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection, transaction))
                            {
                                // Установка параметров для SQL запроса
                                command.Parameters.AddWithValue("@Date", values[0]);
                                command.Parameters.AddWithValue("@Latin", values[1]);
                                command.Parameters.AddWithValue("@Russian", values[2]);
                                command.Parameters.AddWithValue("@Integer", int.Parse(values[3]));
                                command.Parameters.AddWithValue("@Double", double.Parse(values[4]));

                                // Выполнение команды вставки
                                command.ExecuteNonQuery(); 
                                importedCount++;

                                // Коммит после каждых 1000 строк
                                if (importedCount % 1000 == 0) 
                                {
                                    // Фиксация изменений
                                    transaction.Commit();
                                    // Начало новой транзакции
                                    transaction = connection.BeginTransaction(); 

                                    // Вывод информации о ходе процесса импорта
                                    Console.WriteLine($"Imported {importedCount} rows. Remaining rows: {reader.BaseStream.Length - reader.BaseStream.Position}");
                                }
                            }
                        }

                        transaction.Commit(); // Финальный коммит для текущего файла
                        totalImportedCount += importedCount; // Обновление общего количества импортированных строк

                        // Вывод информации об импорте текущего файла
                        Console.WriteLine($"File {filePath} import completed. Total imported rows: {totalImportedCount}");
                    }
                }
                Console.WriteLine($"Import process completed. Total imported rows: {totalImportedCount}.");
            }
        }
    }
}
