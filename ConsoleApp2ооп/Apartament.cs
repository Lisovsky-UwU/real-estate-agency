using System;

namespace ConsoleApp2ооп
{
    /// <summary>Класс, описывающий квартиру</summary>
    class Apartament
    {
        /// <summary>Хозяин квартиры</summary>
        public Client Owner
        {
            get
            {
                return ownerClient;
            }
            set
            {
                if (this.status == ApartamentStatus.available)
                {
                    ownerClient = value;
                }
                else
                {
                    Console.WriteLine("Квартира " + (this.status == ApartamentStatus.sold ? "продана" :
                        (this.status == ApartamentStatus.booked ? "забронирована" : "сдается")));
                    Console.ReadKey(true);
                }
            }
        }
        private Client ownerClient;
        public int ID;
        private string address;
        private int numberOfRooms, area, sellingPrice, leasePrice;

        /// <summary>Статус квартиры/summary>
        public ApartamentStatus status;

        /// <summary>True - когда элемент успешно создан</summary>
        public bool IsCreate { get; } = false;

        /// <summary>Простой конструктор</summary>
        /// <param name="_ID">Номер записи</param>
        public Apartament(int _ID)
        {
            try
            {
                ID = _ID;
                Console.Write("Введите адрес квартиры: ");
                address = Console.ReadLine();
                Console.Write("Введите число комнат квартиры: ");
                numberOfRooms = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите площадь квартиры: ");
                area = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите стоимость покупки квартиры: ");
                sellingPrice = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите стоимость аренды квартиры: ");
                leasePrice = Convert.ToInt32(Console.ReadLine());
                IsCreate = true;
            }
            catch
            {
                Console.WriteLine("Ошибка ввода данных");
                Console.ReadKey(true);
            }
        }

        /// <summary>Конструктор по передаваемым параметрам</summary>
        /// <param name="_ID">ID записи</param>
        /// <param name="_address">Адрес квартиры</param>
        /// <param name="_numberOfRooms">Количество комнат в квартире</param>
        /// <param name="_area">Площадь квартиры</param>
        /// <param name="_sellingPrice">Стоимость продажи квартиры</param>
        /// <param name="_leasePrice">Стоимость аренды квартиры</param>
        /// <param name="_status">Статус квартиры (поумолчанию available)</param>
        /// <param name="_ownerClient">Хозяин квартиры (поумолчанию null)</param>
        public Apartament(int _ID, string _address, int _numberOfRooms, int _area, int _sellingPrice, 
            int _leasePrice, ApartamentStatus _status = ApartamentStatus.available, Client _ownerClient = null)
        {
            ID = _ID;
            address = _address;
            numberOfRooms = _numberOfRooms;
            area = _area;
            sellingPrice = _sellingPrice;
            leasePrice = _leasePrice;
            status = _status;
            ownerClient = _ownerClient;
            IsCreate = true;
        }

        /// <summary>Вывести информацию о записи</summary>
        public void PrintInfo()
        {
            Console.WriteLine
                (
                ID.ToString().PadRight(DataClass.Apartaments.WID) + " | " +
                address.PadRight(DataClass.Apartaments.WAddress) + " | " + numberOfRooms.ToString().PadRight(DataClass.Apartaments.WNumberOfRooms) + " | " +
                area.ToString().PadRight(DataClass.Apartaments.WArea) + " | " + sellingPrice.ToString().PadRight(DataClass.Apartaments.WSellingPrice) + " | " +
                leasePrice.ToString().PadRight(DataClass.Apartaments.WLeasePrice) + " | " +
                (status == ApartamentStatus.available ? "Свободна" : 
                (status == ApartamentStatus.rented ? "Арендуется" : 
                (status == ApartamentStatus.booked ? "Забронирована" : "Продана"))).PadRight(DataClass.Apartaments.WStatus) + " | " +
                (Owner == null ? "Отсутствует" : $"Имеется ({Owner.ID}:{Owner.Name})")
                );

        }

        /// <summary>Получить строку содержащию информацию о записи</summary>
        /// <returns>Вернет строку со значениями записи</returns>
        public string GetStringData()
        {
            return $"{ID}|{address}|{numberOfRooms}|{area}|{sellingPrice}|{leasePrice}|{status}|{(Owner == null ? "0" : Owner.ID.ToString())}";
        }

