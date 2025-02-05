using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Merchant
    {
        public List<int> SellItemNum =new List<int>();
        private int[] CommenItemNum = new int[6] { 0, 2, 6, 9, 12, 15 };
        private int[] RareItemNum = new int[6] { 1, 4, 7, 10, 13, 16 };
        private int[] UniqueItemNum = new int[6] { 3, 5, 8, 11, 14, 17 };
        
        public void MakeListOfSellItemNum(Player player)
        { 
            Random random = new Random();
            int randomChance = random.Next(1, 101);
            SellItemNum.Clear();
            if (player.Level < 3)
            { 
                if (randomChance <= 70)
                {
                    AddItems(CommenItemNum, 4);  
                    AddItems(RareItemNum, 3);    
                }
                else if (randomChance <= 90)
                {
                    AddItems(CommenItemNum, 3);  
                    AddItems(RareItemNum, 3);    
                    AddItems(UniqueItemNum, 1); 
                }
                else if (randomChance <= 99)
                {
                    AddItems(CommenItemNum, 2);  
                    AddItems(RareItemNum, 3);   
                    AddItems(UniqueItemNum, 2); 
                }
                else 
                {
                    AddItems(RareItemNum, 4);  
                    AddItems(UniqueItemNum, 3);  
                }
            }
            else if(player.Level < 5)
            {
                if (randomChance <= 70)
                {
                    AddItems(CommenItemNum, 2);
                    AddItems(RareItemNum, 3);
                    AddItems(UniqueItemNum, 2);
                }
                else if (randomChance <= 90)
                {
                    AddItems(CommenItemNum, 1);
                    AddItems(RareItemNum, 3);
                    AddItems(UniqueItemNum, 3);
                }
                else 
                {
                    AddItems(RareItemNum, 3);
                    AddItems(UniqueItemNum, 4);
                }
            }
            else 
            {
                if (randomChance <= 70)
                {
                    AddItems(CommenItemNum, 1);
                    AddItems(RareItemNum, 3);
                    AddItems(UniqueItemNum, 3);
                }
                else if (randomChance <= 90)
                {
                    AddItems(CommenItemNum, 1);
                    AddItems(RareItemNum, 2);
                    AddItems(UniqueItemNum, 4);
                }
                else
                {
                    AddItems(CommenItemNum, 1);
                    AddItems(RareItemNum, 1);
                    AddItems(UniqueItemNum, 5);
                }
            }
            RemoveAcquiredItems(player);//플레이어가 소유한 아이템 목록에서 제거
        }

        private void AddItems(int[] itemArray, int count)// 배열에서 랜덤으로 아이템 번호를 선택
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(itemArray.Length);
                if (!SellItemNum.Contains(itemArray[randomIndex])) // 중복 체크
                {
                    SellItemNum.Add(itemArray[randomIndex]);
                }
            }

        }
        public void RemoveAcquiredItems(Player player)
        {
            for (int i = SellItemNum.Count - 1; i >= 0; i--)
            {
                if (player.PlayerAcquire[SellItemNum[i]])  // 해당 아이템이 획득되었으면
                {
                    SellItemNum.RemoveAt(i);  // 해당 인덱스의 아이템을 제거
                }
            }

        }
        public void Purchase(Player player,Inventory inventory,ItemManager item, int itemNum)
        {
            if (itemNum == player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum)//장착중이라면
            {
                inventory.Unequip(player, item, itemNum);
            }
            player.PlayerAcquire[itemNum] = false;
            player.Gold += (int)(item.items[itemNum].Price * 0.85f);

        }

        public void transaction(Player player, int sellItemNum,int price)
        {

            player.PlayerAcquire[sellItemNum] = true;
            player.Gold-=price;
        }
    }
}
