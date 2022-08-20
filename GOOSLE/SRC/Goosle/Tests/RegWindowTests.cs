using Xunit;
using Goosle;

namespace Tests
{
    public class RegWindowTests
    {
        // тестируем метод checkStringInputs: возвращает true, если во входной строке (ФИО) есть 
        // некорректные символы. В полях с ФИО могут быть только буквы русского алфавита.
        [Theory]
        [InlineData("Смирнов")]
        [InlineData("Иванов ")]
        [InlineData(" Иванов")]
        [InlineData("Уварова Лукьянова")]
        [InlineData("Уварова-Лукьянова")]
        [InlineData("Игорь")]
        [InlineData("Игорь ")]
        [InlineData("Игоревич")]
        [InlineData("Елена-Кристина")]
        // мы не проверяем текст вида ПеТрОВ, так как во всех полях ввода как только пользватель 
        // закончит ввод текста в поле, форматирование автоматом поменяет на Петров
        public void CheckRegistrationStringInputsFalse(string surname)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkStringInputs(surname);
            Assert.False(value);
        }

        [Theory]
        [InlineData("Ива?-нов")]
        [InlineData("Петроi")]
        [InlineData("Petrov ")]
        [InlineData("5ванов")]
        [InlineData("666")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("Cмирнов")]//нерусская С
        [InlineData("Игорь+")]
        [InlineData("Иго)рь ")]
        [InlineData("Игорев~~~~ич")]
        [InlineData("Елена-Крис>тина")]
        // мы не проверяем текст вида ПеТрОВ, так как во всех полях ввода как только пользватель 
        // закончит ввод текста в поле, форматирование автоматом поменяет на Петров
        public void CheckRegistrationStringInputsTrue(string surname)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkStringInputs(surname);
            Assert.True(value);
        }






        // тестируем метод checkNumInputs: возвращает true, если во входной строке не только цифры.
        [Theory]
        [InlineData("22456775")]
        [InlineData("3344333 ")]
        [InlineData(" 564567543")]
        [InlineData("4545 546 546")]
        // пробелы автоматически убираются в графическом интерфейсе, но тоже проверим и здесь
        public void CheckRegistrationNumInputsFalse(string passport)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkNumInputs(passport);
            Assert.False(value);
        }

        [Theory]
        [InlineData("46546?-467676")]
        [InlineData("Пет3553роi")]
        [InlineData("Pe3ov ")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("Cмирнов")]//нерусская С
        [InlineData("4+")]
        [InlineData("Иго)рь ")]
        [InlineData("677~~~~787898")]
        [InlineData("666-556>45556")]
        // мы не проверяем текст вида ПеТрОВ, так как во всех полях ввода как только пользватель 
        // закончит ввод текста в поле, форматирование автоматом поменяет на Петров
        public void CheckRegistrationNumInputsTrue(string passport)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkNumInputs(passport);
            Assert.True(value);
        }





        // тестируем метод checkEntryCard: возвращает true, если во входной строке шаблон: [sta][0-9]+.
        [Theory]
        [InlineData("s22456775")]
        [InlineData("t22456775")]
        [InlineData("a22456775")]
        [InlineData("A22456775")]
        [InlineData("S22456775")]
        [InlineData("T22456775")]
        [InlineData("s3344333 ")]
        [InlineData(" s564567543")]
        [InlineData("s4545 546 546")]
        // пробелы автоматически убираются в графическом интерфейсе, но тоже проверим и здесь
        public void CheckRegistrationEntryCardInputsFalse(string EntryCard)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkEntryCard(EntryCard);
            Assert.False(value);
        }

        [Theory]
        [InlineData("s224&-56775")]
        [InlineData("SS22456775")]
        [InlineData("W22456775")]
        [InlineData("rt22456775")]
        [InlineData("t2245&67,75")]
        [InlineData("46546?-467676")]
        [InlineData("Пет3553роi")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("Иго)рь ")]
        [InlineData("677~~~~787898")]
        [InlineData("666-556>45556")]
        // мы не проверяем текст вида ПеТрОВ, так как во всех полях ввода как только пользватель 
        // закончит ввод текста в поле, форматирование автоматом поменяет на Петров
        public void CheckRegistrationEntryCardInputsTrue(string EntryCard)
        {
            // тестируем метод CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkEntryCard(EntryCard);
            Assert.True(value);
        }
    }
}

