using System;

/*
 * Задание:
 * Написать  программу  для моделирования  работы  агентства недвижимости.  
 * Создать  классы: Квартира, Каталог_квартир, Клиент, База_клиентов. 
 * Квартира  должна  иметь  хозяина,она может  быть забронирована, сдана в аренду, продана. 
 * Клиентами являются как хозяева, так и люди, подбирающие квартиру для покупки или аренды. 
 * Самостоятельно продумать структуру классов (определить необходимые поля,  конструкторы,  методы)  и  согласовать  с  преподавателем. 
 * Написать программу для демонстрации работы системы.
 */

namespace ConsoleApp2ооп
{
    class Program
    {
        static void Main(string[] args)
        {
            ApartamentList apartaments = new ApartamentList();
            ClientList clientList = new ClientList();
            Console.CursorVisible = false;
            string fileName = "dataFile.txt";
            FileManagment.TryOpenFile(fileName, apartaments, clientList);

            ConsoleKey k;
            do
            {
                Console.Clear();
                Console.WriteLine
                    (
                    "АГЕНСТВО НЕДВИЖИМОСТИ, ЕПТА\n\n" +
                    "Выберите с чем хотите работать\n" +
                    "1 - квартиры, 2 - клиенты\n" +
                    "0 - на выход"
                    );
                k = Console.ReadKey(true).Key;
                if (k >= ConsoleKey.D1 && k <= ConsoleKey.D2)
                    switch (k)
                    {
                        case ConsoleKey.D1:
                            apartaments.Menu(clientList);
                            break;
                        case ConsoleKey.D2:
                            clientList.Menu(apartaments.apartList);
                            break;
                    }
            } while (k != ConsoleKey.D0);
            if (apartaments.apartList.Count != 0 || clientList.clientList.Count != 0)
            {
                FileManagment.AskToSave(fileName, apartaments, clientList);
            }
        }
    }
}
