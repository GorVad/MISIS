package LR3Main;

import java.io.*;
import java.util.Scanner;
import java.util.zip.*;
import java.io.IOException;

public class LR3MainClass
{
    public static void main(String[] args) throws Exception
    {
        try
        {
            //Считывание введенной информации
            Scanner scanner = new Scanner(System.in);
            System.out.println("Укажите путь к папке/файлу:");
            String filePath = scanner.nextLine();
            System.out.println("Укажите путь к архиву:");
            String zipPath = scanner.nextLine();

            //Создание потоков для работы
            FileOutputStream fileStream = new FileOutputStream(zipPath);
            ZipOutputStream zipStream = new ZipOutputStream(fileStream);
            archive(zipStream, new File(filePath), null);

            //Закрытие потоков
            zipStream.flush();
            fileStream.flush();
            zipStream.close();
            fileStream.close();

            System.out.println("Данные успешно заархивированы");
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
    public static void archive(ZipOutputStream zipStream, File fileToArchive, String fileParentDir) throws Exception
    {
        //Проверка на наличие файоа
        if (fileToArchive == null || !fileToArchive.exists()) return;

        String zipEntryName = fileToArchive.getName();

        if (fileParentDir!=null && !fileParentDir.isEmpty()) zipEntryName = fileParentDir + "/" + fileToArchive.getName();

        //Запуск повторной дочерней записи вложенных файлов
        if (fileToArchive.isDirectory()) for (File file : fileToArchive.listFiles()) archive(zipStream, file, zipEntryName);
        else
            {
                //Запись в архив
                byte[] buffer = new byte[1024];
                FileInputStream fis = new FileInputStream(fileToArchive);
                zipStream.putNextEntry(new ZipEntry(zipEntryName));

                int length;
                while ((length = fis.read(buffer)) > 0) zipStream.write(buffer, 0, length);
                zipStream.closeEntry();
                fis.close();
            }
    }
}
