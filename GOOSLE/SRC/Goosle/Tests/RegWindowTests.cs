using Xunit;
using Goosle;

namespace Tests
{
    public class RegWindowTests
    {
        // ��������� ����� checkStringInputs: ���������� true, ���� �� ������� ������ (���) ���� 
        // ������������ �������. � ����� � ��� ����� ���� ������ ����� �������� ��������.
        [Theory]
        [InlineData("�������")]
        [InlineData("������ ")]
        [InlineData(" ������")]
        [InlineData("������� ���������")]
        [InlineData("�������-���������")]
        [InlineData("�����")]
        [InlineData("����� ")]
        [InlineData("��������")]
        [InlineData("�����-��������")]
        // �� �� ��������� ����� ���� ������, ��� ��� �� ���� ����� ����� ��� ������ ����������� 
        // �������� ���� ������ � ����, �������������� ��������� �������� �� ������
        public void CheckRegistrationStringInputsFalse(string surname)
        {
            // ��������� ����� CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkStringInputs(surname);
            Assert.False(value);
        }

        [Theory]
        [InlineData("���?-���")]
        [InlineData("�����i")]
        [InlineData("Petrov ")]
        [InlineData("5�����")]
        [InlineData("666")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("C������")]//��������� �
        [InlineData("�����+")]
        [InlineData("���)�� ")]
        [InlineData("������~~~~��")]
        [InlineData("�����-����>����")]
        // �� �� ��������� ����� ���� ������, ��� ��� �� ���� ����� ����� ��� ������ ����������� 
        // �������� ���� ������ � ����, �������������� ��������� �������� �� ������
        public void CheckRegistrationStringInputsTrue(string surname)
        {
            // ��������� ����� CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkStringInputs(surname);
            Assert.True(value);
        }






        // ��������� ����� checkNumInputs: ���������� true, ���� �� ������� ������ �� ������ �����.
        [Theory]
        [InlineData("22456775")]
        [InlineData("3344333 ")]
        [InlineData(" 564567543")]
        [InlineData("4545 546 546")]
        // ������� ������������� ��������� � ����������� ����������, �� ���� �������� � �����
        public void CheckRegistrationNumInputsFalse(string passport)
        {
            // ��������� ����� CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkNumInputs(passport);
            Assert.False(value);
        }

        [Theory]
        [InlineData("46546?-467676")]
        [InlineData("���3553��i")]
        [InlineData("Pe3ov ")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("C������")]//��������� �
        [InlineData("4+")]
        [InlineData("���)�� ")]
        [InlineData("677~~~~787898")]
        [InlineData("666-556>45556")]
        // �� �� ��������� ����� ���� ������, ��� ��� �� ���� ����� ����� ��� ������ ����������� 
        // �������� ���� ������ � ����, �������������� ��������� �������� �� ������
        public void CheckRegistrationNumInputsTrue(string passport)
        {
            // ��������� ����� CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkNumInputs(passport);
            Assert.True(value);
        }





        // ��������� ����� checkEntryCard: ���������� true, ���� �� ������� ������ ������: [sta][0-9]+.
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
        // ������� ������������� ��������� � ����������� ����������, �� ���� �������� � �����
        public void CheckRegistrationEntryCardInputsFalse(string EntryCard)
        {
            // ��������� ����� CalculateSum
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
        [InlineData("���3553��i")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("���)�� ")]
        [InlineData("677~~~~787898")]
        [InlineData("666-556>45556")]
        // �� �� ��������� ����� ���� ������, ��� ��� �� ���� ����� ����� ��� ������ ����������� 
        // �������� ���� ������ � ����, �������������� ��������� �������� �� ������
        public void CheckRegistrationEntryCardInputsTrue(string EntryCard)
        {
            // ��������� ����� CalculateSum
            //RegistrationWindow testWindow = new RegistrationWindow();
            bool value = CheckRegProp.checkEntryCard(EntryCard);
            Assert.True(value);
        }
    }
}

