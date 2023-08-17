namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 사용 예시
            Dog dog = new Dog();
            dog.Name = "Bobby";
            dog.Age = 3;

            dog.Eat();      // Animal is eating.
            dog.Sleep();    // Animal is sleeping.
            dog.Bark();     // Dog is barking

            Cat cat = new Cat();
            cat.Name = "KKami";
            cat.Age = 10;

            cat.Eat();
            cat.Sleep();
            cat.Meow();
        }
    }
    // 부모 클래스
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void Eat()
        {
            Console.WriteLine("Animal is eating.");
        }

        public void Sleep()
        {
            Console.WriteLine("Animal is sleeping.");
        }
    }

    // 자식 클래스
    public class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("Dog is bark.");
        }
    }

    public class Cat : Animal
    {
        public void Sleep()
        {
            Console.WriteLine("Cat is sleeping.");
        }

        public void Meow()
        {
            Console.WriteLine("Cat is meow.");
        }
    }
}