package LR2Main;

import org.ini4j.Profile;
import org.ini4j.Wini;

import java.io.FileReader;
import java.io.IOException;
import java.util.Map;
import java.util.Scanner;
import java.util.Set;

public class LR2MainClass
{
    public static void main(String[] args) throws IOException {
        {
            try
            {
                //Считывание введенной информации
                Scanner scanner = new Scanner(System.in);
                System.out.println("Укажите путь к ini-файлу:");
                String filePath = scanner.nextLine();

                //Инициализация переменной для работы с Ini
                Wini ini = new Wini(new FileReader(filePath));
                Integer sectionSize = ini.size();
                System.out.println("Количество секций: " + sectionSize);

                //Проверка на наличие секций
                if (sectionSize != 0)
                {
                    //Вывод секций
                    Set<Map.Entry<String, Profile.Section>> sectionNames = ini.entrySet();
                    for (Map.Entry<String, Profile.Section> sectionName : sectionNames)
                    {
                        //Вывод ключей и их значений
                        System.out.println("[" + sectionName.getValue().getName() + "]");
                        Set<Map.Entry<String, String>> sectionKeys = sectionName.getValue().entrySet();
                        for (Map.Entry<String, String> optionKey : sectionKeys)
                        {
                            System.out.println("\t" + optionKey.getKey() + "=" + optionKey.getValue());
                        }
                    }
                }
            }
            catch (IOException ioException)
            {
                System.out.println("Ошибка при чтении файла: "+ ioException);
            }
            catch (Exception exception)
            {
                System.out.println("При выполнении программы возникла неизвестная ошибка: "+ exception);
            }
        }
    }
}
