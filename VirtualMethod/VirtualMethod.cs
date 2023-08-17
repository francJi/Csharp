namespace VirtualMethod
{
    internal class VirtualMethod
    {
        static void Main(string[] args)
        {
            //Unit
            List<Unit> list = new List<Unit>();
            list.Add(new Marine());
            list.Add(new Zergling());

            foreach(Unit unit in list) 
            {
                unit.Move();
            }

            // 추상 클래스
            List<Shape> shapeList = new List<Shape>();
            shapeList.Add(new Circle());
            shapeList.Add(new Square());
            shapeList.Add(new Triangle());

            foreach (Shape shape in shapeList)
            {
                shape.Draw();
            }
        }
    }
    public class Unit
    {
        public virtual void Move()
        {
            Console.WriteLine("두발로 걷기");
        }

        public void Attack()
        {
            Console.WriteLine("Unit 공격");
        }
    }

    public class Marine : Unit
    {

    }

    public class Zergling : Unit
    {
        public override void Move()
        {
            Console.WriteLine("네발로 걷기");
        }
    }

    abstract class Shape
    {
        public abstract void Draw();
    }

    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a circle");
        }
    }

    class Square : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a square");
        }
    }

    class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a triangle");
        }
    }
}