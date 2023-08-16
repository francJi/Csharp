using System.ComponentModel.DataAnnotations;

namespace MethodsBasic
{
    internal class MethodsBasic
    {
        static void Main(string[] args)
        {

            //// 스태틱 메서드 Add 사용
            //int plused = Calc.Add(3, 5);
            //Console.WriteLine(plused);

            //// 인스턴스 메서드 AddInstance 사용
            //int a = 3;
            //int b = 5;
            //Console.WriteLine("a : " + a);
            //Console.WriteLine("b : " + b);

            //Calc calc = new Calc();
            ////calc.SetElement(3);
            ////calc.SetElement(5);
            
            //calc.Swap(ref a,ref b);
            //Console.WriteLine("a : " + a);
            //Console.WriteLine("b : " + b);

            //int result = calc.GetTotal();
            //Console.WriteLine(result);

            DataTypeManager dataTypeManager = new DataTypeManager();
            string dataName = "short";
            int[] dataInfo = { 2, -32768 };
            DataType newData = dataTypeManager.SetData(dataName, dataInfo);

            Console.WriteLine($"Data Type Name: {newData.typeName}");
            Console.WriteLine($"Byte Size: {newData.byteSize}");
            Console.WriteLine($"Minimum Value: {newData.minValue}");


        }
    }

    public struct DataType
    {
        public string typeName { get; set; }
        public int byteSize { get; set; }
        public int minValue { get; set; }
    }

    public class DataTypeManager
    {
        public DataType SetData(string dataName, int[] dataTypeArray)
        {
            DataType getNew = new DataType { typeName = dataName, byteSize = dataTypeArray[0], minValue = dataTypeArray[1]};
            return getNew;
        }
    }
    

    class Calc
    {
        public static int Add(int a, int b)  // 스태틱 메서드
        {
            int sum = a + b;
            return sum;
        }

        private int total;

        public Calc()
        {
            total = 0;
        }

        public void SetElement(int value)
        {
            total += value;
        }

        public int GetTotal()                // 인스턴스 메서드
        {
            return total;
        }

        public void Swap (ref int a, ref int b)
        {
            int temp = b;
            b = a;
            a = temp;
        }
    }
}