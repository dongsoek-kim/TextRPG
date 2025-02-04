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
        public int Level { get; set; }
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
            Console.WriteLine($"이름: {Name}\n레벨: {Level}\n직업: {Job}\n공격력: {AttackPower}\n방어력: {Defense}\n체력: {Health}\n골드: {Gold}");
        }
        
        public void Acquire(int itemNum)
        {
            PlayerAcquire[itemNum] = true;
        }
        public void Equip(int itemNum,ItemManager item)
        { 
            int equipSlot = item.items[itemNum].EquipSlot;
            equipInfo[equipSlot].PlayerEquipItemNum=itemNum;
            equipInfo[equipSlot].PlayerEquipSlot = true;
            item.items[itemNum].Equip = true;
            
        }
        public void Unequip(int itemNum, ItemManager item)
        {
            int equipSlot = item.items[itemNum].EquipSlot;
            equipInfo[equipSlot].PlayerEquipSlot = false;
            equipInfo[equipSlot].PlayerEquipItemNum = -1;
            item.items[itemNum].Equip = false;
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