        /// <summary>
        /// Метод изменения статуса и клиента у квартиры
        /// </summary>
        /// <param name="newStatus">Новый статус</param>
        /// <param name="clientBase">Список клиентов</param>
        private void ChangeClientOwner(ApartamentStatus newStatus, ClientList clientBase)
        {
            Console.Clear();
            if (status == ApartamentStatus.available)
            {
                clientBase.PrintClientList();
                int index = DataClass.ChoiseID("Выберите ID клиента", clientBase.clientList.Count);
                if (index == -1)
                {
                    return;
                }
                Owner = clientBase.clientList[index];
            }

            status = newStatus;
            Console.WriteLine("Статус квартиры успешно изменен");
            Console.ReadKey(true);
        }

        /// <summary>Изменить статус квартиры</summary>
        /// <param name="clientList">Список клиентов</param>
        public void ChangeApartStatus(ClientList clientList)
        {
            ConsoleKey k;
            do
            {
                Console.Clear();
                DataClass.Apartaments.PrintHead();
                PrintInfo();
                Console.WriteLine
                    (
                    "\nВыберите новый статус квартиры:\n" +
                    "1 - Свободна, 2 - Забронирована, 3 - Сдается, 4 - Продана\n" +
                    "0 - Назад"
                    );
                k = Console.ReadKey(true).Key;
                switch (k)
                {
                    case ConsoleKey.D1:
                        status = ApartamentStatus.available;
                        Owner = null;
                        Console.WriteLine("Статус успешно изменен");
                        Console.ReadKey(true);
                        return;
                    case ConsoleKey.D2:
                        ChangeClientOwner(ApartamentStatus.booked, clientList);
                        return;
                    case ConsoleKey.D3:
                        ChangeClientOwner(ApartamentStatus.rented, clientList);
                        return;
                    case ConsoleKey.D4:
                        ChangeClientOwner(ApartamentStatus.sold, clientList);
                        return;
                }
            } while (k != ConsoleKey.D0);
        }

        /// <summary>Метод изменения данных о записи</summary>
        public void ChangeInfo()
        {
            ConsoleKey k;
            do
            {
                Console.Clear();
                DataClass.Apartaments.PrintHead();
                PrintInfo();
                Console.WriteLine
                    (
                    "\nВыберите поле для изменения:\n" +
                    "1 - Адрес, 2 - Кол-во комнат, 3 - Площадь, 4 - Цена покупки, 5 - Цена аренды\n" +
                    "0 - Назад"
                    );
                k = Console.ReadKey(true).Key;
                if (k >= ConsoleKey.D1 && k <= ConsoleKey.D5)
                {
                    Console.Write("Введите новое значение: ");
                    string newData = Console.ReadLine();
                    try
                    {
                        switch (k)
                        {
                            case ConsoleKey.D1:
                                address = newData;
                                break;
                            case ConsoleKey.D2:
                                numberOfRooms = Convert.ToInt32(newData);
                                break;
                            case ConsoleKey.D3:
                                area = Convert.ToInt32(newData);
                                break;
                            case ConsoleKey.D4:
                                sellingPrice = Convert.ToInt32(newData);
                                break;
                            case ConsoleKey.D5:
                                leasePrice = Convert.ToInt32(newData);
                                break;
                        }
                        Console.WriteLine("Данные были изменены");
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка изменения данных, данные не были изменены");
                    }
                    Console.ReadKey(true);
                    CorrectWidth();
                }
            } while (k != ConsoleKey.D0);
        }

        /// <summary>Метод корректировки ширины столбцов у таблицы</summary>
        public void CorrectWidth()
        {
            DataClass.Apartaments.WID =             (int)Math.Log10(ID) + 1;
            DataClass.Apartaments.WAddress =        address.Length;
            DataClass.Apartaments.WNumberOfRooms =  (int)Math.Log10(numberOfRooms) + 1;
            DataClass.Apartaments.WArea =           (int)Math.Log10(area) + 1;
            DataClass.Apartaments.WSellingPrice =   (int)Math.Log10(sellingPrice) + 1;
            DataClass.Apartaments.WLeasePrice =     (int)Math.Log10(leasePrice) + 1;
        }
    }
}
