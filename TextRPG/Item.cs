using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextRPG
{
    internal abstract class Item
    {
        public int ItemNumber { get; set; } // 아이템 고유 번호
        public string Grade { get; set; } // 아이템 등급
        public int Price {  get; set; }//아이템 가격
        public string Name { get; set; }//아이템 이름
        public string Description { get; set; } // 아이템 설명
        public int EquipSlot { get; set; } // //장착부위: 0=head , 1=body , 2=arm , 3=leg , 4=foot , 5= weapon
        public string Type { get; set; }
        //public bool Own { get; set; }//소유여부


        public Item(int itemNumber, string grade, string name, string description,int price)
        {
            ItemNumber = itemNumber;
            Grade = grade;
            Name = name;
            Description = description;
            Price = price;
        }

        public abstract void DisplayInfoInventory(); // 인벤토리에서 아이템 출력
        public abstract void DisplayInfoPurchase();//상점 판매에서 아이템 출력
        public abstract void DisplayInfoMerchant(Player player); // 상점 구입에서 아이템 출력
    }

    // 방어구 클래스
    internal class Armor : Item
    {
        public int Defense { get; set; } // 방어력

        public Armor(int itemNumber, string grade, string name, string description, string equipSlot, int defense, int price)
            : base(itemNumber, grade, name, description, price)
        {
            switch (equipSlot)
            {
                case ("head"):
                    {
                        EquipSlot = 0;
                        break;
                    }
                case ("body"):
                    {
                        EquipSlot = 1;
                        break;

                    }
                case ("arm"):
                    {
                        EquipSlot = 2;
                        break;
                    }
                case ("leg"):
                    {
                        EquipSlot = 3;
                        break;
                    }
                case ("foot"):
                    {
                        EquipSlot = 4;
                        break;
                    }
            }            //장착부위: 0=head , 1=body , 2=arm , 3=leg , 4=foot

            Defense = defense;
            Type = "Armor";
        }

        public override void DisplayInfoInventory()
        {
            Console.WriteLine($"이름: {Name} | 등급: {Grade} | 방어력: {Defense} | 설명: {Description}");
        }
        public override void DisplayInfoMerchant(Player player)
        {
            Console.Write($"이름: {Name} | 가격: {Price} | 방어력: {Defense} | 설명: {Description}");
            if (player.PlayerAcquire[ItemNumber])
            {
                Console.WriteLine("| 구매완료");
            }
            else
            {
                Console.WriteLine();
            }
        }
        public override void DisplayInfoPurchase()
        {
            Console.WriteLine($"이름: {Name} | 판매가격:{(int)Price * 0.85f} | 방어력: {Defense} | 설명: {Description}");
        }
    }
    // 무기 클래스
    internal class Weapon : Item
    {
        public int AttackPower { get; set; } // 공격력

        public Weapon(int itemNumber, string grade,string name,string description, int attackPower,int price)
            : base(itemNumber, grade, name,description,price)
        {
            AttackPower = attackPower;
            EquipSlot= 5;
            Type = "Weapon";
        }

        public override void DisplayInfoInventory()
        {
            Console.WriteLine($"이름: {Name} | 등급: {Grade} | 공격력: {AttackPower} | 설명: {Description}");
        }
        public override void DisplayInfoMerchant(Player player)
        {
            Console.Write($"이름: {Name} | 가격: {Price} | 공격력: {AttackPower} | 설명: {Description}");
            if (player.PlayerAcquire[ItemNumber])
            {
                Console.WriteLine("| 구매완료");
            }
            else 
            {
                Console.WriteLine();
            }
        }
        public override void DisplayInfoPurchase()
        {
            Console.WriteLine($"이름: {Name} | 판매가격:{(int)Price * 0.85f} | 공격력: {AttackPower} | 설명: {Description}");
        }

    }

    // 아이템 관리 클래스
    internal class ItemManager
    {
        public List<Item> items = new List<Item>();
        public float AttackPower { get; set; } = 0;
        public float Defense { get; set; } = 0;
        public float Health { get; set; }= 0;
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        
        public void ItemLoad()
        {
            string relativePath = @"..\..\..\";
            string jsonFile = "items.json";  // JSON 파일명
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            if (File.Exists(jsonPath))
            {
              
                string jsonData = File.ReadAllText(jsonPath);
                List<Dictionary<string, object>> itemsData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);

                foreach (var item in itemsData)
                {
                    int itemNumber = Convert.ToInt32(item["ItemNumber"]);
                    string grade = item["Grade"].ToString();
                    string name = item["Name"].ToString();
                    string description = item["Description"].ToString();
                    string type = item["Type"].ToString();
                    int price=0;
                    switch (grade)
                    {
                        case "Common":
                            price = 500;
                            break;
                        case "Rare":
                            price = 800;
                            break;
                        case "Unique":
                            price = 1200;
                            break;
                    }
                    if (type == "Armor")
                    {
                        string equipSlot = item["EquipSlot"].ToString();
                        int defense = Convert.ToInt32(item["Defense"]);

                        Armor armor = new Armor(itemNumber, grade, name, description, equipSlot, defense, price);
                        AddItem(armor);
                    }
                    else if (type == "Weapon")
                    {
                        int attackPower = Convert.ToInt32(item["AttackPower"]);

                        Weapon weapon = new Weapon(itemNumber, grade, name, description, attackPower,price);
                        AddItem(weapon);
                    }
                }
            }
            else
            {
                Console.WriteLine($"현재 실행 경로: {Directory.GetCurrentDirectory()}\");\r\n");
            }
        }
    }
}
