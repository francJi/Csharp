namespace DungeonSparta
{
    internal class Program
    {
        public static Character player;
        public static Inventory inventory = new Inventory();
        

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            //Inventory inventory = new Inventory();
            Item supplySword = new("보급형 단검", 1, null, "복지용으로 지급하는 단검입니다.", false);
            Item supplyArmor = new("보급형 갑옷", null, 4, "갑옷까지 무료로 지급합니다. 친절하게도..", true);
            inventory.AddItem(supplySword);
            inventory.AddItem(supplyArmor);
            
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory(inventory);
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory(Inventory inventory)
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            // 인베토리 내 아이템 정보 정리한 것 출력.
            List<string> getItemInfo = inventory.GetItemInfo(inventory.ShowInventory());
            foreach (string itemInfo in getItemInfo)
            {
                bool isEquipped = itemInfo.Contains("[E]");
                if (isEquipped)
                {
                    int EquippedMarkIndex = itemInfo.IndexOf("[E]");
                    for (int strIndex = 0; strIndex < EquippedMarkIndex; strIndex++)
                    {
                        Console.Write(itemInfo[strIndex]);
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[E]");
                    Console.ResetColor();
                    for (int strIndex = EquippedMarkIndex + 3; strIndex < itemInfo.Length; strIndex++)
                    {
                        Console.Write(itemInfo[strIndex]);
                    }

                }
                else
                {
                    Console.WriteLine(itemInfo);
                }
            }
            Console.WriteLine("\n\n\n1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayEquipManagement(inventory);
                    break;

            }

        }

        static void DisplayEquipManagement(Inventory inventory)
        {

        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string ItemName { get; }
        public int? Atk { get; }
        public int? Def { get; }
        public string ItemInfo { get; }
        public bool IsEquiped { get; }
        public List<string> InventoryInfo;

        public Item(string itemName, int? atk, int? def, string itemInfo, bool isEquiped)
        {
            ItemName = itemName;
            Atk = atk;
            Def = def;
            ItemInfo = itemInfo;
            IsEquiped = isEquiped;
        }
    }

    public class Inventory
    {
        private List<Item> InventoryList { get; }

        public Inventory()
        {
            InventoryList = new List<Item>();
        }

        public IReadOnlyList<Item> Items => InventoryList;       // 리스트 inventory 에 읽기 전용 프로퍼티 부여.

        // 인벤토리에 아이템 추가하는 메서드
        public void AddItem(Item item)
        {
            InventoryList.Add(item);
        }

        // 인벤토리에 아이템 제거하는 메서드
        public void RemoveItem(Item item) 
        {
            InventoryList.Remove(item); 
        }

        // 인벤토리 리스트를 반환하는 메서드
        public List<Item> ShowInventory()
        {
            return InventoryList;
        }

        List<string> inventoryInfo = new List<string>();

        // inventory List에 있는 Item들 정보 문자열로.
        public List<string> GetItemInfo(List<Item> inventory)
        {
            foreach (Item item in InventoryList)
            {
                string itemName = item.ItemName;
                itemName = (item.IsEquiped) ? "- [E] " + itemName : "- " + itemName;

                int nameLength = itemName.Length;
                int nameVacantSize = 18;                                               // 글자 수 제한 : 18자.
                string vacant = "";
                for (int str = 0; str < nameVacantSize - nameLength; str++) { vacant += " "; }
                string itemInfo = $"{itemName}{vacant}|";

                string itemSpec = "";
                itemSpec += (item.Atk != null) ? $" 공력력 + {item.Atk} " : "";
                itemSpec += (item.Def != null) ? $" 방어력 + {item.Def} " : "";
                itemInfo += itemSpec;

                itemInfo += $"| {item.ItemInfo}";

                inventoryInfo.Add(itemInfo);                
            }
            return inventoryInfo;
        }
    }
}