using System;
using System.Collections.Generic;
using System.IO;

namespace Telegram_BOT
{
    public class Car
    {
        public uint ID { get; set; }
        public string person { get; set; }
        public string birthday { get; set; }
        public string registrationAddress { get; set; }
        public string operationCode { get; set; }
        public string operationName { get; set; }
        public string registrationDate { get; set; }
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string makeYear { get; set; }
        public string color { get; set; }
        public string kind { get; set; }
        public string body { get; set; }
        public string purpose { get; set; }
        public string fuel { get; set; }
        public string capacity { get; set; }
        public string ownWeight { get; set; }
        public string totalWeight { get; set; }
        public string newRegistrationNumber { get; set; }
        public Car(uint ID, string person, string birthday, string registrationAddress, string operationCode, string operationName,
            string registrationDate, string departmentCode, string departmentName, string brand, string model, string makeYear, string color,
            string kind, string body, string purpose, string fuel, string capacity, string ownWeight, string totalWeight, string newRegistrationNumber)
        {
            this.ID = ID;
            this.person = person;
            this.birthday = birthday;
            this.registrationAddress = registrationAddress;
            this.operationCode = operationCode;
            this.operationName = operationName;
            this.registrationDate = registrationDate;
            this.departmentCode = departmentCode;
            this.departmentName = departmentName;
            this.brand = brand;
            this.model = model;
            this.makeYear = makeYear;
            this.color = color;
            this.kind = kind;
            this.body = body;
            this.purpose = purpose;
            this.fuel = fuel;
            this.capacity = capacity;
            this.ownWeight = ownWeight;
            this.totalWeight = totalWeight;
            this.newRegistrationNumber = newRegistrationNumber;
        }
        public override bool Equals(object obj)
        {
            Car secondCar = obj as Car;
            if(secondCar == null) return false;
            else if((this.ID == secondCar.ID) && (this.person == secondCar.person) && (this.birthday == secondCar.birthday) &&
                (this.registrationAddress == secondCar.registrationAddress) && (this.operationCode == secondCar.operationCode) && 
                (this.operationName == secondCar.operationName) && (this.registrationDate == secondCar.registrationDate) && 
                (this.departmentCode == secondCar.departmentCode) && (this.departmentName == secondCar.departmentName) &&
                (this.brand == secondCar.brand) && (this.model == secondCar.model) && (this.makeYear == secondCar.makeYear) && 
                (this.color == secondCar.color) && (this.kind == secondCar.kind) && (this.body == secondCar.body) &&
                (this.purpose == secondCar.purpose) && (this.fuel == secondCar.fuel) && (this.capacity == secondCar.capacity) &&
                (this.ownWeight == secondCar.ownWeight) && (this.totalWeight == secondCar.totalWeight) && (this.newRegistrationNumber == secondCar.newRegistrationNumber)) return true;
            else return false;
        }
        public string ToStringUkr()
        {
            string carInfoToString = "\r\nУнікальний номер: " + ID + "\r\nТип власника: " + person + "\r\nДата народження власника: " + birthday +
                "\r\nАдреса реєстрації: " + registrationAddress + "\r\nКод операції: " + operationCode + "\r\nПодробиці операції: " + operationName +
                "\r\nДата реєстрації: " + registrationDate + "\r\nНомер місця реєстрації: " + departmentCode + "\r\nМісце реєстрації: " + departmentName +
                "\r\nБренд авто: " + brand + "\r\nМодель авто: " + model + "\r\nРік створення: " + makeYear + "\r\nКолір авто: " + color +
                "\r\nТип: " + kind + "\r\nКузов: " + body + "\r\nПризначення: " + purpose + "\r\nТип палива: " + fuel +
                "\r\nОб'єм двигуна: " + capacity + "\r\nВласна вага: " + ownWeight + "\r\nПовна вага:" + totalWeight + "\r\nНомер авто: " + newRegistrationNumber;
            return carInfoToString;
        }
        public string ToStringRus()
        {
            string carInfoToString = "\r\nУникальный номер: " + ID + "\r\nТип владельца: " + person + "\r\nДата рождения владельца: " + birthday +
                "\r\nАдресс регестрации: " + registrationAddress + "\r\nКод операции: " + operationCode + "\r\nПодробности операции: " + operationName +
                "\r\nДата регестрации: " + registrationDate + "\r\nНомер места регестрации: " + departmentCode + "\r\nМесто регестрации: " + departmentName +
                "\r\nБренд авто: " + brand + "\r\nМодель авто: " + model + "\r\nГод випуска: " + makeYear + "\r\nЦвет авто: " + color +
                "\r\nТип: " + kind + "\r\nКузов: " + body + "\r\nПредназначение: " + purpose + "\r\nТип топлива: " + fuel +
                "\r\nОбъем двигателя: " + capacity + "\r\nСобственная масса: " + ownWeight + "\r\nМаксимальная масса:" + totalWeight + "\r\nНомер авто: " + newRegistrationNumber;
            return carInfoToString;
        }
        public string ToStringEng()
        {
            string carInfoToString = "\r\nID: " + ID + "\r\nOwner type: " + person + "\r\nOwner birthdate: " + birthday +
                "\r\nRegistration adress: " + registrationAddress + "\r\nOperation code: " + operationCode + "\r\nOperation details: " + operationName +
                "\r\nRegistration date: " + registrationDate + "\r\nDepartment code: " + departmentCode + "\r\nDepartment name: " + departmentName +
                "\r\nBrand: " + brand + "\r\nModel: " + model + "\r\nYear of creation: " + makeYear + "\r\nColor: " + color +
                "\r\nType: " + kind + "\r\nBody: " + body + "\r\nPurpose: " + purpose + "\r\nFuel type: " + fuel +
                "\r\nCapacity: " + capacity + "\r\nOwn weight: " + ownWeight + "\r\nTotal weight:" + totalWeight + "\r\nRegistration number: " + newRegistrationNumber;
            return carInfoToString;
        }
        public static Car FindCar(string carNumber, string pathToFolder)
        {
            string[] fileNames = Directory.GetFiles(pathToFolder, "*.csv");
            if (fileNames.Length == 0)
            {
                return null;
            }
            foreach (string fileName in fileNames)
            {
                string line;
                string[] car;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        car = line.Split(';');
                        if (car[20] == carNumber)
                        {
                            return new Car(Convert.ToUInt32(car[0]), car[1], car[2], car[3], car[4], car[5], car[6], car[7], car[8], car[9], car[10],
                                car[11], car[12], car[13], car[14], car[15], car[16], car[17], car[18],
                                car[19], car[20]);
                        }
                    }
                }
            }
            return null;
        }
        public static void WriteResultToFile(string number, string pathToFile, bool flag)
        {
            string str;
            if (flag) str = "Successful search for a car with a number : " + number;
            else str = "Unsuccessful search for a car with a number : " + number;
            using (StreamWriter sw = new StreamWriter(pathToFile, true))
            {
                sw.WriteLine(str);
            }
        }
        public static bool DirectoryValidation(string pathToFolder)
        {
            if (!Directory.Exists(pathToFolder))
            {
                Console.WriteLine("Папки с базою даних не існує!");
                return false;
            }
            if (Directory.GetFiles(pathToFolder).Length == 0)
            {
                Console.WriteLine("Папка с базою даних порожня!");
                return false;
            }
            return true;
        }
        public static bool FileValidation(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                Console.WriteLine("Файл для веденя історії пошуку не уснує!");
                return false;
            }
            return true;
        }
        public static string ConvertToCyrilicAlphabet(string str)
        {
            Dictionary<char, char> translit = new Dictionary<char, char>()
            {
                {'A', 'А'},
                {'B', 'И'},
                {'E', 'Е'},
                {'K', 'К'},
                {'M', 'М'},
                {'H', 'Н'},
                {'O', 'О'},
                {'P', 'Р'},
                {'C', 'С'},
                {'T', 'Т'},
                {'Y', 'У'},
                {'X', 'Х'},
                {'I', 'I'},
            };
            for(int i = 0; i < str.Length; i++)
            {
                if(translit.ContainsKey(str[i]))
                {
                    str = str.Replace(str[i], translit[str[i]]);
                }
            }
            return str;
        }
    }
}
