using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

namespace TextRPG
{
    internal class Gamemanager
    {

        static void Main()
        {
            Console.WriteLine("원활한 플레이를 위해 전체화면으로 바꿔주세요");
            Console.WriteLine("아무 키나 눌러서 실행...");
            Console.ReadKey();

            string name;
            string job;
            int? path;
            ItemManager item = new ItemManager();
            // 프레임 출력
            Draw.DrawFrame();
            item.ItemLoad();
            Draw.GameStart(out name, out job);
            Player player = new Player(name, job);
            while (true)
            {
                MainScene(out path, player);
                if (path == 1)
                {
                    Draw.DrawFrame();
                    Draw.Setcuusor_up(1);
                    player.ShowPlayer();
                    Console.ReadKey();
                }
                else if (path == 4)
                {
                    break;
                }
            }
            // 종료 대기
            Console.SetCursorPosition(40, 1);
            Draw.DrawTitle();
            Console.SetCursorPosition(40, 25);
            Console.WriteLine("아무 키나 눌러서 종료...");
            Console.ReadKey();

        }

        static void MainScene(out int? paht, Player player)
        {
            Draw.DrawFrame();
            Draw.MainScene(out paht);
        }
    }
}