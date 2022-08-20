package FINAL;


import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.regex.Pattern;
import java.util.regex.Matcher;

public class FinalMainClass
{
    public static void main(String[] args) throws IOException {
        //Обозначение путей к файлам для чтения/записи
        Scanner scanner = new Scanner(System.in);
        System.out.println("Введите путь к базе Телефон-ФИО (для чтения):");
        String telNamePath = scanner.nextLine();
        System.out.println("Введите путь к базе Телефон-Комментарий (для чтения):");
        String telComPath = scanner.nextLine();
        System.out.println("Введите путь к базе Телефон-ФИО-Комментарий (для записи):");
        String telNameComPath = scanner.nextLine();

        //Открытие потоков для работы с файлами
        FileInputStream telcomFileStream = new FileInputStream(telComPath);
        BufferedReader telcomBufferedReader = new BufferedReader(new InputStreamReader(telcomFileStream));
        FileInputStream telnameFileStream = new FileInputStream(telNamePath);
        BufferedReader telnameBufferedReader = new BufferedReader(new InputStreamReader(telnameFileStream));
        FileOutputStream telnamecomFileStream = new FileOutputStream(telNameComPath);
        DataOutputStream telnamecomOutStream = new DataOutputStream(new BufferedOutputStream(telnamecomFileStream));
        PrintStream printStream = new PrintStream(telnamecomFileStream);

        //Создание регулярного выражения для поиска телефона
        Pattern telephone = Pattern.compile(".+\\d");

        //Создание списков из объектов
        ArrayList<String> telnameList = new ArrayList<String>();
        ArrayList<String> telcomList = new ArrayList<String>();
        
        String telnameLine;
        String telcomLine;
        while ((telnameLine = telnameBufferedReader.readLine()) != null)
        {
            Matcher telnameMatcher = telephone.matcher(telnameLine);
            if (telnameMatcher.find()) telnameList.add(telnameLine);
        }
        while ((telcomLine = telcomBufferedReader.readLine()) != null)
        {
            Matcher telcomMatcher = telephone.matcher(telcomLine);
            if (telcomMatcher.find())telcomList.add(telcomLine);
        }

        //Поиск записей в списках с идентичными номерами и формирование итогового файла
        printStream.println("Телефон - ФИО - Комментарий");
        printStream.println("-----------------------------");
        for (String telname : telnameList)
        {
            for (String telcom : telcomList)
            {
                if (telname.split(" - ")[0].equals(telcom.split(" - ")[0]))
                    printStream.println(telname + " - " + telcom.split(" - ")[1]);
            }
        }

        //Закрытие потоков для работы с файлами
        telnamecomOutStream.close();
        telcomFileStream.close();
        telnameFileStream.close();
    }
}
