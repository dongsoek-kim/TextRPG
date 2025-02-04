using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Merchant
    {
        public HashSet<int> SellItemset = new HashSet<int>();//중복을 자동으로 방시하는 HashSet
        public List<int> SellItemNum =new List<int>();
        private int[] CommenItemNum = new int[6] { 0, 2, 6, 9, 12, 15 };
        private int[] RareItemNum = new int[6] { 1, 4, 7, 10, 13, 16 };
        private int[] UniqueItemNum = new int[6] { 3, 5, 8, 11, 14, 17 };
        
        public void MakeListOfSellItemNum(Player player)
        { 
            Random random = new Random();
            int randomChance = random.Next(1, 101);
            SellItemset.Clear();
            SellItemNum.Clear();
            if (player.Level < 5)
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
            else if(player.Level < 10)
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
            SellItemNum = new List<int>(SellItemset);//리스트에 삽입
        }

        private void AddItems(int[] itemArray, int count)// 배열에서 랜덤으로 아이템 번호를 선택
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(itemArray.Length); 
                int itemNumber = itemArray[randomIndex];  
                SellItemset.Add(itemNumber);
            }
        }
        public void RemoveAcquiredItems(Player player)
        {
            // 리스트에서 아이템을 순회하며, Acquire가 true인 경우 해당 아이템을 제거
            foreach (var item in SellItemNum.ToList()) 
            {
                if (player.PlayerAcquire[item])  
                {
                    SellItemNum.Remove(item);  
                }
            }
        }
        public void transaction(Player player, int sellItemNum,int price)
        {

            player.PlayerAcquire[sellItemNum] = true;
            player.Gold-=price;
        }
    }
}
