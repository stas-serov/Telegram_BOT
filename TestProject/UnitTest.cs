using System;
using Xunit;
using Telegram_BOT;

namespace TestProject
{
    public class UnitTest
    {
        [Fact]
        public void TestFindCar()
        {
            Car expectedCar = new Car(3267016910, "P", "1989-06-12", "2310136300", "530", "\"530 - «Õﬂ““ﬂ « Œ¡ÀI ” ƒÀﬂ –≈¿ÀI«¿÷IØ\"",
                "2017-01-03", "2341", "\"÷ÂÌÚ 2341\"", "\"¬¿«  210700-20\"", "210700-20", "2008", "«≈À≈Õ»…", "À≈√ Œ¬»…", "—≈ƒ¿Õ-B", "«¿√¿À‹Õ»…",
                "¡≈Õ«»Õ", "1451", "1060", "1460", "08¬¿8008"); ;
            Car actualCar = Car.FindCar("08¬¿8008", @"..\..\..\..\Telegram_BOT\Database");
            Assert.Equal(expectedCar, actualCar);
        }
        [Fact]
        public void TestValidationDirectory()
        {
            bool expectedResult = true;
            bool actualResult = Car.DirectoryValidation(@"..\..\..\..\Telegram_BOT\Database");
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void TestValidationFile()
        {
            bool expectedResult = true;
            bool actualResult = Car.FileValidation(@"..\..\..\..\Telegram_BOT\history.txt");
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void TestConvertToCyrilicAlphabet()
        {
            string expectedResult = "”””";
            string actualResult = Car.ConvertToCyrilicAlphabet("YYY");
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
