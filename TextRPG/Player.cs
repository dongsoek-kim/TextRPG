using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        string Name;
        int level { get; set; }
        string job { get; set; }
        int attackPower { get; set; }
        int defense { get; set; }
        float health { get; set; }
        int gold { get; set; }
        
        public Player(string _name, string _job)
        {
            Name = _name;
            level = 1;
            job = _job;
            attackPower = 10;
            defense = 5;
            health = 100f;
            gold = 1000;
        }
        public void ShowPlayer()
        {
            Console.WriteLine($"이름: {Name}\n| 레벨: {level}\n| 직업: {job}\n| 공격력: {attackPower}\n| 방어력: {defense}\n| 체력: {health}\n| 골드: {gold}");
        }
    }
}
