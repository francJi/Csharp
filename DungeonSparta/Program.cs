using System.Net;

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
            // 인벤토리 리스트에 아이템 추가.
            inventory.GetItemInfo();

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

        static void EToYellow(string itemInfo)
        {
            bool isEquipped = itemInfo.Contains("[E]");
            string invenInfo = "- " + itemInfo;

            if (isEquipped)
            {
                int EquippedMarkIndex = invenInfo.IndexOf("[E]");
                for (int strIndex = 0; strIndex < EquippedMarkIndex; strIndex++)
                {
                    Console.Write(invenInfo[strIndex]);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[E]");
                Console.ResetColor();
                for (int strIndex = EquippedMarkIndex + 3; strIndex < invenInfo.Length; strIndex++)
                {
                    Console.Write(invenInfo[strIndex]);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(invenInfo);
            }
        }

        static void ColorPrint(string word, ConsoleColor? wordColor = null, ConsoleColor? backgroundColor = null)
        {
            if (wordColor != null) { Console.ForegroundColor = wordColor.Value; }
            if (backgroundColor != null) { Console.BackgroundColor = backgroundColor.Value; }
            Console.Write(word);
            Console.ResetColor();
        }

        static void DisplayInventory(Inventory inventory)
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]\n");
            // 인베토리 내 아이템 정보 정리한 것 출력.
            List<string> itemsInfo = inventory.ShowItemInfo();
            foreach (string itemInfo in itemsInfo)
            {
                EToYellow(itemInfo);
            }
            Console.WriteLine("\n\n원하시는 행동을 입력해주세요.");
            Console.WriteLine("\n\n1. 장착 관리");
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
            int selectIndex = 0;
        

            while (true)
            {
                List<string> getItemInfo = inventory.ResetItemInfo();
                List<string> menuOptions = new() { "1. 아이템 정렬", "0. 나가기" };
                foreach (string menuOption in menuOptions)
                {
                    getItemInfo.Add(menuOption);
                }

                Console.Clear();

                ColorPrint("인벤토리 - 장착관리",ConsoleColor.Yellow,null);
                Console.WriteLine();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]\n");
                
                
                for (int itemIndex = 0; itemIndex < getItemInfo.Count; itemIndex++)
                {
                    if (itemIndex < getItemInfo.Count - 2)
                    {
                        if (itemIndex == selectIndex) 
                        { 
                            ColorPrint($"{itemIndex + 1}. {getItemInfo[itemIndex]}", ConsoleColor.Black, ConsoleColor.White);
                            Console.WriteLine();
                        }
                        else { EToYellow($"{itemIndex + 1}. {getItemInfo[itemIndex]}"); }
                    }
                    else if (itemIndex == getItemInfo.Count - 2)
                    {
                        Console.ResetColor();
                        Console.WriteLine("\n\n원하시는 행동을 입력해주세요.\n\n");
                        if (itemIndex == selectIndex)
                        {
                            ColorPrint($"1. 아이템 정렬", ConsoleColor.Black, ConsoleColor.White);
                            Console.WriteLine();
                        }
                        else { Console.WriteLine("1. 아이템 정렬"); }
                    }
                    else
                    {
                        if (itemIndex == selectIndex)
                        {
                            ColorPrint($"0. 나가기", ConsoleColor.Black, ConsoleColor.White);
                            Console.WriteLine();
                        }
                        else { Console.WriteLine("0. 나가기"); }
                        
                    }
                    Console.ResetColor();
                }
                
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectIndex -= 1;
                        selectIndex = (selectIndex < 0) ? 0 : selectIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        selectIndex += 1;
                        selectIndex = (selectIndex > getItemInfo.Count - 1) ? getItemInfo.Count - 1 : selectIndex;
                        break;
                    case ConsoleKey.Enter:
                        if (selectIndex < getItemInfo.Count - 2) { inventory.EquipItem(selectIndex); }
                        else if (selectIndex == getItemInfo.Count - 2) { DisplayEquipArrange(inventory); return; }  // return;을 해야 무한루프에서 빠져나올 수 있음.
                        else { DisplayInventory(inventory); }
                        break;
                    default:
                        break;
                }
            }
        }



        static void DisplayEquipArrange(Inventory inventory)
        {
            int selectIndex = 0;
            string equipped = "장착여부   | ";
            string arrangeName = "아이템 명   | ";
            string attackName = "공격력   | ";
            string defendName = "방어력   | ";
            string explanation = "            설명                ";

            string originalMark = " ";
            string lowerMark = "▼";
            string upperMark = "▲";
            string[] markArray = new string[3] { originalMark, lowerMark, upperMark };
         
            List<Item> inventoryList = inventory.ShowInventory();
            List<Item> originalList = inventoryList.ToList<Item>();                        // 리스트는 참조형식. 이렇게 같다라고 해버리면, 해당 주소를 복사해버림.
            List<string> itemInfoList = inventory.ShowItemInfo();

            int clickCount = 0;
            int prevSelectIndex = 0;

            while (true)
            {
                List<string> getItemInfo = inventory.ResetItemInfo();
                List<string> headLine = new List<string> { equipped, arrangeName, attackName, defendName };
                List<Item> handleHeadLine = new List<Item> { };

                Console.Clear();

                ColorPrint("인벤토리 - 아이템정렬", ConsoleColor.Yellow, null);
                Console.WriteLine();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                
                Console.WriteLine("[아이템 목록]");
                // 테이블 컬럼
                
                for (int headIndex = 0; headIndex < headLine.Count(); headIndex++)
                {
                    if (selectIndex == headIndex)
                    {
                        ColorPrint(headLine[headIndex], ConsoleColor.Black, ConsoleColor.White);
                    }
                    else
                    {
                        Console.Write(headLine[headIndex]);
                    }
                }
                Console.WriteLine(explanation);

                // 아이템 목록
                for (int itemIndex = 0; itemIndex < getItemInfo.Count; itemIndex++)
                {
                    EToYellow($"{itemIndex + 1}. {getItemInfo[itemIndex]}");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        selectIndex -= 1;
                        selectIndex = (selectIndex < 0) ? 0 : selectIndex;
                        break;
                    case ConsoleKey.RightArrow:
                        selectIndex += 1;
                        selectIndex = (selectIndex > headLine.Count - 1) ? headLine.Count - 1 : selectIndex;
                        break;
                    case ConsoleKey.Enter:
                        // 누를 때마다 해당 선택한 컬럼 기준으로 내림차순, 오름차순, 원래대로
                        if (prevSelectIndex != selectIndex)
                        {
                            equipped = "장착여부   | ";
                            arrangeName = "아이템 명   | ";
                            attackName = "공격력   | ";
                            defendName = "방어력   | ";
                            clickCount = 0;
                        }
                        clickCount++;
                        string headElement = headLine[selectIndex];
                        int wordLength = headElement.Count();
                        string serialMark = markArray[clickCount % 3];

                        if (clickCount % 3 == 0)
                        {
                            equipped = "장착여부   | ";
                            arrangeName = "아이템 명   | ";
                            attackName = "공격력   | ";
                            defendName = "방어력   | ";
                            inventory.GetThisListToInventory(originalList);
                            inventoryList = originalList.ToList<Item>();
                        }
                        else
                        {
                            switch (selectIndex)
                            {
                                case 0:
                                    equipped = equipped.Replace(markArray[(clickCount - 1) % 3] + " |", markArray[clickCount % 3] + " |");  // 다른 이름으로 변경시, 초기화 해주는 기능 추가해야함.
                                    inventoryList = inventory.ChangeOrder(inventoryList, item => item.IsEquiped, clickCount).ToList();                                                                                                      // 아이템
                                    break;
                                case 1:
                                    arrangeName = arrangeName.Replace(markArray[(clickCount - 1) % 3] + " |", markArray[clickCount % 3] + " |");
                                    inventoryList = inventory.ChangeOrder(inventoryList, item => item.ItemName, clickCount).ToList();
                                    break;
                                case 2:
                                    inventoryList = inventory.ChangeOrder(inventoryList, item => item.Atk, clickCount).ToList();
                                    attackName = attackName.Replace(markArray[(clickCount - 1) % 3] + " |", markArray[clickCount % 3] + " |");
                                    break;
                                case 3:
                                    defendName = defendName.Replace(markArray[(clickCount - 1) % 3] + " |", markArray[clickCount % 3] + " |");
                                    inventoryList = inventory.ChangeOrder(inventoryList, item => item.Def, clickCount).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        inventory.GetThisListToInventory(inventoryList);
                        prevSelectIndex = selectIndex;
                        break;
                    default:
                        break;
                }
                
            }
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

        public static List<Item> OrderByProperty<T>(List<Item> itemList, Func<Item, T> propertySelector, bool descending)
        {
            if (descending)
            {
                return itemList.OrderByDescending(propertySelector).ToList();
            }
            else
            {
                return itemList.OrderBy(propertySelector).ToList();
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
        public bool IsEquiped { get; set; }
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
        private List<Item> equipmentList;
        public List<Item> EquipmentList
        {
            get { return equipmentList; }
            set 
            {
                foreach (Item item in InventoryList)
                {
                    if (item.IsEquiped)
                    {
                        equipmentList.Add(item);
                    }
                    else
                    {
                        equipmentList.Remove(item);
                    }
                }
            }
        }


        public Inventory()
        {
            InventoryList = new List<Item>();
        }

        public List<Item> ChangeOrder<T>(List<Item> inventoryList, Func<Item, T> itemField, int clickCount)
        {
            if (clickCount % 3 == 1)
            {
                return inventoryList.OrderByDescending(itemField).ToList();
            }
            else if (clickCount % 3 == 2)
            {
                return inventoryList.OrderBy(itemField).ToList();
            }
            else { return inventoryList; }   
        }

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

        // 아이템 미장착시 장착, 장착시 해제하는 메서드.
        public void EquipItem(int itemIndex) 
        {
            InventoryList[itemIndex].IsEquiped = (InventoryList[itemIndex].IsEquiped) ? false : true;
        }

        // 인벤토리 리스트를 반환하는 메서드
        public List<Item> ShowInventory()
        {
            return InventoryList;
        }

        // 인벤토리 리스트를 최신화하는 메서드
        public void GetThisListToInventory(List<Item> newInventoryList)
        {
            InventoryList.Clear();
            foreach (Item newItem in newInventoryList)
            {
                InventoryList.Add(newItem);
            }
        }

        List<string> inventoryInfo = new List<string>();

        // inventory List에 있는 Item들 정보 문자열로 추가
        public List<string> GetItemInfo()
        {
            foreach (Item item in InventoryList)
            {
                string itemName = item.ItemName;
                itemName = (item.IsEquiped) ? "[E] " + itemName : itemName;

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
        //  item 정보 최신화
        public List<string> ResetItemInfo()
        {
            inventoryInfo.Clear();
            inventoryInfo = GetItemInfo();
            return inventoryInfo;

        }
        // 아이템 정보 리스트
        public List<string> ShowItemInfo()
        {
            return inventoryInfo;
        }
    }
}