using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Dungeon
    {
        public int Exp {  get; set; }
        public int gold {  get; set; }
        public float NeedDefense { get; set; }
        public float LoseHealth { get; set; }
        public Dungeon() 
        {
            Exp = 0;
            gold = 0;
            NeedDefense = 0;
            LoseHealth = 0;
        }
        public string InDungeon(Player player,int difficulty)
        {
            float sumPlayerDefens = player.Defense + player.EquipDefense;
            Random random = new Random();
            if (difficulty == 1)
            {
                NeedDefense = 8;
            }
            else if (difficulty == 2)
            {
                    NeedDefense = 10;
            }
            else
            {
                    NeedDefense = 15;
            }
            
                
            if(sumPlayerDefens < (NeedDefense/2))
            {
                return Defeat(player);
            }
            else
            {
                LoseHealth= (float)(random.NextDouble() * (35 - 20) + 20) - sumPlayerDefens + NeedDefense;
                if(LoseHealth < 0) LoseHealth = 0;
                player.Health -= LoseHealth;
                if (player.Health > 0)
                {
                    return Clear(player,difficulty);
                }
                else
                {
                    return "Death";
                }
            }
        }
        public string Defeat(Player player)
        {
            player.Health -= 50f;
            if (player.Health > 0)
            {
                return "Defeat";
            }
            else
            {
                return "Death";
            }

        }
        public string Clear(Player player, int difficulty)
        {
            float sumPlayerAttacpower = player.AttackPower + player.EquipAttackPower;
            Random random = new Random();
            if (difficulty == 1)
            {
                Exp = 1;
                gold = 1000+ (int)(1000 * random.NextDouble() * ((sumPlayerAttacpower / 10) * 2) - (sumPlayerAttacpower / 10)); 
                player.GetGold(gold);
                return player.GetExp(Exp);
            }
            else if (difficulty == 2)
            {
                Exp = 2;
                gold = 1500+ (int)(1500 * random.NextDouble() * ((sumPlayerAttacpower / 10) * 2) - (sumPlayerAttacpower / 10));
                player.GetGold(gold);
                return player.GetExp(Exp);
            }
            else 
            {
                Exp = 3;
                gold = 2000+ (int)(2000 * random.NextDouble() * ((sumPlayerAttacpower / 10) * 2) - (sumPlayerAttacpower / 10));
                player.GetGold(gold);
                return player.GetExp(Exp);
            }
        }
    }
}
