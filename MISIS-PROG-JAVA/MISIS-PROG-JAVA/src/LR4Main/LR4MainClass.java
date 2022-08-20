package LR4Main;

import java.io.*;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class LR4MainClass
{
    public static void main(String[] args) throws Exception
    {
        //Создание потока на считывание файла
        FileInputStream logFileStream = new FileInputStream("src/LR4Main/phx.log");
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(logFileStream, "windows-1251"));

        //Создание регулярных выражений для юзера, агента, ошибки (причина возникновения + дата)
        Pattern agentPattern = Pattern.compile("agent ver:(\\d+.\\d+.\\d+)");
        Pattern userPattern = Pattern.compile("(\\S+)@");
        Pattern errorPattern = Pattern.compile("(\\d+.\\d+.\\d+.\\d+.\\d+.\\d+.\\d+\\s.\\d+.\\d+.\\s\\w+\\s\\w\\d.\\d.\\d+\\s\\w+.\\s.+)");

        //Инициализация переменных строки и номера ошибки
        String line;
        int errorNum = 1;
        while ((line = bufferedReader.readLine()) != null)
        {
            //При нахождении ошибки
            Matcher errorMatcher = errorPattern.matcher(line);
            if (errorMatcher.find())
            {
                //Вывести разделительную линию между ошибками и номер ошибки
                System.out.println ("--------------------------------------------");
                System.out.println ("Ошибка №" + errorNum);

                //Вывести дату, переформатированную в dd.MM.yyyy и время возникновения ошибки
                DateFormat dateFormat = new SimpleDateFormat("yyyy/mm/dd", Locale.ENGLISH);
                System.out.println ("Дата и время возникновения ошибки: "+new SimpleDateFormat("dd.MM.yyyy").
                        format(dateFormat.parse(line.substring(errorMatcher.start(), errorMatcher.end()).split(" ")[0])) + " " +
                        line.substring(errorMatcher.start(), errorMatcher.end()).split(" ")[1]);
                //Вывести текст ошибки
                System.out.println("Текст ошибки: " + line.substring(errorMatcher.start(), errorMatcher.end()).split("EFOpenError: ")[1]);

                errorNum++;
            }

            //Вывести юзера, у которого возникла ошибка
            Matcher userMatcher = userPattern.matcher(line);
            if (userMatcher.find()) System.out.println ("Пользователь: " + line.substring(userMatcher.start(), userMatcher.end()).split("@")[0]);

            //Вывести версию агента (сервера), на котором была воспроизведена ошибка
            Matcher agentMatcher = agentPattern.matcher(line);
            if (agentMatcher.find()) System.out.println ("Версия сервера (агента): " +
                    line.substring(agentMatcher.start(), agentMatcher.end()).split(":")[1]);
        }
        logFileStream.close();
    }
}
