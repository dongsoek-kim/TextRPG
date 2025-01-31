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
        int atk { get; set; }
        int def { get; set; }
        float health { get; set; }
        int gold { get; set; }

        public Player(string _name, string _job)
        {
            Name = _name;
            level = 1;
            job = _job;
            atk = 10;
            def = 5;
            health = 100f;
            gold = 1000;
        }
        public void ShowPlayer()
        {
            Console.WriteLine($"이름: {Name},레벨: {level},직업: {job},공격력: {atk},방어력: {def},체력: {health}, 골드: {gold}");
        }
    }
}
