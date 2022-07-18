using System;
using System.Collections.Generic;
using System.IO;

namespace Telegram_BOT
{
    public static class CustomMethods
    {
        public enum BotStates
        {
            MainMenu,
            CarSearching,
            InformationMenu,
            LanguageMenu
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
            for (int i = 0; i < str.Length; i++)
            {
                if (translit.ContainsKey(str[i]))
                {
                    str = str.Replace(str[i], translit[str[i]]);
                }
            }
            return str;
        }
    }
}
