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
            
            player.InitializeEquipInfo(); 
            Merchant merchant= new Merchant();
            merchant.MakeListOfSellItemNum(player);
            Inventory inventory = new Inventory(); 
            while (true)
            {
                MainScene(out path, player);
                if (path == 1)
                {

                    Draw.DrawFrame();
                    Draw.Setcuusor_up(1);
                    Draw.PlayerState(player);
                    Console.ReadKey();
                }
                else if (path == 2)
                {
                    Draw.DrawFrame();
                    Draw.Inventory(player, item);
                    int inputNum;
                    inputNum = Draw.HelpInput();
                    Draw.SetCursor_down(1);
                    do
                    {
                        inventory.MakeOwnList(player);
                        if (inventory.OwnItem.Count == 0)
                        {
                            break;
                        }
                        switch (inputNum)
                        {
                            case 1://아이템장착관리
                                do
                                {
                                    Draw.DrawFrame();
                                    Draw.Inventory(player, item);
                                    inventory.MakeOwnList(player);
                                    inputNum = Draw.Equipmentprocedures(player);
                                    inventory.Equipmentprocedures(player, item, inputNum);
                                } while (inputNum != 0);
                                break;
                            case 0:
                                break;
                            default:
                                Draw.SetCursor_down(0);
                                Console.WriteLine("잘못된 값 입력                            ");
                                inputNum = Draw.HelpInput();
                                break;
                        }
                    } while (inputNum != 0);
                }
                else if (path == 3)
                {
                    int sellItemNum;
                    int itemNum;
                    do
                    {
                        Draw.DrawFrame();
                        Draw.Merchant(player, merchant, item);
                        if (merchant.SellItemNum.Count == 0)
                        {
                            Draw.EmptyMerchant();
                            break;
                        }
                        else
                        {
                            sellItemNum = Draw.OpenMerchnat();

                            if (sellItemNum == -1)
                            {
                                break;
                            }
                            else if (sellItemNum > merchant.SellItemNum.Count())
                            {
                                Draw.SetCursor_down(0);
                                Console.WriteLine("잘못된 값 입력                            ");
                                Console.ReadKey();
                                continue;
                            }
                            itemNum = merchant.SellItemNum[sellItemNum - 1];
                            if (player.Gold >= item.items[itemNum].Price)
                            {
                                Draw.Transaction(player, merchant, sellItemNum);
                                merchant.transaction(player, itemNum, item.items[itemNum].Price);
                            }
                            else
                            {
                                Draw.SetCursor_down(1);
                                Console.WriteLine("Gold 부족");
                                Console.ReadKey();
                            }
                        }
                    } while (true);
                }
                else if (path == 4)
                {

                }
                else if (path == 5)
                {
                    if (player.Gold < 500)
                    {
                        Draw.NoMoney(player);
                        Console.ReadKey();
                    }
                    else
                    {
                        int rest;
                        Draw.DrawFrame();
                        rest = Draw.Rest(player);
                        if (rest == 1)
                        {
                            Rest.RestTime(player);
                            Draw.DrawFrame();
                            Draw.SetCursorAndWrite_up(1, "체력이 전부 회복되었다");
                            Console.ReadKey();
                        }

                    }
                }
                else if (path == 6)
                {
                    break;
                }

            }
            // 종료 대기
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