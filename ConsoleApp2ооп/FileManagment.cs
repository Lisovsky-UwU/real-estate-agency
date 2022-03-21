using System;
using System.IO;

namespace ConsoleApp2ооп
{
    /// <summary>Класс по работе с сохранением и чтением файлов</summary>
    class FileManagment
    {
        /// <summary>Метод пробующий найти и открыть файл с данными</summary>
        /// <param name="fileName">Название файла с данными</param>
        /// <param name="apartaments">Список квартир для чтения</param>
        /// <param name="clientList">Список клиентов для чтения</param>
        public static void TryOpenFile(string fileName, ApartamentList apartaments, ClientList clientList)
        {
            if (File.Exists(fileName) == true)
            {
                ConsoleKey k;
                Console.WriteLine("Обнаружен файл с данными. Желаете его открыть?\n(y - да, n - нет)");
                do
                {
                    k = Console.ReadKey(true).Key;
                } while (k != ConsoleKey.Y && k != ConsoleKey.N);
                if (k == ConsoleKey.Y)
                {
                    Console.WriteLine("Производится чтение из файла...");
                    clientList.ReadFromFile(fileName);
                    apartaments.ReadFromFile(fileName, clientList.clientList);
                    Console.WriteLine("Чтение выполнено успешно");
                    Console.ReadKey(true);
                }
            }
        }
        /// <summary>Метод сохранения данных в файл</summary>
        /// <param name="fileName">Имя файла для сохранения</param>
        /// <param name="apartaments">Список квартир</param>
        /// <param name="clientList">Список клиентов</param>
        public static void AskToSave(string fileName, ApartamentList apartaments, ClientList clientList)
        {
            ConsoleKey k;
            Console.Clear();
            Console.WriteLine("Желаете сохранить данные в файл?\n(y - да, n - нет)");
            do
            {
                k = Console.ReadKey(true).Key;
            } while (k != ConsoleKey.Y && k != ConsoleKey.N);
            if (k == ConsoleKey.Y)
            {
                Console.Clear();
                if (File.Exists(fileName) == true)
                {
                    Console.WriteLine("Файл уже существует, перезаписать его?\n(y - да, n - нет)");
                    do
                    {
                        k = Console.ReadKey(true).Key;
                    } while (k != ConsoleKey.Y && k != ConsoleKey.N);
                    if (k == ConsoleKey.Y)
                    {
                        SaveFile(fileName, apartaments, clientList);
                    }
                }
                else
                {
                    SaveFile(fileName, apartaments, clientList);
                }
            }
            Console.Clear();
        }
        /// <summary>Метод непосредственного сохранения в файл</summary>
        /// <param name="fileName">Имя файла для сохранения</param>
        /// <param name="apartaments">Список квартир</param>
        /// <param name="clientList">Список клиентов</param>
        public static void SaveFile(string fileName, ApartamentList apartaments, ClientList clientList)
        {
            Console.WriteLine("Выполняется сохранение данных...");
            clientList.SaveInFile(fileName);
            apartaments.SaveFile(fileName);
            Console.WriteLine("Данные сохранены");
            Console.ReadKey(true);
        }
    }
}
