using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_BOT
{
    class Program
    {
        private static int state = 0;
        private static string language = "Русский";
        private static string token = "5578074153:AAFp1QXZrOjj1DVUzcivQad6Ti31T4yb6QQ";
        private static TelegramBotClient client;
        private static string pathToDatabase = "../../../Database";
        private static string pathToSearchHistory = "../../../history.txt";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            if (!CustomMethods.DirectoryValidation(pathToDatabase)) return;
            if(!CustomMethods.FileValidation(pathToSearchHistory)) return;
            CancellationTokenSource cts = new CancellationTokenSource();
            ReceiverOptions receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            client = new TelegramBotClient(token);
            client.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token);
            Console.ReadLine();
            cts.Cancel();
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    var msg = update.Message;
                    string infoMenu;
                    switch (msg.Text)
                    {
                        case "/start":
                            await client.SendTextMessageAsync(msg.Chat.Id, "Выберите опцию", replyMarkup: MainMenuButtons());
                            break;
                        case "Поиск автомобиля":
                            await client.SendTextMessageAsync(msg.Chat.Id, "Введите государственный номер:", replyMarkup: SearchMenuButtons());
                            state = 1;
                            break;
                        case "Информационное меню":
                            infoMenu = "Данный бот создан для поиска информации об автомобиле по государственному номеру.\n" +
                                "Нажмите кнопку \"Поиск автомобиля\" для осуществления поиска" +
                                "Нажмите кнопку \"Информационное меню\" для просмотра информационного меню и инструкции насчет пользования\n" +
                                "Нажмите кнопку \"Изменить язык\" для изменения языка бота\n";
                            await client.SendTextMessageAsync(msg.Chat.Id, infoMenu);
                            break;
                        case "Изменить язык":
                            await client.SendTextMessageAsync(msg.Chat.Id, "Выберите язык", replyMarkup: LanguageMenuButtons());
                            state = 2;
                            break;
                        case "Вернуться назад":
                            await client.SendTextMessageAsync(msg.Chat.Id, "Выберите опцию", replyMarkup: MainMenuButtons());
                            state = 0;
                            break;
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
                            if (state == 1)
                            {
                                Car car;
                                string messageText = CustomMethods.ConvertToCyrilicAlphabet(msg.Text.ToUpper());
                                car = Car.FindCar(messageText, pathToDatabase);
                                if (car != null)
                                {
                                    if (language == "Українська")
                                    {
                                        await client.SendTextMessageAsync(msg.Chat.Id, "Інформація щодо авто:" + car.ToStringUkr());
                                    }
                                    else if (language == "Русский")
                                    {
                                        await client.SendTextMessageAsync(msg.Chat.Id, "Информация про автомобиль:" + car.ToStringRus());
                                    }
                                    else
                                    {
                                        await client.SendTextMessageAsync(msg.Chat.Id, "Car information:" + car.ToStringEng());
                                    }
                                    Console.WriteLine("Вдала спроба пошуку автомобіля з номером: " + messageText);
                                    CustomMethods.WriteResultToFile(messageText, pathToSearchHistory, true);
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
                                    CustomMethods.WriteResultToFile(messageText, pathToSearchHistory, false);
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
                    break;
                default:
                    break;

            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
        }
        private static IReplyMarkup MainMenuButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup;
            if (language == "Українська")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Пошук автомобіля"},
                    new KeyboardButton[] {"Інформаційне меню"},
                    new KeyboardButton[] {"Змінити мову"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Поиск автомобиля"},
                    new KeyboardButton[] {"Информационное меню"},
                    new KeyboardButton[] {"Изменить язык"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Car search"},
                    new KeyboardButton[] {"Information menu"},
                    new KeyboardButton[] {"Change the language"}
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
                    new KeyboardButton[] {"🇺🇦Українська"},
                    new KeyboardButton[] {"Русский"},
                    new KeyboardButton[] {"English"},
                    new KeyboardButton[] {"Повернутися назад"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Українська"},
                    new KeyboardButton[] {"Русский"},
                    new KeyboardButton[] {"English"},
                    new KeyboardButton[] {"Вернуться назад"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Українська"},
                    new KeyboardButton[] {"Русский"},
                    new KeyboardButton[] {"English"},
                    new KeyboardButton[] {"Back"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            return replyKeyboardMarkup;
        }
        private static IReplyMarkup SearchMenuButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup;
            if (language == "Українська")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Повернутися назад"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else if (language == "Русский")
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Вернуться назад"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            else
            {
                replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] {"Back"}
                })
                {
                    ResizeKeyboard = true
                };
            }
            return replyKeyboardMarkup;
        }
    }
}
