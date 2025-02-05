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
                IMaiiInterface.SetCursor_down(1);
                Console.WriteLine("범위에서 벗어났습니다");
            }
            else
            {   listnum--;
                int itemNum = OwnItem[listnum];
                Console.WriteLine(item.items[OwnItem[listnum]].Name);
                if (itemNum == player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum)//장비하고있다면 해제
                { 
                  Unequip(player, item, itemNum);
                }
                else if (player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot)//장비슬롯이 비어있지않다면 교체
                { 
                    EquipmentReplacement(itemNum, item.items[itemNum].EquipSlot, item, player);
                }
                else//둘다 아니라면 장비
                {
                    Equip(player, item, itemNum);
                    player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot = true;
                    player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum = itemNum;
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

