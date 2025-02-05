using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal interface IInventory
    {
        public static void Inventory(Player player, ItemManager item)//인벤토리에서 보유중인 아이템 출력
        {
            int height = 0;
            int itemNum = 0;
            bool have = false;
            foreach (bool plyaerAcquire in player.PlayerAcquire)
            {
                if (plyaerAcquire)
                {
                    have = true;
                    Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1 + height);
                    height += 1;
                    if (player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot && player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum == itemNum)
                    {
                        Console.Write($"[E]{height}.");
                    }
                    else
                    {
                        Console.Write($"   {height}. ");
                    }
                    item.items[itemNum].DisplayInfoInventory();
                }
                itemNum++;
            }
            if (!have)
            {
                SetCursorAndWrite_up(0, "보유한 아이템이 없습니다.");
            }
            else
            {
                SetCursorAndWrite_up(22, "1.장착관리");
                SetCursorAndWrite_up(23, "0.나가기");
            }
        }
        public static int Equipmentprocedures(Player player)
        {

            SetCursorAndWrite_up(22, "장착화면입니다 착용하고싶은 장비 번호를 입력해주세요,");
            SetCursorAndWrite_up(23, "착용중인 장비를 입력하면 해제됩니다.");
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
