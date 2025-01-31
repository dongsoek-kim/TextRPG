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
            int width = Console.WindowWidth - 1;  // 프레임 너비
            int height1 = Console.WindowHeight - 7; // 상단 프레임 높이 (하단의 두 배)
            int height2 = Console.WindowTop + 7;  // 하단 프레임 높이
            int startX = 0;   // X 시작 위치
            int startY = 0;   // Y 시작 위치

            //Console.Clear();

            DrawBox(startX, startY, width, height1);// 상단 프레임
            DrawBox(startX, startY + height1 - 1, width, height2);//하단프레임
        }

        static public void DrawBox(int x, int y, int width, int height)
        {
            //지붕 프레임
            Console.SetCursorPosition(x, y);
            Console.WriteLine(new string('*', width));

            // 중간 프레임
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write('*');
                Console.SetCursorPosition(x + width - 1, y + i);
                Console.Write('*');
            }

            // 바닥프레임
            Console.SetCursorPosition(x, y + height - 1);
            Console.WriteLine(new string('*', width));
        }

        static public void GameStart(out string name,out string job)
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1);
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");

            string? input;
            do
            {
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 3);
                Console.WriteLine("원하시는 이름을 설정해주세요");
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 4);
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
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 1);
                Console.WriteLine("원하시는 직업을 선택해주세요");
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 3);
                Console.WriteLine("1.전사");
                Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop + 4);
                Console.WriteLine("2.도적");
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
        }
    }
}