namespace interfaceEx;

internal class Program
{
    interface IMovable
    {
        public void Move(int x, int y) { }
    }

    public class Player : IMovable
    {
        public void Move(int x, int y) { }
    }

    public class Enemy : IMovable
    {
        public void Move(int x, int y) { }
    }

    static void Main(string[] args)
    {
        IMovable movableObject1 = new Player();
        IMovable movableObject2 = new Enemy();

        movableObject1.Move(1, 2);
        movableObject2.Move(3, 7);
    }
}
