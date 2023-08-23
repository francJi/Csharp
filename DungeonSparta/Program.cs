using System.Net;
using System.Reflection.Metadata;

namespace DungeonSparta
{
    internal class Program
    {
        public static Character player;
        public static Inventory inventory = new Inventory();
        public static Shop shop = new Shop();
        

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
            Item supplySword = new("보급형 단검", 1, null, "복지용으로 지급하는 단검입니다.", false, 100);
            Item supplyArmor = new("보급형 갑옷", null, 4, "갑옷까지 무료로 지급합니다. 친절하게도..", true, 100);

            // 인벤토리 객체에 아이템 추가.
            inventory.AddItem(supplySword);
            inventory.AddItem(supplyArmor);
            //
            inventory.GetItemInfo();

            // 상점에 물품채우기
            Item sharpSword = new("날카로운 단검", 3, null, "예리해 보이는 단검입니다. 보급형 단검과 비슷해보인다면 착각입니다.", false, 600);
            Item chainArmor = new("체인 아머", null, 6,  "확실히 튼튼해 보입니다. 무겁게 보인다면 기분탓입니다.", false, 400);
            Item sampleSword = new("전시용 장검", 4, null, "상점 한구석 벽에 전시되어 있는 장검입니다. 전시용 치고는 낡은 것 같습니다.", false, 500);
            Item roundShield = new("라운드 실드", null, 5, "둥근 방패입니다. 생각보다 작습니다.", false, 500);
            Item longSword = new("롱소드", 6, null, "흔한 롱소드입니다.", false, 1400);
            shop.AddItem(sharpSword);
            shop.AddItem(chainArmor);
            shop.AddItem(sampleSword);
            shop.AddItem(roundShield);
            shop.AddItem(longSword);

        }

