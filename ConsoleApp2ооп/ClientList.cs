using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2ооп
{
    /// <summary>Класс, описывающий список клиентов</summary>
    class ClientList
    {
        /// <summary>Коллекция клиентов</summary>
        public List<Client> clientList;

        /// <summary>Конструктор да и все на этом</summary>
        public ClientList()
        {
            clientList = new List<Client>();
        }

        /// <summary>Метод чтения данных из файла</summary>
        /// <param name="fileName">Имя файла</param>
        public void ReadFromFile(string fileName)
        {
            clientList.Clear();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != "КВАРТИРЫ")
                {
                    if (line != "КЛИЕНТЫ")
                    {
                        string[] arrLine = line.Split('|');
                        clientList.Add(new Client(Convert.ToInt32(arrLine[0]), arrLine[1], arrLine[2], arrLine[3], arrLine[4]));
                    }
                }
            }
            CorrectAllWidth();
        }

        /// <summary>Метод сохранения данных в файл</summary>
        /// <param name="fileName">Имя файла</param>
        public void SaveInFile(string fileName)
        {
            string text = "КЛИЕНТЫ";
            foreach (Client client in clientList)
            {
                text += "\n" + client.GetStringData();
            }
            using StreamWriter writer = new StreamWriter(fileName, false);
            writer.WriteLine(text);
        }

        /// <summary>Метод добавления новой записи в список</summary>
        private void AddEntry()
        {
            clientList.Add(new Client(clientList.Count + 1));
            CorrectAllWidth();
        }

        /// <summary>Метод обновления ID у записей</summary>
        private void UpdateID()
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                clientList[i].ID = i + 1;
            }
        }

        /// <summary>Метод для изменения данных у определенной записи</summary>
        private void ChangeClientData()
        {
            Console.Clear();
            PrintClientList();
            int index = DataClass.ChoiseID("\nВыберите ID клиента для изменения", clientList.Count);
            if (index != -1)
            {
                clientList[index].ChangeInfo();
                CorrectAllWidth();
            }
        }

        /// <summary>Метод для удаления записи из списка</summary>
        /// <param name="apartList">Список квартир, связанный со списком клиентов</param>
        private void RemoveClient(List<Apartament> apartList)
        {
            Console.Clear();
            PrintClientList();
            int index = DataClass.ChoiseID("\nВыберите ID клиента для удаления", clientList.Count);
            if (index != -1)
            {
                foreach (Apartament apar in apartList)
                {
                    if (apar.Owner == clientList[index])
                    {
                        apar.status = ApartamentStatus.available;
                        apar.Owner = null;
                    }
                }
                clientList.RemoveAt(index);
                UpdateID();
                Console.WriteLine("Запись успешно удалена");
                Console.ReadKey(true);
            }
        }

        /// <summary>Метод вывода всего списка клиентов</summary>
        /// <param name="apartList">Список квартир, связанный со списком клиентов</param>
        private void PrintClientAparts(List<Apartament> apartList)
        {
            Console.Clear();
            PrintClientList();
            int index = DataClass.ChoiseID("\nВыберите ID клиента для вывода всех квартир клиента", clientList.Count);
            if (index != -1)
            {
                Console.Clear();
                int count = 0;
                foreach (Apartament apar in apartList)
                {
                    if (apar.Owner == clientList[index])
                    {
                        if (count == 0)
                        {
                            DataClass.Apartaments.PrintHead();
                        }
                        apar.PrintInfo();
                        count++;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine("У данного клиента отсутствуют квартиры");
                }
                Console.ReadKey(true);
            }
        }

        /// <summary>Метод для работы с записями</summary>
        /// <param name="apartList">Список квартир</param>
        public void Menu(List<Apartament> apartList)
        {
            ConsoleKey k;
            do
            {
                Console.Clear();
                if (clientList.Count == 0)
                {
                    Console.WriteLine("Клиенты отсутствуют");
                }
                else
                {
                    Console.Clear();
                    PrintClientList();
                }
                Console.WriteLine
                    (
                    "\n1 - добавить клиента, 2 - изменить данные клиента, 3 - удалить клиента, 4 - вывести квартиры клиента\n" +
                    "0 - назад"
                    );
                k = Console.ReadKey(true).Key;
                if (k == ConsoleKey.D1)
                {
                    AddEntry();
                }
                else if (clientList.Count == 0 && k > ConsoleKey.D1 && k <= ConsoleKey.D4)
                {
                    Console.WriteLine("Записи для редактирования отсутствуют");
                    Console.ReadKey(true);
                }
                else
                {
                    switch (k)
                    {
                        case ConsoleKey.D2:
                            ChangeClientData();
                            break;
                        case ConsoleKey.D3:
                            RemoveClient(apartList);
                            break;
                        case ConsoleKey.D4:
                            PrintClientAparts(apartList);
                            break;
                    }
                }

            } while (k != ConsoleKey.D0);
            
        }

        /// <summary>Вывести список клиентов</summary>
        public void PrintClientList()
        {
            DataClass.Client.PrintHead();
            foreach (Client client in clientList)
            {
                client.PrintInfo();
            }
        }

        /// <summary>Скорректировать ширину столбцов в таблице</summary>
        private void CorrectAllWidth()
        {
            DataClass.Client.RefreshWidth();
            foreach (Client c in clientList)
            {
                c.CorrectWidth();
            }
        }
    }
}
