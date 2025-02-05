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
            IMaiiInterface.DrawFrame();
            ItemManager item = new ItemManager();
            item.ItemLoad();//아이템 로드
            Player player;
            player = Player.LoadPlayerData();  // 플레이어 데이터를 불러옴

            if (player.Name == "")//이름이 초기값이라면
            {
                IMaiiInterface.GameStart(out name, out job);
                player = new Player(name, job);//플레이어 생성
                player.InitializeEquipInfo();
            }
            Merchant merchant;
            merchant = Merchant.LoadMerchantData();
            if (merchant.SellItemNum.Count == 0)
            {
                merchant = new Merchant();
                merchant.MakeListOfSellItemNum(player);
            }
            Inventory inventory = new Inventory();
            inventory.MakeOwnList(player);
            Dungeon dungeon = new Dungeon();   
            while (true)
            {
                MainScene(out path, player);
                if (path == 1)
                {

                    IMaiiInterface.DrawFrame();
                    IMaiiInterface.Setcuusor_up(1);
                    IMaiiInterface.PlayerState(player);
                    Console.ReadKey();
                }
                else if (path == 2)
                {
                    IMaiiInterface.DrawFrame();
                    IInventory.Inventory(player, item);
                    int inputNum;
                    inputNum = IInventory.HelpInput();
                    IMaiiInterface.SetCursor_down(1);
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
                                    IMaiiInterface.DrawFrame();
                                    IInventory.Inventory(player, item);
                                    
                                    inputNum = IInventory.Equipmentprocedures(player);
                                    inventory.Equipmentprocedures(player, item, inputNum);
                                } while (inputNum != 0);
                                break;
                            case 0:
                                break;
                            default:
                                IInventory.SetCursor_down(0);
                                Console.WriteLine("잘못된 값 입력                            ");
                                inputNum = IMaiiInterface.HelpInput();
                                break;
                        }
                    } while (inputNum != 0);
                }
                else if (path == 3)
                {
                    int input;
                    int itemNum;

                    IMaiiInterface.DrawFrame();
                    IMerchant.Merchant(player, merchant, item);
                    if (merchant.SellItemNum.Count == 0)
                    {
                        do
                        {
                            IMaiiInterface.DrawFrame();
                            IMerchant.EmptyMerchant();
                            input = IMaiiInterface.HelpInput();
                            if (input == 0)
                            {
                                break;
                            }
                            else if (input == 2)
                            {
                                IMerchant.purchase(player, item);
                            }
                            else
                            {
                                IMaiiInterface.SetCursor_down(0);
                                Console.WriteLine("잘못된 값 입력                            ");
                                IMaiiInterface.SetCursor_down(1);
                                Console.WriteLine("                                          ");
                            }
                        } while (true);
                    }
                    else
                    {
                        do
                        {
                            IMaiiInterface.DrawFrame();
                            IMerchant.Merchant(player, merchant, item);
                            IMerchant.MerchantSelect();
                            input = IMerchant.HelpInput();
                            if (input == 0)
                            {
                                break;
                            }
                            else if (input == 1)
                            {
                                do
                                {
                                    IMaiiInterface.DrawFrame();
                                    IMerchant.SetCursorAndWrite_up(1, "구입화면입니다");
                                    IMerchant.Merchant(player, merchant, item);
                                    IMerchant.SetCursor_down(0);
                                    Console.WriteLine("구입하고싶은 아이템 번호를 입력해주세요");
                                    input = IMerchant.HelpInput();
                                    IMaiiInterface.DownFrameClear();
                                    if (input == 0)
                                    {
                                        break;
                                    }
                                    if (input > merchant.SellItemNum.Count())
                                    {
                                        IMerchant.SetCursor_down(0);
                                        Console.WriteLine("잘못된 값 입력                            ");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    itemNum = merchant.SellItemNum[input - 1];
                                    if (player.Gold >= item.items[itemNum].Price)
                                    {
                                        IMerchant.Transaction(player, merchant, input);
                                        if (!player.PlayerAcquire[merchant.SellItemNum[input - 1]])
                                        {
                                            merchant.transaction(player, itemNum, item.items[itemNum].Price);
                                        }
                                    }
                                    else
                                    {
                                        IMerchant.SetCursor_down(1);
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
                                    IMaiiInterface.DrawFrame();
                                    IMerchant.SetCursorAndWrite_up(1, "판매화면입니다");
                                    IMerchant.purchase(player,item);
                                    input = IMerchant.HelpInput();
                                    IMaiiInterface.DownFrameClear();
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
                                        IMerchant.SetCursor_down(1);
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
                    IMaiiInterface.DrawFrame();
                    IDungeon.EnterDungeon();
                    do
                    {
                        string dungeonResult;
                        input = IDungeon.HelpInput();
                        IMaiiInterface.DrawFrame();
                        if (input == 1)
                        {
                            dungeonResult = dungeon.InDungeon(player, input);
                            IDungeon.InDungeon(player, dungeon, dungeonResult, input);
                            if (dungeonResult == "Death")
                            {
                                IDungeon.SetCursor_down(1);
                                Console.ReadKey();
                                player.Death();
                            }
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

                            IDungeon.InDungeon(player, dungeon, dungeonResult, input); 
                            if (dungeonResult == "Death")
                            {
                                IDungeon.SetCursor_down(1);
                                Console.ReadKey();
                                player.Death();
                            }

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
                            IDungeon.InDungeon(player, dungeon, dungeonResult, input);
                            if (dungeonResult == "Death")
                            {
                                IDungeon.SetCursor_down(1);
                                Console.ReadKey();
                                player.Death();
                            }
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
                            IMaiiInterface.SetCursor_down(1);
                            Console.WriteLine("잘못된 입력입니다.                    ");
                        }
                    } while (true);
                }
                else if (path == 5)
                {
                    if (player.Gold < 500)
                    {
                        IMaiiInterface.NoMoney(player);
                        Console.ReadKey();
                    }
                    else
                    {
                        int rest;
                        IMaiiInterface.DrawFrame();
                        rest = IMaiiInterface.Rest(player);
                        if (rest == 1)
                        {
                            Rest.RestTime(player);
                            IMaiiInterface.DrawFrame();
                            IMaiiInterface.SetCursorAndWrite_up(1, "체력이 전부 회복되었다");
                            Console.ReadKey();
                        }

                    }
                }
                else if (path == 6)
                {
                    player.SavePlayerData();
                    merchant.SaveMerchantData();
                    break;
                }
                else if(path==99)
                {
                    player.Name = "";
                    player.SavePlayerData();
                    merchant.SellItemNum.Clear();
                    merchant.SaveMerchantData();
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
            IMaiiInterface.DrawFrame();
            IMaiiInterface.MainScene(out paht);
        }
    }
}