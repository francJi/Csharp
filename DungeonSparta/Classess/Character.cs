using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonSparta.Classess
{
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

            List<int> statList = new List<int>() { sumAtk, sumDef };

            return statList;
        }
    }
}
