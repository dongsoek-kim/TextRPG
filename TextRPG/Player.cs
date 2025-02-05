using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        public string Name;
        public int Level { get; set; }
        public string Job { get; set; }
        public float AttackPower { get; set; }
        public float Defense { get; set; }
        public float Health { get; set; }
        public int Gold { get; set; }
        public int EquipAttackPower { get; set; }
        public int EquipDefense { get; set; }
        public bool[] PlayerAcquire { get; set; } = new bool[20];
        public int RequiredExp { get; set; }
        public int CurrentExp { get; set; }


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
            Gold = 10000;
            EquipAttackPower = 0;
            EquipDefense = 0;
            RequiredExp = 1;
            CurrentExp = 0;
        }
        public void InitializeEquipInfo()
        {
            for (int i = 0; i < equipInfo.Length; i++)
            {
                equipInfo[i] = new EquipInfo { PlayerEquipSlot = false, PlayerEquipItemNum = -1 };
            }
        }
        public string GetExp(int getExp)
        {
            CurrentExp += getExp;
            if (CurrentExp >= RequiredExp)
            {
                Level += 1;
                CurrentExp = 0;
                RequiredExp = 1 + (Level * (2 - 1));
                AttackPower += 0.5f;
                Defense += 1;
                return "LevelUP";
            }
            else 
            {
                return "Clear";
            }
        }
        public void GetGold(int getgold)
        {
            Gold += getgold;
        }
        public void Death()
        {

        }
        public int HaveItemNumber()
        {
            int haveItemNumber=0; 
            foreach(bool playerAcquire in PlayerAcquire)
            {
                if (playerAcquire)
                {
                    haveItemNumber++;
                }
            }
            return haveItemNumber;
        }
    }
       
}
