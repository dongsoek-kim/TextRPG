using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Inventory
    {
        public List<int>OwnItem= new List<int>();
        public void Acquire(Player player, int itemNum)
        {
            player.PlayerAcquire[itemNum] = true;
        }
        public void Equip(Player player, ItemManager item,int itemNum)
        {
            int equipSlot = item.items[itemNum].EquipSlot;
            player.equipInfo[equipSlot].PlayerEquipItemNum = itemNum;
            player.equipInfo[equipSlot].PlayerEquipSlot = true;
            if (item.items[itemNum] is Armor armor)
            {
                player.EquipDefense += armor.Defense;
            }
            else if(item.items[itemNum] is Weapon weapon)
            {
                player.EquipAttackPower += weapon.AttackPower;
            }
                

        }
        public void Unequip(Player player,ItemManager item, int itemNum)
        {
            int equipSlot = item.items[itemNum].EquipSlot;
            player.equipInfo[equipSlot].PlayerEquipSlot = false;
            player.equipInfo[equipSlot].PlayerEquipItemNum = -1;
            if (item.items[itemNum] is Armor armor)
            {
                player.EquipDefense -= armor.Defense;
            }
            else if (item.items[itemNum] is Weapon weapon)
            {
                player.EquipAttackPower -= weapon.AttackPower;
            }
        }

        public void EquipmentReplacement(int itemNum, int equipSlot, ItemManager item, Player player)
        {
            int prevEquip = player.equipInfo[equipSlot].PlayerEquipItemNum;
            Unequip(player, item,prevEquip);
            Equip(player, item, itemNum);
            player.equipInfo[equipSlot].PlayerEquipItemNum = itemNum;
        }
        public void Equipmentprocedures(Player player, ItemManager item, int listnum)
        {
            if (listnum > OwnItem.Count||listnum<=0)
            {
                Draw.SetCursor_down(1);
                Console.WriteLine("범위에서 벗어났습니다");
            }
            else
            {   listnum--;
                int itemNUM = OwnItem[listnum];
                Console.WriteLine(item.items[OwnItem[listnum]].Name);
                if (itemNUM == player.equipInfo[item.items[itemNUM].EquipSlot].PlayerEquipItemNum)//장비하고있다면 해제
                { 
                    Console.WriteLine("장비중입니다");
                  Unequip(player, item, itemNUM);
                }
                else if (player.equipInfo[item.items[itemNUM].EquipSlot].PlayerEquipSlot)//장비슬롯이 비어있지않다면 교체
                { 
                    Console.WriteLine("슬롯에 다른아이템이 있습니다.");
                    EquipmentReplacement(itemNUM, item.items[itemNUM].EquipSlot, item, player);
                }
                else//둘다 아니라면 장비
                {
                    Console.WriteLine("비어있습니다.");
                    Equip(player, item, itemNUM);
                    player.equipInfo[item.items[itemNUM].EquipSlot].PlayerEquipSlot = true;
                    player.equipInfo[item.items[itemNUM].EquipSlot].PlayerEquipItemNum = itemNUM;
                }
            }
        }
        public void MakeOwnList(Player player)
        {
            int inListItemNum = 0;
            OwnItem.Clear();
            foreach (bool plyaerAcquire in player.PlayerAcquire)
            {
                if (plyaerAcquire)
                {
                    OwnItem.Add(inListItemNum);
                }
                inListItemNum++;
            }
        }
    }
}

