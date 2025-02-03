using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextRPG
{
    internal abstract class Item
    {
        public int ItemNumber { get; set; } // 아이템 고유 번호
        public string Grade { get; set; } // 아이템 등급
        public string Name { get; set; }//아이템 이름
        public string Description { get; set; } // 아이템 설명
        public int EquipSlot { get; set; } // //장착부위: 0=head , 1=body , 2=arm , 3=leg , 4=foot , 5= weapon
        public string Type { get; set; }
        public bool Own { get; set; }//소유여부
        

        public Item(int itemNumber, string grade, string name, string description)
        {
            ItemNumber = itemNumber;
            Grade = grade;
            Name = name;
            Description = description;
        }

        public abstract void DisplayInfo(); // 아이템 정보 출력 (추상 메서드)
    }

    // 방어구 클래스
    internal class Armor : Item
    {
        public int Defense { get; set; } // 방어력

        public Armor(int itemNumber, string grade, string name,string description, string equipSlot, int defense)
            : base(itemNumber, grade, name, description)
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

        public override void DisplayInfo()
        {
            Console.WriteLine($"[방어구] 번호: {ItemNumber}, 등급: {Grade},이름: {Name}");
            Console.Write($"설명: {Description}, 착용 부위: {EquipSlot}, 방어력: {Defense},소유여부: ");
            Console.WriteLine(Own?"O":"X");
        }
    }

    // 무기 클래스
    internal class Weapon : Item
    {
        public int AttackPower { get; set; } // 공격력

        public Weapon(int itemNumber, string grade,string name,string description, int attackPower)
            : base(itemNumber, grade, name,description)
        {
            AttackPower = attackPower;
            EquipSlot= 6;
            Type = "Weapon";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[무기] 번호: {ItemNumber}, 등급: {Grade},이름: {Name}");
            Console.Write($"설명: {Description}, 공격력: {AttackPower},소유여부: ");
            Console.WriteLine(Own ? "O" : "X");
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
        
        public void ShowAllItems()
        {
            int height = 0;
            foreach (var item in items)
            {
                Console.SetCursorPosition(Console.WindowLeft+2, Console.WindowTop + 1+ height);
                height+=2;
                item.DisplayInfo();
            }
        }
        public void ShowItem(int item_num)
        {
            Console.SetCursorPosition(Console.WindowLeft + 2, Console.WindowTop);
            items[item_num].DisplayInfo();
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
                    if (type == "Armor")
                    {
                        string equipSlot = item["EquipSlot"].ToString();
                        int defense = Convert.ToInt32(item["Defense"]);

                        Armor armor = new Armor(itemNumber, grade, name, description, equipSlot, defense);
                        AddItem(armor);
                    }
                    else if (type == "Weapon")
                    {
                        int attackPower = Convert.ToInt32(item["AttackPower"]);

                        Weapon weapon = new Weapon(itemNumber, grade, name, description, attackPower);
                        AddItem(weapon);
                    }
                }
            }
            else
            {
                Console.WriteLine($"현재 실행 경로: {Directory.GetCurrentDirectory()}\");\r\n");
            }

            // 아이템 목록 출력
            //ShowAllItems();
        }

        public void Inventory(bool[] _player_have)//인벤토리, 보유한 아이템리스트와 착용중인아이템 구분.
        {

        }

        public (float,float) Equipment(bool[] _player_have)//튜플을 이용한 AttackPower,Defense출력
        {
            int item_num = 0;
            AttackPower = 0;
            Defense = 0;
            foreach (bool player_have in _player_have)
            {
                if (player_have)
                {
                    switch (items[item_num].Type)
                    {
                        case "Armor":
                            Defense += ((Armor)items[item_num]).Defense;
                            break;
                        case "Weapon":
                            AttackPower += ((Weapon)items[item_num]).AttackPower;
                            break;
                    }
                }
                item_num++;
            }
            return (AttackPower, Defense);
        }
    }
}
