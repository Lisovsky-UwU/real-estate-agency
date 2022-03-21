using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2ооп
{
    /// <summary>Класс, описывающий список квартир</summary>
    class ApartamentList
    {
        /// <summary>Коллекция квартир</summary>
        public List<Apartament> apartList;

        /// <summary>Простой конструктор</summary>
        public ApartamentList()
        {
            apartList = new List<Apartament>();
        }

        /// <summary>Основной метод для работы с записями</summary>
        /// <param name="clientList">Список клиентов</param>
        public void Menu(ClientList clientList)
        {
            ConsoleKey k;
            do
            {
                Console.Clear();
                if (apartList.Count == 0)
                {
                    Console.WriteLine("Квартиры отсутствуют");
                }
                else
                {
                    PrintAparts();
                }
                Console.WriteLine
                    (
                    "\n1 - добавить квартиру, 2 - изменить квартиру, 3 - удалить квартиру, 4 - сменить статус\n" +
                    "0 - назад"
                    );
                k = Console.ReadKey(true).Key;
                if (k == ConsoleKey.D1)
                {
                    AddEntry();
                }
                else if (apartList.Count == 0 && k > ConsoleKey.D1 && k <= ConsoleKey.D4)
                {
                    Console.WriteLine("Записи для редактирования отсутствуют");
                    Console.ReadKey(true);
                }
                else
                {
                    switch (k)
                    {
                        case ConsoleKey.D2:
                            ChoiseChange();
                            break;
                        case ConsoleKey.D3:
                            ChoiseDeleted();
                            break;
                        case ConsoleKey.D4:
                            ChangeStatus(clientList);
                            CorrectFieldWidth();
                            break;
                    }
                }
            } while (k != ConsoleKey.D0);
        }

        /// <summary>Добавить запись в список</summary>
        public void AddEntry()
        {
            apartList.Add(new Apartament(apartList.Count + 1));
            if (apartList[^1].IsCreate == false)
            {
                apartList.RemoveAt(apartList.Count - 1);
            }
            else
            {
                CorrectFieldWidth();
            }
        }

        /// <summary>Метод выбора записи для изменения данных</summary>
        private void ChoiseChange()
        {
            Console.Clear();
            PrintAparts();
            int index = DataClass.ChoiseID("\nВыберите ID для изменения записи", apartList.Count);
            if (index != -1)
            {
                apartList[index].ChangeInfo();
            }
        }

        /// <summary>Метод выбора записи для удаления</summary>
        private void ChoiseDeleted()
        {
            Console.Clear();
            PrintAparts();
            int index = DataClass.ChoiseID("\nВыберите ID для удаления записи", apartList.Count);
            if (index != -1)
            {
                apartList.RemoveAt(index);
                UpdateID();
                Console.WriteLine("Квартира успешно удалена");
                Console.ReadKey();
            }
        }

        /// <summary>Метод выбора записи для изменения статуса</summary>
        /// <param name="clientList">Список клиентов</param>
        private void ChangeStatus(ClientList clientList)
        {
            Console.Clear();
            PrintAparts();
            int index = DataClass.ChoiseID("\nВыберите ID для изменения статуса", apartList.Count);
            if (index != -1)
            {
                apartList[index].ChangeApartStatus(clientList);
            }
        }

        /// <summary>Вывести список квартир</summary>
        public void PrintAparts()
        {
            DataClass.Apartaments.PrintHead();
            foreach (Apartament apar in apartList)
            {
                apar.PrintInfo();
            }
        }

        /// <summary>Прочитать данные из файла</summary>
        /// <param name="fileName">Имя файла для чтения</param>
        /// <param name="clientList">Список клиентов</param>
        public void ReadFromFile(string fileName, List<Client> clientList)
        {
            apartList.Clear();
            using StreamReader reader = new StreamReader(fileName);
            string line;
            while ((line = reader.ReadLine()) != "КВАРТИРЫ") ;
            ApartamentStatus apartStatus;
            Client apartClient;
            while ((line = reader.ReadLine()) != null)
            {
                string[] arrLine = line.Split('|');
                apartStatus = (arrLine[6]) switch
                {
                    "booked" => ApartamentStatus.booked,
                    "sold" => ApartamentStatus.sold,
                    "rented" => ApartamentStatus.rented,
                    _ => ApartamentStatus.available,
                };
                if (Convert.ToInt32(arrLine[7]) == 0)
                {
                    apartClient = null;
                }
                else
                {
                    apartClient = clientList[Convert.ToInt32(arrLine[7]) - 1];
                }
                apartList.Add(new Apartament(
                              Convert.ToInt32(arrLine[0]), arrLine[1], Convert.ToInt32(arrLine[2]), Convert.ToInt32(arrLine[3]),
                              Convert.ToInt32(arrLine[4]), Convert.ToInt32(arrLine[5]), apartStatus, apartClient)
                              );
            }
            CorrectFieldWidth();
        }

        /// <summary>Метод сохранения данных в файл</summary>
        /// <param name="fileName">Имя файла</param>
        public void SaveFile(string fileName)
        {
            string text = "КВАРТИРЫ";
            foreach (Apartament apart in apartList)
            {
                text += "\n" + apart.GetStringData();
            }
            using StreamWriter writer = new StreamWriter(fileName, true);
            writer.WriteLine(text);
        }

        /// <summary>Обновить ID у записей</summary>
        private void UpdateID()
        {
            for (int i = 0; i < apartList.Count; i++)
            {
                apartList[i].ID = i + 1;
            }
        }

        /// <summary>Скорректировать ширину столбцов таблицы</summary>
        public void CorrectFieldWidth()
        {
            DataClass.Apartaments.RefreshWidth();
            foreach (Apartament apar in apartList)
            {
                apar.CorrectWidth();
            }
        }
    }
}
