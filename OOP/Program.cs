using System.Security.Cryptography.X509Certificates;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 사용 예시
            //Dog dog = new Dog();
            //dog.Name = "Bobby";
            //dog.Age = 3;

            //dog.Eat();      // Animal is eating.
            //dog.Sleep();    // Animal is sleeping.
            //dog.Bark();     // Dog is barking

            //Cat cat = new Cat();
            //cat.Name = "KKami";
            //cat.Age = 10;

            //cat.Eat();
            //cat.Sleep();
            //cat.Meow();

            //Person francJi = new Person();
            //francJi.Name = "FrancJi";
            //francJi.Age = 28;
            //francJi.CallAge();

            //Person francJi = new Person();                 // 인스턴스 생성
            //Person minJi = new Person();

            //francJi.CompareAge(minJi);
            //francJi.Name = "FrancJi";                       // Main에 실행.
            //francJi.Age = 28;
            //francJi.CallAge();

            //void CallDirect(string Name, int Age)
            //{
            //    Console.WriteLine($"이름은 {Name}, 나이는 {Age.ToString()} .");
            //}

            //string francJiName = "FrancJi";                       // Main 함수에 직접 변수 선언
            //int francJiAge = 28;
            //CallDirect(francJiName, francJiAge);

            //Dog mungs = new Dog("Mungs", 5, "웰시코기");
            //Console.WriteLine($"이름 : {mungs.Name}  나이 : {mungs.Age}  견종 : {mungs.DogBreed}");

            //GameCharacter gunner = new GameCharacter(5);
            //int characterLevel = gunner.Level;
            //Console.WriteLine($"Character Level: {characterLevel}");

            Dog mungs = new Dog("Mungs", 5, "웰시코기");
            Cat nyeaons = new Cat("Nyeaons", 3);
            mungs.Eat();
            nyeaons.Eat();
            mungs.Sleep();
        }
    }
    // 클래스의 필드
    public class GameCharacter
    {
        private int level;

        public int Level
        {
            get { return level;}
            set { level = value; }
        }
        public GameCharacter(int levelParameter)
        {
            Level = levelParameter;
        }
    }


    public class Person
    {
        public string Name { get; set; }                // 필드 : Name
        public int Age { get; set; }                    // 필드 : Age

        public void CallAge()
        {
            Console.WriteLine($"이름은 {Name}, 나이는 {Age.ToString()} .");
        }

        public void CompareAge(Person person2)
        {
            int age1 = Age;
            int age2 = person2.Age;
            if (age1 > age2)
            {
                Console.WriteLine("!!");
            }
        }

        ~Person()
        {
            Console.WriteLine("소멸자 사용");
        }
    }


    interface ImakeSound
    {
        void MakeSound();
    }
    // 부모 클래스
    public class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public virtual void Eat()
        {
            Console.WriteLine("Animal is eating.");
        }

        public void Sleep()
        {
            Console.WriteLine("Animal is sleeping.");
        }

    }

    // 자식 클래스
    public class Dog : Animal, ImakeSound
    {
        public string DogBreed { get; set; }
        
        public Dog (string name, int age, string dogBreed) : base(name, age)
        {
            DogBreed = dogBreed;
            Console.WriteLine("Dog 생성자 호출");
        }

        public void MakeSound()
        {
            Console.WriteLine("Dog is bark.");
        }

        public override void Eat()
        {
            Console.WriteLine("Dog is Eating....");
        }
    }

    public class Puppy : Dog
    {
        public Puppy(string name, int age, string dogBreed) : base(name, age, dogBreed)
        {
        }

        public void CuteAct()
        {
            Console.WriteLine("My Heart is Melting");
        }
    }

    public class Cat : Animal, ImakeSound
    {
        public Cat(string name, int age) : base(name, age)
        {
            Console.WriteLine("Cat 생성자 호출");
        }
        public void Sleep()
        {
            Console.WriteLine("Cat is sleeping.");
        }

        public void MakeSound()
        {
            Console.WriteLine("Cat is meow.");
        }
    }
}