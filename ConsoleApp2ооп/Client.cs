using System;

namespace ConsoleApp2ооп
{
    /// <summary>Класс, описывающий клиента</summary>
    class Client
    {
        private int ID_client;
        private string fullName, dateOfBirth, phoneNumber, passport;

        /// <summary>Номер записи</summary>
        public int ID
        {
            get
            {
                return ID_client;
            }
            set
            {
                ID_client = value;
                DataClass.Client.WID = (int)Math.Log10(ID_client) + 1;
            }
        }

        /// <summary>Полное имя клиента</summary>
        public string Name { get { return fullName; } }

        /// <summary>Простой конструктор</summary>
        /// <param name="_ID">Номер записи</param>
        public Client(int _ID)
        {
            ID_client = _ID;
            Console.Write("Введите ФИО клиента: ");
            fullName = Console.ReadLine();
            Console.Write("Введите дату рождения клиента: ");
            dateOfBirth = Console.ReadLine();
            Console.Write("Введите телефон клиента: ");
            phoneNumber = Console.ReadLine();
            Console.Write("Введите паспорт клиента: ");
            passport = Console.ReadLine();
        }

        /// <summary>
        /// Конструктор с передаваемыми значениями
        /// </summary>
        /// <param name="_ID">Номер записи</param>
        /// <param name="_fullName">Полное имя клиента</param>
        /// <param name="_dataOfBirth">Дата рождения клиента</param>
        /// <param name="_phoneNumber">Номер телефона клиента</param>
        /// <param name="_passport">Паспорт клиента</param>
        public Client(int _ID, string _fullName, string _dataOfBirth, string _phoneNumber, string _passport)
        {
            ID_client = _ID;
            fullName = _fullName;
            dateOfBirth = _dataOfBirth;
            phoneNumber = _phoneNumber;
            passport = _passport;
        }

        /// <summary>Метод для изменения записи</summary>
        public void ChangeInfo()
        {
            ConsoleKey k;
            do
            {
                Console.Clear();
                DataClass.Client.PrintHead();
                PrintInfo();
                Console.WriteLine
                    (
                    "\nВыберите поле для изменения:\n" +
                    "1 - ФИО, 2 - Дата рождения, 3 - Телефон, 4 - Паспорт\n" +
                    "0 - Назад"
                    );
                k = Console.ReadKey(true).Key;
                if (k >= ConsoleKey.D1 && k <= ConsoleKey.D4)
                {
                    Console.CursorVisible = true;
                    Console.Write("Введите новое значение: ");
                    string newData = Console.ReadLine();
                    switch (k)
                    {
                        case ConsoleKey.D1:
                            fullName = newData;
                            break;
                        case ConsoleKey.D2:
                            dateOfBirth = newData;
                            break;
                        case ConsoleKey.D3:
                            phoneNumber = newData;
                            break;
                        case ConsoleKey.D4:
                            passport = newData;
                            break;
                    }
                    Console.CursorVisible = false;
                    Console.WriteLine("Данные были изменены");
                    Console.ReadKey(true);
                    CorrectWidth();
                }
            } while (k != ConsoleKey.D0);
        }

        /// <summary>Вывести информацию о записи</summary>
        public void PrintInfo()
        {
            Console.WriteLine
                    (
                    ID_client.ToString().PadRight(DataClass.Client.WID) + " | " +
                    fullName.PadRight(DataClass.Client.WFullName) + " | " +
                    dateOfBirth.PadRight(DataClass.Client.WDateOfBirth) + " | " +
                    phoneNumber.PadRight(DataClass.Client.WPhoneNumber) + " | " +
                    passport.PadRight(DataClass.Client.WPassport)
                    );
        }

        /// <summary>Получить строку с данными по записи</summary>
        /// <returns>Вернет строку с данными о записи</returns>
        public string GetStringData()
        {
            return $"{ID_client}|{fullName}|{dateOfBirth}|{phoneNumber}|{passport}";
        }

        /// <summary>Скорректировать ширину столбцов в таблице</summary>
        public void CorrectWidth()
        {
            DataClass.Client.WID =          (int)Math.Log10(ID) + 1;
            DataClass.Client.WFullName =    fullName.Length;
            DataClass.Client.WDateOfBirth = dateOfBirth.Length;
            DataClass.Client.WPhoneNumber = phoneNumber.Length;
            DataClass.Client.WPassport =    passport.Length;
        }
    }
}