        // 장착 여부 색깔로
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
            }
            else
            {
                Console.Write(invenInfo);
            }
        }

        // 지정 문구 염색 메서드
        public static void ColorPrint(string word, ConsoleColor? wordColor = null, ConsoleColor? backgroundColor = null)
        {
            if (wordColor != null) { Console.ForegroundColor = wordColor.Value; }
            if (backgroundColor != null) { Console.BackgroundColor = backgroundColor.Value; }
            Console.Write(word);
            Console.ResetColor();
        }

        // 선택지 하이라이트 액션.
        static Action<string, int, int> highLight = (message, menuNum, menuPoint) =>
        {
            var goodBye = $"{message}";
            if (menuNum == menuPoint) { ColorPrint(message, ConsoleColor.Black, ConsoleColor.White); }
            else { EToYellow($"{message}"); }
        };

        // 게임 시작 화면
        static void DisplayGameIntro()
        {
            int menuIndex = 0;
            string[] menuChoices = new string[3] { "1. 상태 보기", "2. 인벤토리" , "3. 상점"};

            while (true) 
            {
                Console.Clear();

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();


                for (int choice = 0; choice < menuChoices.Count(); choice ++) 
                {
                    highLight(menuChoices[choice], choice, menuIndex);
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuIndex -= 1;
                        menuIndex = (menuIndex < 0) ? 0 : menuIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        menuIndex += 1;
                        menuIndex = (menuIndex > menuChoices.Length - 1) ? menuChoices.Length - 1 : menuIndex;
                        break;
                    case ConsoleKey.Enter:
                        switch (menuIndex)
                        {
                            case 0: 
                                DisplayMyInfo(); return;
                            case 1: 
                                DisplayInventory(inventory); return;
                            case 2:
                                DisplayShop(shop); return;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayMyInfo()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보르 표시합니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level}");
                Console.WriteLine($"{player.Name}({player.Job})");
                Console.WriteLine($"공격력 :{player.Atk + player.GetEquipedStat(inventory)[0]} (+{player.GetEquipedStat(inventory)[0]})");
                Console.WriteLine($"방어력 : {player.Def + player.GetEquipedStat(inventory)[1]} (+{player.GetEquipedStat(inventory)[1]})");
                Console.WriteLine($"체력 : {player.Hp}");
                Console.WriteLine($"Gold : {player.Gold} G");
                Console.WriteLine();
                ColorPrint("0. 나가기", ConsoleColor.Black, ConsoleColor.White);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        DisplayGameIntro(); break;
                    default:
                        break;
                }
            }
        }


        static void DisplayInventory(Inventory inventory)
        {
            int menuIndex = 0;
            string[] menuChoices = new string[2] { "1. 장착 관리", "0. 나가기" };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]\n");
                // 인베토리 내 아이템 정보 정리한 것 출력.
                List<string> itemsInfo = inventory.ResetItemInfo().ToList();
                
                foreach (string itemInfo in itemsInfo)
                {
                    EToYellow(itemInfo);
                    Console.WriteLine();
                }
                Console.WriteLine("\n\n원하시는 행동을 입력해주세요.\n\n");

                for (int choice = 0; choice < 2; choice++)
                {
                    highLight(menuChoices[choice], choice, menuIndex);
                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuIndex -= 1;
                        menuIndex = (menuIndex < 0) ? 0 : menuIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        menuIndex += 1;
                        menuIndex = (menuIndex > menuChoices.Length - 1) ? menuChoices.Length - 1 : menuIndex;
                        break;
                    case ConsoleKey.Enter:
                        switch (menuIndex)
                        {
                            case 0:
                                DisplayEquipManagement(inventory); return;
                            case 1:
                                DisplayGameIntro(); return;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        static void DisplayEquipManagement(Inventory inventory)     
        {
            int selectIndex = 0;
        

            while (true)
            {
                List<string> getItemInfo = inventory.ResetItemInfo().ToList();
                List<string> menuOptions = new() { "1. 아이템 정렬", "0. 나가기" };

                Console.Clear();

                ColorPrint("인벤토리 - 장착관리",ConsoleColor.Yellow,null);
                Console.WriteLine();
                Console.WriteLine("장착 혹은 해제하고 싶은 장비를 선택하고 enter를 누르세요.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]\n");

                for (int itemIndex = 0; itemIndex < getItemInfo.Count; itemIndex++)
                {
                    highLight(getItemInfo[itemIndex], itemIndex, selectIndex);
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[메뉴]\n");
                for (int choice = getItemInfo.Count - 1; choice < getItemInfo.Count + menuOptions.Count - 1; choice++)
                {
                    highLight(menuOptions[choice - getItemInfo.Count + 1], choice - getItemInfo.Count + 1, selectIndex - getItemInfo.Count);
                    Console.WriteLine();
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
                        selectIndex = (selectIndex > getItemInfo.Count + menuOptions.Count - 1) ? getItemInfo.Count + menuOptions.Count - 1 : selectIndex;
                        break;
                    case ConsoleKey.Enter:
                        if (selectIndex < getItemInfo.Count) { inventory.EquipItem(selectIndex); }
                        else if (selectIndex == getItemInfo.Count) { DisplayEquipArrange(inventory); return; }  // return;을 해야 무한루프에서 빠져나올 수 있음. + 객체에 변화를 가한 것을 저장할 수 있음.
                        else { DisplayInventory(inventory); return; }
                        break;
                    default:
                        break;
                }
            }
        }



        static void DisplayEquipArrange(Inventory inventory)
        {
            int selectIndex = 0;
            int menuIndex = 0;
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
            List<Item> originalList = inventoryList.ToList<Item>();                        // 리스트는 참조형식. 단순히, 같다라고 해버리면, 해당 주소를 복사해버림.
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
                    highLight(headLine[headIndex], headIndex, selectIndex);
                }
                Console.WriteLine(explanation);

                // 아이템 목록
                for (int itemIndex = 0; itemIndex < getItemInfo.Count; itemIndex++)
                {
                    EToYellow($"{itemIndex + 1}. {getItemInfo[itemIndex]}");
                    Console.WriteLine();
                }

                string outMessage = "\n - 나가기 -";
                highLight(outMessage, menuIndex, 1);
                
                // 키보드 입력
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
                    case ConsoleKey.UpArrow:
                        menuIndex = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        menuIndex = 1;
                        break;
                    case ConsoleKey.Enter:
                        // 누를 때마다 해당 선택한 컬럼 기준으로 내림차순, 오름차순, 원래대로.. 다른 컬럼을 선택시 기존 정렬 초기화
                        if (menuIndex ==1)
                        {
                            DisplayEquipManagement(inventory);
                            return;
                        }
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
                                    equipped = equipped.Replace(markArray[(clickCount - 1) % 3] + " |", markArray[clickCount % 3] + " |");  
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

        static void DisplayShop(Shop shop)
        {
            int shopIndex = 0;
   
            List<Item> shopList = shop.ShowInventory().ToList();
            

            while (true)
            {
                Console.Clear();

                List<string> menuChoice = new List<string> { "구매 화면", "아이템 판매", "나가기" };
                List<string> shopItemInfo = new List<string>();
                shopItemInfo = shop.GetItemInfo();

                ColorPrint("상점", ConsoleColor.Yellow, null);
                Console.WriteLine();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                ColorPrint("[보유 골드]", ConsoleColor.Yellow, null);
                Console.WriteLine();
                int leftGold = player.Gold;
                string goldExpression = $"{leftGold} G";
                Console.WriteLine(goldExpression);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int itemIndex = 0; itemIndex < shopItemInfo.Count(); itemIndex++) { Console.WriteLine(shopItemInfo[itemIndex].ToString()); }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");

                for (int choice = 0; choice < menuChoice.Count; choice++) 
                {
                    highLight(menuChoice[choice], choice, shopIndex);
                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        shopIndex -= 1;
                        shopIndex = (shopIndex < 0) ? 0 : shopIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        shopIndex += 1;
                        shopIndex = (shopIndex > menuChoice.Count - 1) ? menuChoice.Count - 1 : shopIndex;
                        break;
                    case ConsoleKey.Enter:
                        switch (shopIndex)
                        {
                            case 0:
                                DisplayBuying(); return;
                            case 1:
                                DisplaySelling(); return;
                            case 2:
                                DisplayGameIntro(); return;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayBuying()
        {
            int shopIndex = 0;

            while (true)
            {
                Console.Clear();

                List<string> menuChoice = new List<string> { "아이템 판매", "나가기" };

                List<string> shopItemInfo = shop.GetItemInfo();
                List<Item> shopList = shop.ShowInventory().ToList();

                ColorPrint("상점 - 구매", ConsoleColor.Yellow, null);
                Console.WriteLine();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                ColorPrint("[보유 골드]", ConsoleColor.Yellow, null);
                Console.WriteLine();
                int leftGold = player.Gold;
                string goldExpression = $"{leftGold} G";
                Console.WriteLine(goldExpression);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine("구매하고 싶은 아이템을 선택해주세요.");
                Console.WriteLine(">>");



                for (int choice = 0; choice < shopItemInfo.Count(); choice++)
                {
                    highLight($"{(choice + 1).ToString()}. {shopItemInfo[choice]}", choice, shopIndex);
                    Console.WriteLine();
                }
                
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 선택해주세요.");
                Console.WriteLine(">>");
                
                for (int choice = shopItemInfo.Count() - 1; choice < shopItemInfo.Count() + menuChoice.Count() - 1; choice++)
                {
                    int additionalChoice = choice - shopItemInfo.Count() + 1;
                    highLight(menuChoice[additionalChoice], additionalChoice, shopIndex-shopItemInfo.Count());
                    Console.WriteLine();
                }
                

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        shopIndex -= 1;
                        shopIndex = (shopIndex < 0) ? 0 : shopIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        shopIndex += 1;
                        shopIndex = (shopIndex > shopItemInfo.Count() + menuChoice.Count() - 1) ? shopItemInfo.Count() + menuChoice.Count() - 1 : shopIndex;
                        break;
                    case ConsoleKey.Enter:
                        if (shopIndex < shopItemInfo.Count())
                        {
                            if (shopList[shopIndex].isSelled)
                            {
                                ColorPrint("\n\n !!!! 이미 구매한 아이템입니다.", ConsoleColor.Red, null); 
                                Thread.Sleep(500);
                                break;
                            }
                            Calculator merchant = new Calculator(player, shopList[shopIndex], inventory);
                            merchant.OnTransaction += player.HandleBuyGold;
                            merchant.OnTransaction += inventory.HandlePutBuyed;
                            merchant.OnTransaction += shop.HandleStock;
                            merchant.AttemptBuying(shopList[shopIndex]);
                        }
                        else if (shopIndex == shopItemInfo.Count())
                        {
                            DisplaySelling(); return;
                        }
                        else if (shopIndex == shopItemInfo.Count() + 1)
                        {
                            DisplayShop(shop); return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplaySelling()
        {
            int shopIndex = 0;

            while (true)
            {
                Console.Clear();

                List<string> menuChoice = new List<string> { "아이템 구매", "나가기" };

                List<string> invenItemInfo = inventory.ResetItemInfo();
                List<Item> invenList = inventory.ShowInventory().ToList();

                ColorPrint("상점 - 판매", ConsoleColor.Yellow, null);
                Console.WriteLine();
                Console.WriteLine("언제나 최고가로 매입하고 있는 친절한 상점이라고 합니다.");
                Console.WriteLine();
                ColorPrint("[보유 골드]", ConsoleColor.Yellow, null);
                Console.WriteLine();
                int leftGold = player.Gold;
                string goldExpression = $"{leftGold} G";
                Console.WriteLine(goldExpression);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine("판매하고 싶은 아이템을 선택해주세요.");
                Console.WriteLine(">>");



                for (int choice = 0; choice < invenItemInfo.Count(); choice++)
                {
                    highLight($"{(choice + 1).ToString()}. {invenItemInfo[choice]}", choice, shopIndex);
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 선택해주세요.");
                Console.WriteLine(">>");

                for (int choice = invenItemInfo.Count() - 1; choice < invenItemInfo.Count() + menuChoice.Count() - 1; choice++)
                {
                    int additionalChoice = choice - invenItemInfo.Count() + 1;
                    highLight(menuChoice[additionalChoice], additionalChoice, shopIndex - invenItemInfo.Count());
                    Console.WriteLine();
                }


                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        shopIndex -= 1;
                        shopIndex = (shopIndex < 0) ? 0 : shopIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        shopIndex += 1;
                        shopIndex = (shopIndex > invenItemInfo.Count() + menuChoice.Count() - 1) ? invenItemInfo.Count() + menuChoice.Count() - 1 : shopIndex;
                        break;
                    case ConsoleKey.Enter:
                        if (shopIndex < invenItemInfo.Count())
                        {
                            Calculator merchant = new Calculator(player, invenList[shopIndex], inventory);
                            merchant.OnTransaction += player.HandleSellGold;
                            merchant.OnTransaction += inventory.HandleSelled;
                            merchant.Selling(invenList[shopIndex]);
                            
                        }
                        else if (shopIndex == invenList.Count())
                        {
                            DisplayBuying(); return;
                        }
                        else if (shopIndex == invenList.Count() + 1)
                        {
                            DisplayShop(shop); return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // 아이템 정렬시, 공격력, 방어력 오름차순 구간에서 Null 값이 먼저 나오는 현상 배제
    public class NullComparer<T> : IComparer<T>              // 내장 인터페이스
    {
        public int Compare(T x, T y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            return Comparer<T>.Default.Compare(x, y);
        }
    }

    public delegate bool ItemHandler(Item item);

    public class Calculator
    {
        public int Gold { get; set; }
        public Item TargetItem { get; set; }
        //public List<Item> ShoppingList { get; set; }
        public List<Item> inventoryList { get; set; }

        // 판매 & 구매 이벤트 발행자
        public event ItemHandler OnTransaction;

        public Calculator(Character character, Item item, Inventory inventory)
        {
            Gold = character.Gold;
            TargetItem = item;
        }

        // 구매 이벤트 구독자
        public bool AttemptBuying(Item item)
        {
            if (Gold > item.Price)
            {
                Gold -= item.Price;
                OnTransaction?.Invoke(item);
                return true;
            }
            else { Program.ColorPrint("!!!!!!! 골드가 부족합니다", ConsoleColor.Red, null); Thread.Sleep(500); return false; }
        }

        // 판매 이벤트 구독자
        public bool Selling(Item item)
        {
            Gold += (int)(item.Price * 0.85);
            OnTransaction?.Invoke(item);
            Program.ColorPrint($"{item.ItemName}을 {(int)(item.Price * 0.85)} G 에 판매하셨습니다.");
            return true;
        }
    }

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; }
        
        public int Gold { get; set; }


        public bool HandleBuyGold(Item item)
        {
            Gold -= item.Price;
            return true;
        }
        public bool HandleSellGold(Item item)
        {
            Gold += (int)(item.Price * 0.85);
            return true;
        }

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

        // 장비 장착시, 스탯 적용 메서드
        public List<int> GetEquipedStat(Inventory inventory)
        {
            List<Item> getEquipedList = inventory.EquipmentList.ToList();
            int sumAtk = getEquipedList.Select(item => item.Atk).Where(atk => atk.HasValue).Select(atk => atk.Value).ToList().Sum();
            int sumDef = getEquipedList.Select(item => item.Def).Where(def => def.HasValue).Select(def => def.Value).ToList().Sum();
            
            List<int> statList = new List<int>() {sumAtk, sumDef };

            return statList;
        }
    }

    public class Item
    {
        public string ItemName { get; }
        public int? Atk { get; }
        public int? Def { get; }
        public string ItemInfo { get; }
        public bool IsEquiped { get; set; }
        public int Price { get; }
        public bool isSelled { get; set; }

        public List<string> InventoryInfo;

        public Item(string itemName, int? atk, int? def, string itemInfo, bool isEquiped, int price)
        {
            ItemName = itemName;
            Atk = atk;
            Def = def;
            ItemInfo = itemInfo;
            IsEquiped = isEquiped;
            Price = price;
            isSelled = false;
        }
    }

    public class Inventory
    {
        protected List<Item> InventoryList { get; set; }
        private List<Item> equipmentList;

        public event Action<List<Item>> InventoryChanged;
        public List<Item> EquipmentList
        {
            get 
            { return InventoryList.Where(item => item.IsEquiped).ToList(); }
            set
            { ResetEquip(); }
        }


        public Inventory()
        {
            InventoryList = new List<Item>();
        }

        // 구매 이벤트 구독자
        public bool HandlePutBuyed(Item item)
        {
            InventoryList.Add(item);
            return true;
        }

        // 판매 이벤트 구독자
        public bool HandleSelled(Item item)
        {
            InventoryList.Remove(item);
            return true;
        }

        // 아이템을 기준에 따라 정렬하는 메서드
        public List<Item> ChangeOrder<T>(List<Item> inventoryList, Func<Item, T> itemField, int clickCount)
        {
            if (clickCount % 3 == 1)
            {
                return inventoryList.OrderByDescending(itemField).ToList();
            }
            else if (clickCount % 3 == 2)
            {
                return inventoryList.OrderBy(item => itemField(item), new NullComparer<T>()).ToList();
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
            ResetEquip();
        }

        public List<Item> ResetEquip()
        {
            //equipmentList.Clear();
            List<Item> newEquipmentList = new List<Item>();
            newEquipmentList = InventoryList.Where(item => item.IsEquiped).ToList();
            return newEquipmentList;
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

       
        // 아이템 출력 유틸
        public Func<string, string, int, bool, string> formatInfo = (element, vacant, elementVacantSize, isEnd) =>
        {
            vacant = "";
            int elementLength = element.Length;
            for (int str = 0; str < elementVacantSize - elementLength; str++) { vacant += " "; }
            if (isEnd) { element = $"{element}{vacant}"; }
            else { element = $"{element}{vacant}|"; }
  
            return element;
        };

        // inventory List에 있는 Item들 정보 문자열로 추가
        public virtual List<string> GetItemInfo()
        {
            foreach (Item item in InventoryList)
            {
                string itemInfo = formatInfo(item.ItemName, "", 18, false);

                itemInfo += (item.Atk != null) ? formatInfo($" 공력력 + {item.Atk} ", "", 12, false) : "";
                itemInfo += (item.Def != null) ? formatInfo($" 방어력 + {item.Def} ", "", 12, false) : "";

                itemInfo += formatInfo(item.ItemInfo, "", 55, true);

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

    public class Shop : Inventory
    {
        private List<Item> ShopList { get; }
        
        public bool HandleStock(Item stock)
        {
            int stockIndex = ShopList.FindIndex(item => item.ItemName == stock.ItemName);
            ShopList[stockIndex].isSelled = true;
            return true;
        }

        public Shop()
        {
            ShopList = new List<Item>();
            InventoryList = ShopList;                                                  // 상속시, 보호수준 체크.
        }

        List<string> shopInfo = new List<string>();
        public List<string> CleanInfo(List<string> shopInfo)
        {
            List<string> cleanList = new List<string>();
            shopInfo = cleanList.ToList();
            return shopInfo;
        }
        public override List<string> GetItemInfo()
        {
            shopInfo = CleanInfo(shopInfo);
            foreach (Item item in ShopList)
            {
                string itemInfo = formatInfo(item.ItemName, "", 18, false);

                itemInfo += (item.Atk != null) ? formatInfo($" 공력력 + {item.Atk} ", "", 12, false) : "";
                itemInfo += (item.Def != null) ? formatInfo($" 방어력 + {item.Def} ", "", 12, false) : "";

                itemInfo += formatInfo(item.ItemInfo, "", 55, false);

                if (item.isSelled == false) { itemInfo += $" {item.Price} G "; }
                else { itemInfo += " 구매완료"; }


                shopInfo.Add(itemInfo);
            }
            return shopInfo;
        }
    }
}