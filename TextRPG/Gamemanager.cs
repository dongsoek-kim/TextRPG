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
            Draw.DrawFrame();
            ItemManager item = new ItemManager();
            item.ItemLoad();//아이템 로드
            Draw.GameStart(out name, out job);
            Player player = new Player(name, job);//플레이어 생성
            Plyaer_Equip(player, item, 2);
            Plyaer_Equip(player, item, 2);
            while (true)
            {
                MainScene(out path, player);
                if (path == 1)
                {

                    Draw.DrawFrame();
                    Draw.Setcuusor_up(1);
                    player.ShowPlayer(item.AttackPower,item.Defense);
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
        static void Plyaer_Equip(Player player,ItemManager item ,int itemNum)
        {
            if (player.PlayerEquip[itemNum] ==false)
            { 
                player.Equip(itemNum); 
            }
            else
            {
                if(Draw.Is_Equip()==1)
                {
                    player.EquipmentReplacement(itemNum, item.items[itemNum].EquipSlot);
                }
            }

            (float attackPower, float defense) = item.Equipment(player.PlayerEquip);
            player.Player_state(attackPower, defense);
        }
    }
}