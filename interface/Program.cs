namespace interfaceEx;

internal class Program
{
    static void Main(string[] args)
    {
        int[] stack = new int[2];
        stack[0] = 1;
        stack[1] = 2;

        List<T> heap = new List<T>();
        heap[0] = 1;         // !! System.ArgumentOutOfRangeException :
                             // Add 메서드를 통해 리스트에 값을 추가하고, 인덱스를 사용하여 값을 액세스 함.
        heap.Add(1);         // 1을 박싱
        Console.WriteLine(heap.GetType().Name);
    }

    /*
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
    */
    // 아이템을 사용할 수 있는 인터페이스
    // 아이템을 사용할 수 있는 인터페이스

    /*
    public interface IUsable
    {
        void Use();
    }

    // 아이템 클래스
    public class PotionItem : IUsable
    {
        public string Name { get; set; }

        public void Use()
        {
            Console.WriteLine("아이템 {0}을 사용했습니다.", Name);
            // 아이템의 남은 갯수 감소 로직
        }
    }

    public class BuffItem : IUsable
    {
        public string Name { get; set; }

        public void Use()
        {
            Console.WriteLine("버프 {0}을 사용했습니다", Name);
            // 아이템 개수 줄어들지 않음
        }
    }

    // 플레이어 클래스
    public class Player
    {
        public void UseItem(IUsable item)
        {
            item.Use();
        }
    }

    // 게임 실행
    static void Main()
    {
        Player player = new Player();
        PotionItem item = new PotionItem { Name = "Health Potion" };
        BuffItem buff = new BuffItem { Name = "attack increase" };
        player.UseItem(item);
        player.UseItem(buff);
    }

    */
    /*

    public interface IItemPickable
    {
        void PickUp();
    }

    public interface IDroppable
    {
        void Drop();
    }

    public class Item : IItemPickable, IDroppable         // 2개의 인터페이스 상속
    {
        public string Name { get; set; }

        public void PickUp()                              // IItemPickable 구현
        {
            Console.WriteLine("아이템 {0}을 주웠습니다.", Name);
        }

        public void Drop()                                // IDroppable 구현
        {
            Console.WriteLine("아이템 {0}을 버렸습니다.", Name);
        }
    }

    public class Player
    {

        public void InteractWithItem(IItemPickable item)
        {
            item.PickUp();
        }

        public void DropItem(IDroppable item)
        {
            item.Drop();
        }
    }

    static void Main(string[] args)
    {
        Player player = new Player();
        Item item = new Item { Name = "Sword" };

        player.InteractWithItem(item);
        player.DropItem(item);
    }
    */
}
