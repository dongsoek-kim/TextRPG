﻿//전체화면시 높이 209/ 너비 51
//출력관리
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Draw
    {
        static public void DrawFrame()
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
        static public void DrawTitle()
        {
            Console.Clear(); // 콘솔 화면 정리
            DrawFrame();
            int startX = Console.WindowLeft + 15; // X 좌표 시작 위치
            int startY = Console.WindowTop + 1;  // Y 좌표 시작 위치

            // 텍스트 아트 (줄 단위로 배열에 저장)
            string[] titleArt = new string[]
            {
"       ..                                         ",
"      :+=-          .=:                           ",
"       :=--.      .:==-                           ",
"         --::   .-=-:                             ",
"          :++- -+=:                                                    man",
"          :=+**-.             .                                        of",
"        -+===*@#=:           =#=::                                     lamancha",
"        :--=+-+#=+=. ......::-**=-=.              ",
"      .::==-: -+.:+=:   .-+%=:-===#%-             ",
"     :-==--:  =+: .=++: .++++#*+=#@@+             ",
"      .-:.-   -=-   -+=.    ++*#%#+=+=.           ",
"   .-. ..:.   #-=    :==    ::+*#+-..-+=---.      ",
"  -==-==+===-=++-::====+-.:--:*##++-.:#*:.::.     ",
"  .:--=+=++++===   -+======+*-+##+=---*+-:.       ",
"    .:-===--=+=:     ...::-=+=++-:::..-...--.     ",
"     ..=----=++*-:.         .::..:-. -: ...:::    ",
"      ::.   .:-=--==-::.    .:    -. :.           ",
"               .:-=+==+==-::-                     ",
"                 .:::::--:.-=-:.                  "
            };

            // 텍스트 아트를 원하는 좌표에서 출력
            for (int i = 0; i < titleArt.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i); // 각 줄의 위치 설정
                Console.WriteLine(titleArt[i]); // 출력
            }
        }
        static public void DrawBox(int x, int y, int width, int height)
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

        static public void GameStart(out string name,out string job)
        {
            SetCursorAndWrite(1, "스파르타 던전에 오신 여러분 환영합니다.");

            string? input;
            do
            {
                SetCursorAndWrite(3, "원하시는 이름을 설정해주세요");
                SetCursor();
                input = Console.ReadLine(); // 값 입력 받기

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("똑바로 된 값을 넣어주세요.");
                }
            } while (string.IsNullOrEmpty(input));
            name = input;
            Console.Clear();
            DrawFrame();
            do
            {
                SetCursorAndWrite(1, "원하시는 직업을 선택해주세요");
                Console.WriteLine();
                Console.WriteLine();
                SetCursorAndWrite(7, "1.전사");
                Console.WriteLine();
                SetCursorAndWrite(9, "2.도적");
                SetCursor();
                input = Console.ReadLine(); // 값 입력 받기
                Console.Clear();
                DrawFrame();

                if ( input != "1" && input != "2")
                {
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
        /*static void SetCursorAndWrite(int left,int top, string text)//좌우 상하
        {
            Console.SetCursorPosition(Console.WindowLeft + left, Console.WindowTop + top);
            Console.WriteLine(text);
        }*/
        static public void MainScene(out int? path)
        {
            Console.Clear();
            DrawFrame();
            path = null;
            SetCursorAndWrite(1, "스파르타 마을에 오신 여러분 환영합니다.");
            SetCursorAndWrite(2, "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            SetCursorAndWrite(6, "1.상태 보기");
            SetCursorAndWrite(7, "2.인벤토리");
            SetCursorAndWrite(8, "3.상점");
            SetCursorAndWrite(12, "원하시는 행동을 입력해주세요.");
            do
            {
                SetCursor();
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
                    default:
                        SetCursorAndWrite(12, "잘못된 입력입니다.");
                        break;
                }
            } while (path == null);
        }
        static void SetCursorAndWrite(int top, string text)//상하만
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop+top);
            Console.WriteLine(text);
        }
        public static void Setcuusor_up(int top)
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + top);
        }
        static void SetCursor()//아랫단
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowHeight - 5);
        }

    }
}