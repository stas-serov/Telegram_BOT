using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_BOT
{
    class Program
    {
        private static int state = 0;
        private static string language = "Українська";
        private static string token = "5578074153:AAFp1QXZrOjj1DVUzcivQad6Ti31T4yb6QQ";
        private static TelegramBotClient client;
        private static string pathToDatabase = "../../../Database";
        private static string pathToSearchHistory = "../../../history.txt";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            if (!Car.DirectoryValidation(pathToDatabase)) return;
            if(!Car.FileValidation(pathToSearchHistory)) return;            
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            string infoMenu;
            Car car;
            var msg = e.Message;
            switch (msg.Text)
            {
                case "/start":
                    if(language == "Українська")
                    {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Оберіть опцію", replyMarkup: MainMenuButtons());
                    }
                    else if(language == "Русский")
                    {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Выберите опцию", replyMarkup: MainMenuButtons());
                    }
                    else
                    {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Choose the option", replyMarkup: MainMenuButtons());
                    }
                    break;
                case "Пошук автомобіля":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Введіть державний номер:", replyMarkup: SearchMenuButtons());
                    state = 1;
                    break;
                case "Поиск автомобиля":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Введите государственный номер:", replyMarkup: SearchMenuButtons());
                    state = 1;
                    break;
                case "Car search":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Input government number:", replyMarkup: SearchMenuButtons());
                    state = 1;
                    break;
                case "Інформаційне меню":
                    infoMenu = "Даний бот створено для пошуку інформації про автомобілі за державним номером.\n" +
                        "Натисніть кнопку \"Пошук автомобіля\" для здійснення пошуку\n" +
                        "Натисніть кнопку \"Інформаційне меню\" для перегляду інформаційного меню та інструкції щодо користування\n" +
                        "Натисніть кнопку \"Змінити мову\" для зміни мови боту\n";
                    await client.SendTextMessageAsync(msg.Chat.Id, infoMenu);
                    break;
                case "Информационное меню":
                    infoMenu = "Данный бот создан для поиска информации об автомобиле по государственному номеру.\n" +
                        "Нажмите кнопку \"Поиск автомобиля\" для осуществления поиска" +
                        "Нажмите кнопку \"Информационное меню\" для просмотра информационного меню и инструкции насчет пользования\n" +
                        "Нажмите кнопку \"Изменить язык\" для изменения языка бота\n";
                    await client.SendTextMessageAsync(msg.Chat.Id, infoMenu);
                    break;
                case "Information menu":
                    infoMenu = "This bot was created to search information about car by government number.\n" +
                        "Press the button \"Car search\" to search a car" +
                        "Press the button \"Information menu\" to output information menu and instructions for use\n" +
                        "Press the button \"Change the language\" to change the bot language\n";
                    await client.SendTextMessageAsync(msg.Chat.Id, infoMenu);
                    break;
                case "Змінити мову":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Оберіть мову", replyMarkup: LanguageMenuButtons());
                    state = 2;
                    break;
                case "Изменить язык":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Выберите язык", replyMarkup: LanguageMenuButtons());
                    state = 2;
                    break;
                case "Change the language":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Choose the language", replyMarkup: LanguageMenuButtons());
                    state = 2;
                    break;
                case "Повернутися назад":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Оберіть опцію", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                case "Вернуться назад":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Выберите опцию", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                case "Back":
                    await client.SendTextMessageAsync(msg.Chat.Id, "Choose the option", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                case "Українська":
                    language = "Українська";
                    await client.SendTextMessageAsync(msg.Chat.Id, "Оберіть опцію", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                case "Русский":
                    language = "Русский";
                    await client.SendTextMessageAsync(msg.Chat.Id, "Выберите опцию", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                case "English":
                    language = "English";
                    await client.SendTextMessageAsync(msg.Chat.Id, "Choose the option", replyMarkup: MainMenuButtons());
                    state = 0;
                    break;
                default: 
                    if(state == 1)
                    {
                        string messageText = Car.ConvertToCyrilicAlphabet(msg.Text.ToUpper());
                        car = Car.FindCar(messageText, pathToDatabase);
                        if (car != null)
                        {
                            if (language == "Українська")
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Інформація щодо авто:" + car.ToStringUkr());
                            }
                            else if(language == "Русский")
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Информация про автомобиль:" + car.ToStringRus());
                            }
                            else
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Car information:" + car.ToStringEng());
                            }
                            Console.WriteLine("Вдала спроба пошуку автомобіля з номером: " + messageText);
                            Car.WriteResultToFile(messageText, pathToSearchHistory, true);
                        }
                        else
                        {
                            if (language == "Українська")
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Авто з таким номером не знайдено у базі даних! Перевірте номер та " +
                                "спробуйте знову!");
                            }
                            else if (language == "Русский")
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Авто с таким номером не найдено в базе данных! Проверьте номер и" +
                                    "попробуйте снова!");
                            }
                            else
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "Car by this number is not found in database! Check number and" +
                                    "try again");
                            }
                            Console.WriteLine("Невдала спроба пошуку автомобіля з номером: " + messageText);
                            Car.WriteResultToFile(messageText, pathToSearchHistory, false);
                        }
                        if (language == "Українська")
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, "Введіть державний номер:", replyMarkup: SearchMenuButtons());
                        }
                        else if (language == "Русский")
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, "Введите государственный номер:", replyMarkup: SearchMenuButtons());
                        }
                        else
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, "Input government number:", replyMarkup: SearchMenuButtons());
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Некоректна команда!");
                    }
                    break;
            }
        }
        private static IReplyMarkup MainMenuButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup;
            if (language == "Українська")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Пошук автомобіля"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Інформаційне меню"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Змінити мову"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Поиск автомобиля"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Информационное меню"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Изменить язык"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Car search"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Information menu"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Change the language"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            return replyKeyboardMarkup;
        }
        private static IReplyMarkup LanguageMenuButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup;
            if (language == "Українська")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Українська"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Русский"} },
                    new KeyboardButton[] { new KeyboardButton { Text="English"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Повернутися назад"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Українська"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Русский"} },
                    new KeyboardButton[] { new KeyboardButton { Text="English"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Вернуться назад"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Українська"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Русский"} },
                    new KeyboardButton[] { new KeyboardButton { Text="English"} },
                    new KeyboardButton[] { new KeyboardButton { Text="Back"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            return replyKeyboardMarkup;
            return replyKeyboardMarkup;
        }
        private static IReplyMarkup SearchMenuButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup;
            if (language == "Українська")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Повернутися назад"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Вернуться назад"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { new KeyboardButton { Text="Back"} }
                })
                {
                    ResizeKeyboard = true
                };
            }
            return replyKeyboardMarkup;
        }
    }
}
