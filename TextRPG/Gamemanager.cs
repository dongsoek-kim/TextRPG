using System.Runtime.InteropServices;
using System.Threading;

namespace TextRPG
{
    internal class Gamemanager
    {

        static void Main()
        {
            // 프레임 출력
            DrawFrame();

            // 종료 대기
            Console.SetCursorPosition(40, 25);
            Console.WriteLine("아무 키나 눌러서 종료...");
            Console.ReadKey();
        }

        static void DrawFrame()
        {
            int width = Console.WindowWidth - 1;  // 프레임 너비
            int height1 = Console.WindowHeight - 8; // 상단 프레임 높이 (하단의 두 배)
            int height2 = 8;  // 하단 프레임 높이
            int startX = 0;   // X 시작 위치
            int startY = 0;   // Y 시작 위치

            Console.Clear();

            DrawBox(startX, startY, width, height1);// 상단 프레임
            DrawBox(startX, startY + height1-1, width, height2);//하단프레임
        }

        static void DrawBox(int x, int y, int width, int height)
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
    }
}