using System.Linq;
using System.Text.RegularExpressions;

namespace Goosle
{
    public class CheckRegProp
    {
        //Проверка полей. Возвращает true, если будет найдены цифры или символы не русккого алфавита
        public static bool checkStringInputs(string userInfo)
        {
            int hasNonRussianLetters = Regex.Matches(Regex.Replace(userInfo, @"\W", ""), @"\P{IsCyrillic}").Count;
            if (userInfo.Any(char.IsDigit) || 
                userInfo.Replace("-", "").Any(char.IsPunctuation) ||
                userInfo.Replace("-", "").Any(char.IsControl) ||
                userInfo.Replace("-", "").Any(char.IsSymbol) ||
                userInfo.Replace(" ", "").Length == 0 ||
                hasNonRussianLetters > 0)
            {
                return true;
            }
            else
                return false;
        }
        //Проверка полей. Возвращает true, если будет найдены не числовые символы
        public static bool checkNumInputs(string userInfo)
        {
            if (!userInfo.Replace(" ", "").All(char.IsDigit) || userInfo.Replace(" ", "").Length == 0)
            {
                return true;
            }
            else
                return false;
        }
        //Проверка полей. Возвращает true, если номер пропуска подходит под шаблон s24556.., t4543..., a23455... 
        public static bool checkEntryCard(string userInfo)
        {
            userInfo = userInfo.Replace(" ", "");
            if (userInfo.Length != 0)
                userInfo = userInfo.First().ToString().ToLower() + userInfo.Substring(1);
            if (!Regex.IsMatch(userInfo, @"^[a,s,t][0-9]+$") || userInfo.Length == 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
