﻿using System.ComponentModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System;
using System.ComponentModel.Design;
using System.Text;
using System.IO;
using System.Threading;
using System.Numerics;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace memoPad
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Utils utils = new Utils();
            utils.DrawStar(120, 30);
            utils.DrawStar(120, 5);
            utils.DrawStar(120, 7);

            int lineX = 24;
            int lineY = 2;

            utils.SetCursorString(3, lineY++, "[대장간]", false); //false 가 줄넘김
            utils.SetCursorString(3, lineY++, "무엇이든 만들어드립니다...", false);
            lineY += 2;
            //utils.SetCursorString(1, lineY++, "", false);
            utils.SetCursorString(1, lineY++, "제작 아이템         ||   재료이름   |필요숫자|   재료이름   |필요숫자|   재료이름   |필요숫자|   재료이름   |필요숫자", false);
            utils.SetCursorString(1, lineY++, "", false);
            int maxItemLength = 0;              //가나다라마바사아자차||가나다라마바사|12345678|
                                                //우람한 토끼의 발톱이||
            var stallArray = new List<string>();
            foreach (BlackSmith.ItemTable table in BlackSmith.GetItemCombinationTable())
            {
                var stallElementList = new List<string>();
                BlackSmith itemSmith = new BlackSmith(table.CombItem);
                maxItemLength = (maxItemLength > itemSmith.SmithItem.Name.Length) ? maxItemLength : itemSmith.SmithItem.Name.Length;
                stallElementList.Add(itemSmith.SmithItem.Name);

                var blackSmithFields = typeof(BlackSmith).GetFields();
                foreach (var field in blackSmithFields)
                {
                    var value = field.GetValue(itemSmith);

                    if (value != null && field.Name.Contains("Req"))
                    {
                        stallElementList.Add(value.ToString());
                    }
                }
                string showString = $"{utils.LimitString(stallElementList[0], 10)}||{utils.LimitString(stallElementList[1], 7)}|{utils.LimitString(stallElementList[2], 4)}|";
                for (int i = 3; i < 9; i+=2) 
                {
                    try
                    {
                        showString += $"{utils.LimitString(stallElementList[i], 7)}|{utils.LimitString(stallElementList[i + 1], 4)}|";
                    }
                    catch
                    {
                        continue;
                    }
                }
                utils.SetCursorString(1, lineY++, showString, false);
            }
            Console.WriteLine("\n\n");
            lineY += 2;
            utils.SetCursorString(1, lineY++, "", false);
            utils.SetCursorString(4, lineY++, "0. 나가기", false);
            utils.SetCursorString(4, lineY++, "1. 제작", false);
            utils.SetCursorString(4, lineY++, "2. 인벤토리", false);
        }
    }

    class Utils
    {
        public float CountString(string str)
        {
            char space = ' ';
            int spaceFreq = str.Count(f => f == space);
            int stringLength = str.Length;
            string result = Regex.Replace(str, @"[^0-9]", "");
            return ((float)stringLength - ((float)spaceFreq + result.Length) * 0.5f);
        }

        public string LimitString(string str, int limitNum)
        {
            string resultString = (str.Length > limitNum) ? str.Substring(0, 6) + ".. " : str;
            string copy = resultString;
            for (int i = 0; i < ((float)limitNum - CountString(copy)) * 2; i++)
            {
                resultString += " "; 
            }
            return resultString;
        }
        public void DrawStar(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < x; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("*");
            }
            for (int i = 0; i < y; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("*");
                Console.SetCursorPosition(x - 1, i);
                Console.Write("*");
            }
            for (int i = 0; i < x; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write("*");
            }
        }

        public void SetCursorString(int lineX, int lineY, string str, bool isNextLine)
        {
            if (isNextLine)
            {
                Console.SetCursorPosition(lineX, lineY);
                Console.Write(str);
            }
            else
            {
                Console.SetCursorPosition(lineX, lineY);
                Console.WriteLine(str);
            }
        }
    }
    internal class Item
    {

        public int EqAtk;           // 장착 무기
        public int EqDef;           // 장착 방어력
        public int EqHP;            // Hp (임시)
        public int EqMP;            // MP (임시)
        public string Name;         // 아이템 이름
        public string Info;         // 아이템 정보
        public int Type;            // 0 무기 1 방어구 2 포션
        public bool IsEquiped;   //착용유무
        public int Price;           //가격

        public static Item[] ItemInfo = GetItemInfo();

        public Item(string name)
        {
            Item item = ItemInfo.Where(it => it.Name == name).ToArray()[0];
            EqAtk = item.EqAtk;
            EqDef = item.EqDef;
            EqHP = item.EqHP;
            EqMP = item.EqMP;
            Type = item.Type;
            Price = item.Price;
            Name = item.Name;
            Info = item.Info;
            IsEquiped = false;
        }

        public Item(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info)
        {
            //Item item = (Item)ItemInfo.Where(it => it.Name == name);
            EqAtk = eqAtk;
            EqDef = eqDef;
            EqHP = eqHp;
            EqMP = eqMp;
            Type = type;
            Price = price;
            Name = name;
            Info = info;
            IsEquiped = false;
        }
        public virtual void Perfomence()
        {
        }

        public static Item[] GetItemInfo()
        {
            Item[] allItems = new Item[] { };

            string itemPath = Pathes.ItemDataPath();
            string[] itemData = File.ReadAllLines(itemPath, Encoding.UTF8);
            string[] propertyNames = itemData[0].Split(',');

            for (int itemIdx = 1; itemIdx < itemData.Length; itemIdx++)
            {
                string[] itemEach = itemData[itemIdx].Split(",");

                string name = itemEach[Array.IndexOf(propertyNames, "Name")];
                string info = itemEach[Array.IndexOf(propertyNames, "Info")];
                int type = int.Parse(itemEach[Array.IndexOf(propertyNames, "Type")]);
                int eqAtk = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqAtk")]);
                int eqDef = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqDef")]);
                int eqHP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqHp")]);
                int eqMP = int.Parse(itemEach[Array.IndexOf(propertyNames, "EqMp")]);
                int price = int.Parse(itemEach[Array.IndexOf(propertyNames, "Price")]);

                Item item = new Item(eqAtk, eqDef, eqHP, eqMP, type, price, name, info);

                Array.Resize(ref allItems, allItems.Length + 1);
                allItems[itemIdx - 1] = item;
            }

            return allItems;
        }
    }
    internal class Weapon : Item
    {
        public Weapon(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }


    }
    internal class Defense : Item
    {
        public Defense(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }


    }

    internal class Potion : Item
    {
        public Potion(int eqAtk, int eqDef, int eqHp, int eqMp, int type, int price, string name, string info) : base(eqAtk, eqDef, eqHp, eqMp, type, price, name, info) { }

    }
    internal class BlackSmith
    {
        public Item SmithItem;
        public string? Req1Name;
        public int? Req1Num;
        public string? Req2Name;
        public int? Req2Num;
        public string? Req3Name;
        public int? Req3Num;
        public string? Req4Name;
        public int? Req4Num;

        public static ItemTable[] CombinationTable = GetItemCombinationTable();
        public BlackSmith(Item item)
        {
            ItemTable matchingTable = CombinationTable.Where(it => it.CombItem.Name == item.Name).ToArray()[0];
            SmithItem = matchingTable.CombItem;
            string[] tableArray = matchingTable.Requirements; // Req1Name, Req1Num.. 부분을 배열로 한 것
            Req1Name = tableArray.Length > 0 ? tableArray[0] : null;                            // 배열의 갯수에 따라, 배열상에 존재하지 않으면 null. 존재하면 배열의 값
            Req2Name = tableArray.Length > 2 ? tableArray[2] : null;
            Req3Name = tableArray.Length > 4 ? tableArray[4] : null;
            Req4Name = tableArray.Length > 6 ? tableArray[6] : null;
            Req1Num = tableArray.Length > 1 ? int.Parse(tableArray[1]) : null;                   // 문자열 배열로 받았으므로, int로 Parse
            Req2Num = tableArray.Length > 3 ? int.Parse(tableArray[3]) : null;
            Req3Num = tableArray.Length > 5 ? int.Parse(tableArray[5]) : null;
            Req4Num = tableArray.Length > 7 ? int.Parse(tableArray[7]) : null;
        }

        public struct ItemTable
        {
            public Item CombItem;
            public string[] Requirements;

            public ItemTable(Item item, string[] requirements)
            {
                CombItem = item;
                Requirements = requirements;
            }
        }

        public static ItemTable[] GetItemCombinationTable()
        {
            ItemTable[] ItemCombinationTable = new ItemTable[] { };                   // Return할 구조체를 요소로 하는 배열 (각 아이템마다, 조합에 필요한 아이템의 종류가 달라서 채택함,)

            string fullPath = Pathes.ItemCombPath();
            string[] ItemCombTable = File.ReadAllLines(fullPath, Encoding.UTF8);     // ItemCombTable.csv의 각 줄을 요소로 하는 배열
            string[] propertyNames = ItemCombTable[0].Split(',');                    // ItemCombTable.csv의 컬럼을 배열로
            string[] ItemCombinations = ItemCombTable.Skip(1).ToArray();             // 본문(컬럼 아래의 내용)의 각 줄을 요소로 하는 배열

            for (int itemIdx = 0; itemIdx < ItemCombinations.Length; itemIdx++)
            {
                string[] EachItemComb = ItemCombinations[itemIdx].Split(",");         // ItemCombTable.csv의 ItemIdx 번째 줄을 , 로 split하여 배열로 만듦.
                string[] EachCombination = new string[] { };                          // 공백을 제외한 재료아이템 이름과 재료 아이템 수를 가진 string배열
                for (int reqIdx = 1; reqIdx < EachItemComb.Length-7; reqIdx++)
                {
                    if (EachItemComb[reqIdx] != "")       // csv 상, 재료와 개수가 적혀져 있지 않은 부분은 Pass
                    {
                        Array.Resize(ref EachCombination, EachCombination.Length+1);
                        EachCombination[EachCombination.Length - 1] = EachItemComb[reqIdx];
                    }
                }

                string name = EachItemComb[Array.IndexOf(propertyNames, "Name")];           // 컬럼배열의 idx 를 ItemCombTable.csv의 ItemIdx 번째 줄을 , 로 split하여 배열의 idx에 대입
                string info = EachItemComb[Array.IndexOf(propertyNames, "Info")];
                int type = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "Type")]);
                int eqAtk = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqAtk")]);
                int eqDef = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqDef")]);
                int eqHP = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqHP")]);
                int eqMP = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "EqMP")]);
                int price = int.Parse(EachItemComb[Array.IndexOf(propertyNames, "Price")]);

                Item tempItem = new Item(eqAtk, eqDef, eqHP, eqMP, type, price, name, info);  // csv 상의 컬럼과 값으로 Item 선언

                ItemTable tempTable = new ItemTable(tempItem, EachCombination);
                Array.Resize(ref ItemCombinationTable, ItemCombinationTable.Length + 1);
                ItemCombinationTable[itemIdx] = tempTable;
            }
            return ItemCombinationTable;
        }
        //static bool DisplaySmith(Player player)
        //{
        //    return false;
        //}
    }

    public class Pathes
    {
        public static DirectoryInfo currentPath = new DirectoryInfo(Directory.GetCurrentDirectory());
        public static string localPath = currentPath.Parent.Parent.Parent.ToString();
        //public static string localPath = Directory.GetParent(Path.GetFullPath(@"..\Data\MonsterData.csv")).Parent.Parent.Parent.Parent.ToString();
        public static string MonsterDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"MonsterData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string ItemDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"ItemData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string BossSkillDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"BossSkill.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string DropItemDataPath()
        {
            var dataPath = @"Data";
            var fileName = @"DropItemData.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }

        public static string ItemCombPath()
        {
            var dataPath = @"Data";
            var fileName = @"ItemCombTable.csv";
            var fullPath = Path.Combine(localPath, dataPath, fileName);
            return fullPath;
        }
    }
}

