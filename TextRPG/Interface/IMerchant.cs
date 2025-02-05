using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal interface IMerchant
    {
        public static void Merchant(Player player, Merchant merchant, ItemManager item)
        {
            int height = 1;
            int itemlist = 0;

            foreach (int itemNum in merchant.SellItemNum)
            {
                SetCursorAndWrite_up(2, $"소지금 : {player.Gold}Gold");
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 3 + height);
                itemlist++;
                Console.Write($"{itemlist}.");
                item.items[itemNum].DisplayInfoMerchant(player);
                height += 2;
            }
            SetCursorAndWrite_up(15, "0.나가기");

        }

        public static void OpenMerchnat()
        {

            SetCursor_down(0);
            Console.WriteLine("구매할 아이템 번호을 적어주세요.");
            SetCursor_down(1);
        }
        public static void EmptyMerchant()
        {
            SetCursorAndWrite_up(1, "구입할수 있는 아이템이 없습니다.");
            SetCursor_down(0);
            Console.WriteLine("2. 판매");
            SetCursor_down(1);
            Console.WriteLine("0. 나가기");
        }
        public static void MerchantSelect()
        {
            SetCursor_down(-2);
            Console.WriteLine("1. 구매");
            SetCursor_down(-1);
            Console.WriteLine("2. 판매");
            SetCursor_down(0);
            Console.WriteLine("0. 나가기");
        }
        public static void purchase(Player player, ItemManager item)
        {
            int height = 0;
            int itemNum = 0;
            bool have = false;
            foreach (bool plyaerAcquire in player.PlayerAcquire)
            {
                if (plyaerAcquire)
                {
                    have = true;
                    Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1 + height + 3);
                    height += 1;
                    if (player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot && player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum == itemNum)
                    {
                        Console.Write($"[E]{height}.");
                    }
                    else
                    {
                        Console.Write($"   {height}. ");
                    }
                    item.items[itemNum].DisplayInfoPurchase();
                }
                itemNum++;
            }
            if (!have)
            {
                SetCursorAndWrite_up(1, "보유한 아이템이 없습니다.");
                SetCursor_down(0);
                Console.WriteLine("0. 나가기                   ");
            }
            else
            {
                SetCursor_down(-1);
                Console.WriteLine("판매할 아이템 번호을 적어주세요.");
                SetCursor_down(0);
                Console.WriteLine("0. 나가기                   ");
            }


        }
        public static void Transaction(Player player, Merchant merchant, int sellItemNum)
        {

            if (sellItemNum >= 1 && sellItemNum <= merchant.SellItemNum.Count)
            {
                if (!player.PlayerAcquire[merchant.SellItemNum[sellItemNum - 1]])
                {
                    SetCursor_down(0);
                    Console.WriteLine($"{sellItemNum}번 아이템 구매                           ");
                }
                else
                {
                    Console.WriteLine($"이미 구매한 아이템입니다.                           ");
                }
            }
            else
            {
                SetCursor_down(0);
                Console.WriteLine("잘못된입력                           ");
                SetCursor_down(1);
                Console.WriteLine("                           ");
            }

        }
        public static void SetCursorAndWrite_up(int top, string text)//상하만
        {
            Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowTop + 1 + top);
            Console.Write(text);
            // 한글 문자도 제대로 처리하기 위해 바이트 길이 계산
            int textByteLength = System.Text.Encoding.Default.GetByteCount(text);
            int remainingSpace = Console.WindowWidth - (2 + textByteLength);

            if (remainingSpace > 0)
            {
                Console.Write(new string(' ', remainingSpace));
            }
        }
        public static void Setcuusor_up(int top)
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + top);
        }
        public static void SetCursor_down(int down)//아랫단
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowHeight - 5 + down);
        }
        public static void DownFrameClear()
        {
            SetCursor_down(0);
            Console.WriteLine("                                     ");
            SetCursor_down(1);
            Console.WriteLine("                                     ");
            SetCursor_down(2);
            Console.WriteLine("                                     ");
            SetCursor_down(3);
            Console.WriteLine("                                     ");
        }
        public static int HelpInput()
        {
            SetCursor_down(1);
            string input = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    SetCursor_down(0);
                    Console.WriteLine("입력이 비어있습니다. 다시 시도해주세요.");
                    SetCursor_down(1);
                    Console.WriteLine("                           ");
                }
                else if (!int.TryParse(input, out int userInput))
                {
                    SetCursor_down(0);
                    Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                    SetCursor_down(1);
                    Console.WriteLine("                           ");
                }
                else
                {
                    return userInput;
                }
                SetCursor_down(1);
                input = Console.ReadLine();
            }
        }
    }
}
