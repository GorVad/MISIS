package LR1Main;

import java.io.*;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.text.PDFTextStripper;

public class LR1MainClass
{
    public static void main(String[] args)
    {
        try
        {
            File pdfFile = new File("src/LR1Main/LR1_PDFText.pdf");
            File txtFile = new File("LR1Main/LR1_TxtToWrite.txt");
            PDFTextStripper pdfTextStripper = new PDFTextStripper();

            //Чтение файла
            PDDocument pdfDocumentToRead = PDDocument.load(pdfFile);

            //Запись в консоль
            String pdfText = pdfTextStripper.getText(pdfDocumentToRead);
            System.out.println(pdfText);
            pdfDocumentToRead.close();

            //Сохранение в текстовый файл
            FileOutputStream fileToSaveFromPDF = new FileOutputStream(txtFile);
            DataOutputStream outStream = new DataOutputStream(new BufferedOutputStream(fileToSaveFromPDF));
            outStream.writeUTF(pdfText);
            outStream.close();
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
