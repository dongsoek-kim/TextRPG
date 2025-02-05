using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal interface IDungeon
    {
 
        public static void EnterDungeon()
        {
            SetCursorAndWrite_up(1, "던전입장");
            SetCursorAndWrite_up(2, "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            SetCursorAndWrite_up(5, "1. 쉬운 던전     | 방어력 5 이상 권장");
            SetCursorAndWrite_up(6, "2. 일반 던전     | 방어력 11 이상 권장\r\n");
            SetCursorAndWrite_up(7, "3. 어려운 던전    | 방어력 17 이상 권장\r\n");
            SetCursorAndWrite_up(10, "0.나가기");
        }
        public static void InDungeon(Player player, Dungeon dungeon, string dungeonResult, int input)
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
                        SetCursorAndWrite_up(5, $"체력{player.Health + 50}->{player.Health}");
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
