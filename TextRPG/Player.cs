using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        public string Name;
        public int Level { get; set; }
        public string Job { get; set; }
        public float AttackPower { get; set; }
        public float Defense { get; set; }
        public float Health { get; set; }
        public int Gold { get; set; }
        public int EquipAttackPower { get; set; }
        public int EquipDefense { get; set; }
        public bool[] PlayerAcquire { get; set; } = new bool[20];
        public int RequiredExp { get; set; }
        public int CurrentExp { get; set; }


        public struct EquipInfo
        {
            public bool PlayerEquipSlot { get; set; } = false;
            public int PlayerEquipItemNum { get; set; } = -1;
            public EquipInfo() { }
        }
        public EquipInfo[] equipInfo = new EquipInfo[6];//장착부위: 0=head , 1=body , 2=arm , 3=leg , 4=foot,5=weapon       
        public Player(string name, string job)
        {
            Name = name;
            Level = 1;
            Job = job;
            AttackPower = 10;
            Defense = 5;
            Health = 100f;
            Gold = 10000;
            EquipAttackPower = 0;
            EquipDefense = 0;
            RequiredExp = 1;
            CurrentExp = 0;
        }
        public void InitializeEquipInfo()
        {
            for (int i = 0; i < equipInfo.Length; i++)
            {
                equipInfo[i] = new EquipInfo { PlayerEquipSlot = false, PlayerEquipItemNum = -1 };
            }
        }
        public string GetExp(int getExp)
        {
            CurrentExp += getExp;
            if (CurrentExp >= RequiredExp)
            {
                Level += 1;
                CurrentExp = 0;
                RequiredExp = 1 + (Level * (2 - 1));
                AttackPower += 0.5f;
                Defense += 1;
                return "LevelUP";
            }
            else 
            {
                return "Clear";
            }
        }
        public void GetGold(int getgold)
        {
            Gold += getgold;
        }
        public void Death()
        {

        }
        public int HaveItemNumber()
        {
            int haveItemNumber=0; 
            foreach(bool playerAcquire in PlayerAcquire)
            {
                if (playerAcquire)
                {
                    haveItemNumber++;
                }
            }
            return haveItemNumber;
        }
        public void SavePlayerData()
        {
            string relativePath = @"..\..\..\";
            string jsonFile = "PlayerData.json";  // JSON 파일명
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                // Player 객체를 JSON 문자열로 직렬화
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(jsonPath, json); // 파일에 저장
                Console.WriteLine("Player data saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving player data: {ex.Message}");
            }
        }
        public static Player LoadPlayerData()
        {
            string relativePath = @"..\..\..\";
            string jsonFile = "PlayerData.json";  // JSON 파일명
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonFile))
                {
                    // 파일에서 JSON 문자열을 읽어 Player 객체로 역직렬화
                    string json = File.ReadAllText(jsonFile);
                    Player player = JsonConvert.DeserializeObject<Player>(json);
                    Console.WriteLine("Player data loaded successfully!");
                    return player;
                }
                else
                {
                    Console.WriteLine("Player data file not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading player data: {ex.Message}");
                return null;
            }
        }
    }
       
}
