using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
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
            Dungeon dungeon = new Dungeon();   
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
                    int input;
                    int itemNum;

                    Draw.DrawFrame();
                    Draw.Merchant(player, merchant, item);
                    if (merchant.SellItemNum.Count == 0)
                    {
                        do
                        {
                            Draw.DrawFrame();
                            Draw.EmptyMerchant();
                            input = Draw.HelpInput();
                            if (input == 0)
                            {
                                break;
                            }
                            else if (input == 2)
                            {
                                Draw.purchase(player, item);
                            }
                            else
                            {
                                Draw.SetCursor_down(0);
                                Console.WriteLine("잘못된 값 입력                            ");
                                Draw.SetCursor_down(1);
                                Console.WriteLine("                                          ");
                            }
                        } while (true);
                    }
                    else
                    {
                        do
                        {
                            Draw.DrawFrame();
                            Draw.Merchant(player, merchant, item);
                            Draw.MerchantSelect();
                            input = Draw.HelpInput();
                            if (input == 0)
                            {
                                break;
                            }
                            else if (input == 1)
                            {
                                do
                                {
                                    Draw.DrawFrame();
                                    Draw.SetCursorAndWrite_up(1, "구입화면입니다");
                                    Draw.Merchant(player, merchant, item);
                                    Draw.SetCursor_down(0);
                                    Console.WriteLine("구입하고싶은 아이템 번호를 입력해주세요");
                                    input = Draw.HelpInput();
                                    Draw.DownFrameClear();
                                    if (input == 0)
                                    {
                                        break;
                                    }
                                    if (input > merchant.SellItemNum.Count())
                                    {
                                        Draw.SetCursor_down(0);
                                        Console.WriteLine("잘못된 값 입력                            ");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    itemNum = merchant.SellItemNum[input - 1];
                                    if (player.Gold >= item.items[itemNum].Price)
                                    {
                                        Draw.Transaction(player, merchant, input);
                                        if (!player.PlayerAcquire[merchant.SellItemNum[input - 1]])
                                        {
                                            merchant.transaction(player, itemNum, item.items[itemNum].Price);
                                        }
                                    }
                                    else
                                    {
                                        Draw.SetCursor_down(1);
                                        Console.WriteLine("Gold 부족");
                                        Console.ReadKey();
                                    }
                                    inventory.MakeOwnList(player);
                                } while (true);
                            }
                            else if (input == 2)
                            {

                                do
                                {
                                    Draw.DrawFrame();
                                    Draw.SetCursorAndWrite_up(1, "판매화면입니다");
                                    Draw.purchase(player,item);
                                    input = Draw.HelpInput();
                                    Draw.DownFrameClear();
                                    if (input == 0)
                                    {
                                        break;
                                    }
                                    if (input < player.HaveItemNumber()+1)
                                    {
                                        itemNum = inventory.OwnItem[input - 1];
                                        merchant.Purchase(player,inventory, item, itemNum);
                                        inventory.MakeOwnList(player);
                                    }
                                    else 
                                    {
                                        Draw.SetCursor_down(1);
                                        Console.WriteLine("잘못된 값 입력                            ");
                                    }
                                } while (true);
                            }
                        } while (true);
                    }

                }
                else if (path == 4)
                {
                    int input;
                    Draw.DrawFrame();
                    Draw.EnterDungeon();
                    do
                    {
                        string dungeonResult;
                        input = Draw.HelpInput();
                        Draw.DrawFrame();
                        if (input == 1)
                        {
                            dungeonResult = dungeon.InDungeon(player, input);
                            Draw.InDungeon(player, dungeon, dungeonResult, input);
                            if (dungeonResult == "LevelUP")
                            {
                                merchant.MakeListOfSellItemNum(player);
                            }
                            Console.ReadKey();
                            break;
                        }
                        else if (input == 2)
                        {
                            dungeonResult = dungeon.InDungeon(player, input);
                            Draw.InDungeon(player, dungeon, dungeonResult, input);
                            if (dungeonResult == "LevelUP")
                            {
                                merchant.MakeListOfSellItemNum(player);
                            }
                            Console.ReadKey();
                            break;
                        }
                        else if (input == 3)
                        {
                            dungeonResult = dungeon.InDungeon(player, input);
                            Draw.InDungeon(player, dungeon, dungeonResult, input);

                            if (dungeonResult == "LevelUP")
                            {
                                merchant.MakeListOfSellItemNum(player);
                            }
                            Console.ReadKey();
                            break;
                        }
                        else if (input == 0)
                        {
                            break;
                        }
                        else
                        {
                            Draw.SetCursor_down(1);
                            Console.WriteLine("잘못된 입력입니다.                    ");
                        }
                    } while (true);
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