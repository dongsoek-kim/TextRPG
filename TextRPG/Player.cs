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
            Console.WriteLine($"이름: {Name},레벨: {level},직업: {job},공격력: {attackPower},방어력: {defense},체력: {health}, 골드: {gold}");
        }
    }
}
