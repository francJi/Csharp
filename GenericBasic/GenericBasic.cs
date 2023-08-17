namespace GenericBasic
{
    internal class GenericBasic
    {
        static void Main(string[] args)
        {
            // 제너릭 클래스 사용 예시
            Stack<int> intStack = new Stack<int>();
            intStack.Push(1);
            intStack.Push(2);
            intStack.Push(3);
            Console.WriteLine(intStack.Pop()); // 출력 결과: 3
        }
    }

    // 제너릭 클래스 선언 예시
    class Stack<T>
    {
        private T[] elements;
        private int top;

        public Stack()
        {
            elements = new T[100];
            top = 0;
        }

        public void Push(T item)
        {
            elements[top++] = item;
        }

        public T Pop()
        {
            return elements[--top];
        }
    }
}