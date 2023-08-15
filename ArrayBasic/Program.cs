namespace ArrayBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = new int[5];       // 크기가 5인 int형 배열 선언
            string[] array2 = new string[3]; // 크기가 3인 string형 배열 선언
            int num = 0;

            // 배열 초기화
            /*
            array1[0] = 1;
            array1[1] = 2;
            array1[2] = 3;
            array1[3] = 4;
            array1[4] = 5;
             --> 보통은 일일히 배열에 넣지않고 규칙에 근거하여 반복문을 활용해 배열을 채운다. */

            for (int i = 0; i < array1.Length; i++)
            {
                array1[i] = i;
            }

            num = array1[0];
        }
    }
}