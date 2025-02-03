using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        string Name;
        int Level { get; set; }
        string Job { get; set; }
        float AttackPower { get; set; }
        float Defense { get; set; }
        float Health { get; set; }
        int Gold { get; set; }
        public bool[] PlayerAcquire { get; set; } = new bool[20];
        public struct EquipInfo
        {
            public bool PlayerEquipSlot { get; set; } = false;
            public int PlayerEquipItemNum { get; set; } = -1;
            public EquipInfo() { }
        }

       public EquipInfo[] equipInfo = new EquipInfo[6];//장착부위: 0=head , 1=body , 2=arm , 3=leg , 4=foot,5=weapon
  
       
        public Player(string name, string job)
        {
            Name = name;
            Level = 1;
            Job = job;
            AttackPower = 10;
            Defense = 5;
            Health = 100f;
            Gold = 1000;
        }
        public void ShowPlayer(float equipmentAttakPower, float equipmentDefense)
        {
            Player_state(equipmentAttakPower, equipmentDefense);
            Console.WriteLine($"        이름: {Name}\n|       레벨: {Level}\n|      직업: {Job}\n|        공격력: {AttackPower}\n|       방어력: {Defense}\n|       체력: {Health}\n|         골드: {Gold}");
        }
        
        public void Acquire(int itemNum)
        {
            PlayerAcquire[itemNum] = true;
        }
        public void Equip(int itemNum,ItemManager item)
        {
            equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum=itemNum;
            equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot = true;
            item.items[itemNum].Equip = true;
            Console.WriteLine(equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum);
            Console.ReadKey();
        }
        public void Unequip(int itemNum, ItemManager item)
        {
            equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot = false;
            equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum = -1;
            item.items[itemNum].Equip = false;
            Console.WriteLine(equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum);
            Console.ReadKey();
        }
        public void Player_state(float equipmentAttakPower,float equipmentDefense)
        {
            AttackPower = 10f + equipmentAttakPower + (0.5f * (Level-1));
            Defense = 5f + equipmentDefense + (1f * (Level - 1));
        }

        public void EquipmentReplacement(int itemNum,int equipSlot,ItemManager item)
        {
            int prevEquip = equipInfo[equipSlot].PlayerEquipItemNum;
            Unequip(prevEquip,item);
            Equip(itemNum,item);
            equipInfo[equipSlot].PlayerEquipItemNum = itemNum;
        }
    }
}
