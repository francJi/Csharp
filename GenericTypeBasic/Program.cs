namespace GenericTypeBasic
{
    internal class Program
    {
        delegate void Delegate();
        static void Method1()
        {
            Console.WriteLine("메서드 1 호출");
        }

        static void Method2()
        {
            Console.WriteLine("메서드 2 호출");
        }
        static void Main(string[] args)
        {
            Delegate delegatePratice = Method1;
            delegatePratice += Method2;

            // 등록된 모든 메서드 호출
            delegatePratice();

            Console.WriteLine("끝");

            EventExample eventPractice = new EventExample();
            EventOccur eventOccur = new EventOccur();

            eventPractice.EventHandler += eventOccur.Greeting;
            eventPractice.EventHandler += eventOccur.GoodBye;
            eventPractice.Func("HaHAHa");
            eventPractice.Func("afajfpasdj");

            //Calculator calc = new Calculator();
            //int sumInt = calc.Add(1, 2);
            //float sumFloat = calc.Add(1f, 2f);
            //double sumDouble = calc.Add(3.14d, 2.86d);
            //string sumString = calc.Add("이제야 ","절반했다..");

            //Console.WriteLine($"int 타입 Add : {sumInt}      Type : {sumInt.GetType().Name}");
            //Console.WriteLine($"float 타입 Add : {sumFloat}      Type : {sumFloat.GetType().Name}");
            //Console.WriteLine($"double 타입 Add : {sumDouble}      Type : {sumDouble.GetType().Name}");
            //Console.WriteLine($"string 타입 Add : {sumString}      Type : {sumString.GetType().Name}");

            //Iinterview<string, string> interview = new Animal<string, string>("복순이", "12살");
            //interview.CallName();
            //Iinterview<string, int> interviewSecond = new Animal<string, int>("복순이", 12);
            //interview.IntroduceAnimal("복순이", 12);
        }

        delegate void DelegateType(string message);

        class EventExample
        {
            public event DelegateType EventHandler;
            public void Func(string Message)
            {
                EventHandler(Message);
            }
        }

        class EventOccur
        {
            public void Greeting(string message)
            {
                Console.WriteLine($"hi {message}");
            }

            public void GoodBye(string message)
            {
                Console.WriteLine($"bye {message}");
            }
        }
        interface Iinterview<T1, T2>
        {
            void CallName();
            void IntroduceAnimal(T1 name, T2 age);
        }

        public class Animal<T1, T2> : Iinterview<T1, T2>
        {
            public T1 Name { get; set; }
            public T2 Age { get; set; }

            public Animal(T1 name, T2 age)
            {
                Name = name;
                Age = age;
            }

            public void CallName()
            {
                Console.WriteLine($"이 동물의 이름은 {Name} 입니다");
            }

            public void IntroduceAnimal(T1 name, T2 age)
            {
                Console.WriteLine($"이름 : {name}, 나이 : {age}");
            }

            public void Eat()
            {
                Console.WriteLine("Animal is eating.");
            }

            public void Sleep()
            {
                Console.WriteLine("Animal is sleeping.");
            }

        }
        interface Icollection<T>
        {
            void Add(T item);
            void Remove(T item);
            int Count { get; }
        }

        class CollectionCalc<T> : Icollection<T>
        {
            private List<T> items = new List<T>();

            public void Add(T item)
            {
                items.Add(item);
            }

            public void Remove(T item)
            {
                items.Remove(item);
            }

            public int Count
            {
                get { return items.Count; }
            }
        }
        public class Calculator
        {
            public int Add(int a, int b)                       // Method OverLoading
            { 
                return a + b; 
            }
            public float Add(float a, float b)                 // Method OverLoading
            {
                return a + b;
            }
            public double Add(double a, double b)               // Method OverLoading
            {
                return a + b;
            }
            public string Add(string a, string b)               // Method OverLoading
            {
                return a + b;
            }
        }
    }
}