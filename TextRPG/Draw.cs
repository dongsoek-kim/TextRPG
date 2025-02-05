//전체화면시 높이 209/ 너비 51
//출력관리
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG
{
    internal class Draw
    {
        public static void DrawFrame()
        {
            Console.Clear();
            int width = Console.WindowWidth - 1;  // 프레임 너비
            int height1 = Console.WindowHeight - 7; // 상단 프레임 높이 (하단의 두 배)
            int height2 = Console.WindowTop + 7;  // 하단 프레임 높이
            int startX = 0;   // X 시작 위치
            int startY = 0;   // Y 시작 위치
             //Console.Clear();
            DrawBox(startX, startY, width, height1);// 상단 프레임
            DrawBox(startX, startY + height1 - 1, width, height2);//하단프레임
        }
        public static void DrawBox(int x, int y, int width, int height)
        {
            //지붕 프레임
            Console.SetCursorPosition(x, y);
            Console.WriteLine(new string('-', width));

            // 중간 프레임
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write('|');
                Console.SetCursorPosition(x + width - 1, y + i);
                Console.Write('|');
            }

            // 바닥프레임
            Console.SetCursorPosition(x, y + height - 1);
            Console.WriteLine(new string('-', width));
        }

        public static void GameStart(out string name,out string job)
        {
            SetCursorAndWrite_up(1, "스파르타 던전에 오신 여러분 환영합니다.");
            string? input;
            do
            {
                SetCursorAndWrite_up(3, "원하시는 이름을 설정해주세요");
                SetCursor_down(1);
                input = Console.ReadLine(); // 값 입력 받기

                if (string.IsNullOrEmpty(input))
                {
                    SetCursor_down(0);
                    Console.WriteLine("똑바로 된 값을 넣어주세요.");                  
                }
            } while (string.IsNullOrEmpty(input));
            name = input;
            DrawFrame();
            do
            {
                SetCursorAndWrite_up(1, "원하시는 직업을 선택해주세요");
                Console.WriteLine();
                Console.WriteLine();
                SetCursorAndWrite_up(7, "1.전사");
                Console.WriteLine();
                SetCursorAndWrite_up(9, "2.도적");
                SetCursor_down(1);
                input = Console.ReadLine(); // 값 입력 받기
                Console.Clear();
                DrawFrame();

                if ( input != "1" && input != "2")
                {
                    SetCursor_down(0);
                    Console.WriteLine("1 또는 2를 입력하세요");
                }
            } while (input != "1" && input != "2");
            if (input == "1")
            {
                job = "전사";
            }
            else
            {
                job = "도적";
            }
        }//게임시작 대사
         public static void MainScene(out int? path)
        {
            DrawFrame();
            path = null;
            SetCursorAndWrite_up(1, "스파르타 마을에 오신 여러분 환영합니다.");
            SetCursorAndWrite_up(2, "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            SetCursorAndWrite_up(6, "1.상태 보기");
            SetCursorAndWrite_up(7, "2.인벤토리");
            SetCursorAndWrite_up(8, "3.상점");
            SetCursorAndWrite_up(9, "4.던전입장");
            SetCursorAndWrite_up(10,"5.휴식하기");
            SetCursorAndWrite_up(11, "6.게임종료");
            SetCursorAndWrite_up(15, "데이터 초기화를 원하시면 DeleteData를 입력해주세요");
            SetCursor_down(0);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            do
            {
                SetCursor_down(1);
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        path = 1;
                        break;
                    case "2":
                        path = 2;
                        break;
                    case "3":
                        path = 3;
                        break;
                    case "4":
                        path = 4;
                        break;
                    case "5":
                        path = 5;
                        break;
                    case "6":
                        path = 6;
                        break;
                    case "DeleteData":
                        path = 99;
                        break;
                    default:
                        SetCursor_down(0);
                        Console.WriteLine("잘못된 입력입니다.                        ");
                        break;
                }
            } while (path == null);
        }
        public static void PlayerState(Player player)
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1);
            Console.WriteLine($"이름: {player.Name}\n| 레벨: {player.Level}\n| 직업: {player.Job}\n| 공격력: {player.AttackPower+player.EquipAttackPower}+({player.EquipAttackPower})" +
                              $"\n| 방어력: {player.Defense + player.EquipDefense}+({player.EquipDefense})\n| 체력: {player.Health}\n| 골드: {player.Gold}");
        }
        public static void Inventory(Player player,ItemManager item)//인벤토리에서 보유중인 아이템 출력
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
                    if (player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipSlot && player.equipInfo[item.items[itemNum].EquipSlot].PlayerEquipItemNum==itemNum)
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
                SetCursorAndWrite_up(0,"보유한 아이템이 없습니다.");
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
        public static void Merchant(Player player,Merchant merchant,ItemManager item)
        {
            int height = 1;
            int itemlist=0;
            
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
                    Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1 + height+3);
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
        public static void EnterDungeon()
        {
            SetCursorAndWrite_up(1, "던전입장");
            SetCursorAndWrite_up(2, "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            SetCursorAndWrite_up(5, "1. 쉬운 던전     | 방어력 5 이상 권장");
            SetCursorAndWrite_up(6, "2. 일반 던전     | 방어력 11 이상 권장\r\n");
            SetCursorAndWrite_up(7, "3. 어려운 던전    | 방어력 17 이상 권장\r\n");
            SetCursorAndWrite_up(10, "0.나가기");
        }
        public static void InDungeon(Player player,Dungeon dungeon,string dungeonResult,int input)
        {
            switch (dungeonResult)
            {
                case "Death":
                    {
                        SetCursorAndWrite_up(1, "플레이어사망!");
                        SetCursorAndWrite_up(2, "모든 정보가 지워집니다!");

                        break;                        
                    }
                case "Defeat":
                    {
                        SetCursorAndWrite_up(1, "패배!");
                        SetCursorAndWrite_up(2, "당신은 패배하였습니다.");
                        SetCursorAndWrite_up(3, "[탐험결과]");
                        SetCursorAndWrite_up(5, $"체력{player.Health+50}->{player.Health}");
                        break;
                    }
                case "LevelUP":
                    {
                        if (input == 1)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 쉬운 던전을 클리어 하였습니다!");
                        }
                        if (input == 2)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 일반 던전을 클리어 하였습니다!");
                        }
                        if (input == 3)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 어려운 던전을 클리어 하였습니다!");
                        }
                        SetCursorAndWrite_up(3, "[탐험결과]");
                        SetCursorAndWrite_up(4, $"레벨업! Level :{player.Level - 1}->{player.Level}");
                        SetCursorAndWrite_up(5, $"체력{player.Health + dungeon.LoseHealth}->{player.Health}");
                        SetCursorAndWrite_up(6, $"Gold{player.Gold - dungeon.gold}->{player.Gold}");
                        break; 
                    }
                case "Clear":
                    {
                        if (input == 1)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 쉬운 던전을 클리어 하였습니다!");
                        }
                        if (input == 2)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 일반 던전을 클리어 하였습니다!");
                        }
                        if (input == 3)
                        {
                            SetCursorAndWrite_up(1, "축하합니다!");
                            SetCursorAndWrite_up(2, "당신은 어려운 던전을 클리어 하였습니다!");
                        }
                        SetCursorAndWrite_up(5, $"체력{player.Health + dungeon.LoseHealth}->{player.Health}");
                        SetCursorAndWrite_up(6, $"Gold{player.Gold - dungeon.gold}->{player.Gold}");
                        break; 
                    }
                default:
                    {
                        SetCursorAndWrite_up(5, "오류");
                        break;
                    }
            }            
        }
       
        public static int Rest(Player player)
        {
            SetCursorAndWrite_up(1, $"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold}G)");
            SetCursorAndWrite_up(4, " 1.휴식하기");
            SetCursorAndWrite_up(5, " 0. 나가기");
            SetCursor_down(1);
            do
            {
                SetCursor_down(1);
                string input = Console.ReadLine();
                if (input == "1")
                {
                    return 1;
                }
                else if (input == "0")
                {
                    return 0;
                }

                else
                {
                    SetCursor_down(0);
                    Console.WriteLine("잘못된입력                ");
                }
            } while (true);            
        }
        public static void NoMoney(Player player)
        {
            Console.WriteLine($"돈이없다..보유금액 : {player.Gold} Gold");

        }

        public static void SetCursorAndWrite_up(int top, string text)//상하만
        {
            Console.SetCursorPosition(Console.WindowLeft + 1, Console.WindowTop+1 + top);
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
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowHeight - 5+down);
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