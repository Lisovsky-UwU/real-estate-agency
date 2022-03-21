using System;

namespace ConsoleApp2ооп
{
    /// <summary>Описывает статус квартиры</summary>
    public enum ApartamentStatus
    {
        /// <summary>Квартира свободна</summary>
        available,
        /// <summary>Квартира забронирована</summary>
        booked,
        /// <summary>Квартира продана</summary>
        sold,
        /// <summary>Квартира находится в аренде</summary>
        rented
    }

    /// <summary>Класс, хранящий в себе дополнительную необходимую информацию и методы</summary>
    public class DataClass
    {
        /// <summary>Информация для класса Apartament</summary>
        public class Apartaments
        {
            /// <summary>Ширина столбца <em>ID</em></summary>
            public static int WID {             get { return wID_value; }               
                                                set { wID_value = Math.Max(value, wID_value); } }
            /// <summary>Ширина столбца <em>Адрес</em></summary>
            public static int WAddress {        get { return wAddress_value; }          
                                                set { wAddress_value = Math.Max(value, wAddress_value); } }
            /// <summary>Ширина столбца <em>Кол-во комнат</em></summary>
            public static int WNumberOfRooms {  get { return wNumberOfRooms_value; }   
                                                set { wNumberOfRooms_value = Math.Max(value, wNumberOfRooms_value); } }
            /// <summary>Ширина столбца <em>Площадь</em></summary>
            public static int WArea {           get { return wArea_value; }             
                                                set { wArea_value = Math.Max(value, wArea_value); } }
            /// <summary>Ширина столбца <em>Цена продажи</em></summary>
            public static int WSellingPrice {   get { return wSellingPrice_value; }     
                                                set { wSellingPrice_value = Math.Max(value, wSellingPrice_value); } }
            /// <summary>Ширина столбца <em>Цена аренды</em></summary>
            public static int WLeasePrice {     get { return wLeasePrice_value; }       
                                                set { wLeasePrice_value = Math.Max(value, wLeasePrice_value); } }
            /// <summary>Ширина столбца <em>Статус</em></summary>
            public static int WStatus {         get { return wStatus_value; }           
                                                set { wStatus_value = Math.Max(value, wStatus_value); } }

            private static int wID_value = 3, wAddress_value = 5, wNumberOfRooms_value = 6, wArea_value = 7, 
                               wSellingPrice_value = 12, wLeasePrice_value = 11, wStatus_value = 13;

            /// <summary>Вывести заголовок таблицы</summary>
            public static void PrintHead()
            {
                Console.WriteLine
                    (
                    "ID".PadRight(WID) + " | " + "Адрес".PadRight(WAddress) + " | " + 
                    "Комнат".PadRight(WNumberOfRooms) + " | " + "Площадь".PadRight(WArea) + " | " + 
                    "Цена покупки".PadRight(WSellingPrice) + " | " + "Цена аренды".PadRight(WLeasePrice) + " | " + 
                    "Статус".PadRight(WStatus) + " | " + "Хозяин"
                    );
            }
            /// <summary>Вернуть значения поумолчанию</summary>
            public static void RefreshWidth()
            {
                WID = 3;
                WAddress = 5;
                WNumberOfRooms = 6;
                WArea = 7;
                WSellingPrice = 12;
                WLeasePrice = 11;
                WStatus = 13;
            }
        }
        public class Client
        {
            /// <summary>Ширина столбца <em>ID</em></summary>
            public static int WID {             get { return wID_value; }
                                                set { wID_value = Math.Max(value, wID_value); } }
            /// <summary>Ширина столбца <em>ФИО</em></summary>
            public static int WFullName {       get { return wFullName_value; }
                                                set { wFullName_value = Math.Max(value, wFullName_value); } }
            /// <summary>Ширина столбца <em>Дата рож</em></summary>
            public static int WDateOfBirth {    get { return wDateOfBirth_value; }
                                                set { wDateOfBirth_value = Math.Max(value, wDateOfBirth_value); } }
            /// <summary>Ширина столбца <em>Телефон</em></summary>
            public static int WPhoneNumber {    get { return wPhoneNumber_value; }
                                                set { wPhoneNumber_value = Math.Max(value, wPhoneNumber_value); } }
            /// <summaryШирина столбца ><em>Паспорт</em></summary>
            public static int WPassport {       get { return wPassport_value; }
                                                set { wPassport_value = Math.Max(value, wPassport_value); } }
            
            private static int wID_value = 3, wFullName_value = 3, wDateOfBirth_value = 8, wPhoneNumber_value = 7, wPassport_value = 8;

            /// <summary>Вывести заголовок таблицы</summary>
            public static void PrintHead()
            {
                Console.WriteLine
                    (
                    "ID".PadRight(WID) + " | " +
                    "ФИО".PadRight(WFullName) + " | " +
                    "Дата рож".PadRight(WDateOfBirth) + " | " +
                    "Телефон".PadRight(WPhoneNumber) + " | " +
                    "Паспорт".PadRight(WPassport)
                    );
            }
            /// <summary>Вернуть значения поумолчанию</summary>
            public static void RefreshWidth()
            {
                WID = 3;
                WFullName = 3;
                WDateOfBirth = 8;
                WPhoneNumber = 7;
                WPassport = 8;
            }
        }

        /// <summary>Метод для получения индекса выбираемого пользователем элемента от 0 до установленного максимума</summary>
        /// <param name="msg">Подсказка, выводимая пользователю</param>
        /// <param name="maxID">Максимальный предел</param>
        /// <returns>
        /// <para>Возвращает индекс выбранного элемента</para>
        /// <para>Вернет -1, если пользователь отказался выбирать</para>
        /// </returns>
        public static int ChoiseID(string msg, int maxID)
        {
            Console.WriteLine(msg);
            Console.WriteLine("0 - назад");
            string k;
            do
            {
                Console.CursorVisible = true;
                Console.Write("ID: ");
                k = Console.ReadLine();
                try
                {
                    if (Convert.ToInt32(k) > 0 && Convert.ToInt32(k) <= maxID)
                    {
                        Console.CursorVisible = false;
                        return Convert.ToInt32(k) - 1;
                    }
                    Console.WriteLine("Введите число соответствующее записям в таблице");
                }
                catch
                {
                    Console.WriteLine("Введите корректное число");
                }
            } while (k != "0");
            Console.CursorVisible = false;
            return -1;
        }
    }
}
