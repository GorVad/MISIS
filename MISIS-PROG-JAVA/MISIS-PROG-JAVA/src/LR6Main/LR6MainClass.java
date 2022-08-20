package LR6Main;

import com.lowagie.text.Image;
import com.lowagie.text.pdf.PdfContentByte;
import com.lowagie.text.pdf.PdfReader;
import com.lowagie.text.pdf.PdfStamper;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Scanner;

public class LR6MainClass
{
    public static void main(String[] args) throws FileNotFoundException, IOException
    {
        //Получение исходных данных: путь к pdf-файлу, путь к штампу, название нового PDF
        Scanner scanner = new Scanner(System.in);
        System.out.println("Укажите путь к исходному PDF-файлу");
        String pfdFileToWaterMarkPath = scanner.nextLine();
        System.out.println("Укажите путь к исходному файлу-штампу");
        String stampFilePath = scanner.nextLine();
        System.out.println("Укажите путь для сохранения PDF-файла со штампом");
        String watermarkedPDFPath = scanner.nextLine();
        System.out.println("Укажите имя PDF-файла со штампом");
        String watermarkedPDFName = scanner.nextLine();

        //Для дальнейшего выполнения будет использован PDF из первой лабораторной
        try
        {
            //Получение файлов PDF и ватермарки
            PdfContentByte contentByte;
            PdfReader pdfFile = new PdfReader(pfdFileToWaterMarkPath);
            PdfStamper stampFile = new PdfStamper(pdfFile, new FileOutputStream(watermarkedPDFPath +  "/" +watermarkedPDFName + ".pdf"));

            //Изменение изображения
            Image img = Image.getInstance(stampFilePath);
            img.scalePercent(15);
            img.setAbsolutePosition(450,75);

            //Установка ватермарки на первую страницу
            contentByte = stampFile.getOverContent(1);
            contentByte.addImage(img);
            stampFile.close();
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


