package LR5Main;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.*;


import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Random;
import java.util.Scanner;

public class LR5MainClass
{
    public static void main(String[] args) throws FileNotFoundException, IOException
    {
        Random random = new Random();

        //Ввод названия файла и паролей
        Scanner scanner = new Scanner(System.in);
        System.out.println("Введите название файла:");
        String fileName = scanner.nextLine();
        System.out.println("Введите пароль для первого листа:");
        String pass1 = scanner.nextLine();
        System.out.println("Введите пароль для второго листа:");
        String pass2 = scanner.nextLine();

        //Создание файла и задание его свойств
        Workbook book = new HSSFWorkbook();
        CellStyle style = book.createCellStyle();
        style.setWrapText(true);
        Font font = book.createFont();
        font.setFontName("Arial");
        font.setFontHeightInPoints((short)10);
        style.setFont(font);

        //Настройка первого листа
        Sheet list1 = book.createSheet("Лист 1");
        list1.protectSheet(pass1);
        list1.setColumnWidth(0, Math.round(40 * 256));
        list1.setColumnWidth(1, Math.round(25 * 256));

        Row nameRow1 = list1.createRow(0);
        Cell nameCell1_1 = nameRow1.createCell(0, CellType.STRING);
        nameCell1_1.setCellValue("Название столбца 1 на листе 1 (строка)");
        nameCell1_1.setCellStyle(style);
        Cell nameCell1_2 = nameRow1.createCell(1, CellType.STRING);
        nameCell1_2.setCellValue("Название столбца 2 на листе 1 (число)");
        nameCell1_2.setCellStyle(style);

        int rowIndex1 = 1;
        Row row1 = null;
        Cell cell1 = null;
        for (int i = 1; i < 5; i++)
        {
            row1 = list1.createRow(rowIndex1++);

            cell1 = row1.createCell(0, CellType.STRING);
            cell1.setCellValue("Строка " + rowIndex1 + " на листе 1");
            cell1.setCellStyle(style);

            cell1 = row1.createCell(1, CellType.NUMERIC);
            cell1.setCellValue(rowIndex1);
            cell1.setCellStyle(style);
        }

        //Настройка второго листа
        Sheet list2 = book.createSheet("Лист 2");
        list2.protectSheet(pass2);
        list2.setColumnWidth(0, Math.round(40 * 256));
        list2.setColumnWidth(1, Math.round(25 * 256));

        Row nameRow2 = list2.createRow(0);
        Cell nameCell2_1 = nameRow2.createCell(0, CellType.STRING);
        nameCell2_1.setCellValue("Название столбца 1 на листе 2 (строка)");
        nameCell2_1.setCellStyle(style);
        Cell nameCell2_2 = nameRow2.createCell(1, CellType.STRING);
        nameCell2_2.setCellValue("Название столбца 2 на листе 2 (число)");
        nameCell2_2.setCellStyle(style);

        int rowIndex2 = 1;
        Row row2 = null;
        Cell cell2 = null;
        for (int i = 1; i < 5; i++)
        {
            row2 = list2.createRow(rowIndex2++);

            cell2 = row2.createCell(0, CellType.STRING);
            cell2.setCellValue("Строка " + rowIndex2 + " на листе 2");
            cell2.setCellStyle(style);

            cell2 = row2.createCell(1, CellType.NUMERIC);
            cell2.setCellValue(rowIndex2 * random.nextInt(10));
            cell2.setCellStyle(style);
        }

        //Закрытие потока
        book.write(new FileOutputStream("src/LR5Main/"+fileName+".xls"));
        book.close();
    }
}
