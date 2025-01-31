using System.Runtime.InteropServices;
using System.Threading;

namespace TextRPG
{
    internal class Gamemanager
    {
        
        static void Main()
        {
            string name;
            string job;
            ItemManager item =  new ItemManager();
            // 프레임 출력
            Draw.DrawFrame();
            item.ItemLoad();
            Draw.GameStart(out name, out job);
            Player player = new Player(name,job);




            // 종료 대기
            Console.SetCursorPosition(40, 1);
            player.ShowPlayer();
            Draw.DrawTitle();
            Console.SetCursorPosition(40, 25);
            Console.WriteLine("아무 키나 눌러서 종료...");
            Console.ReadKey();

        }

    }
       
}