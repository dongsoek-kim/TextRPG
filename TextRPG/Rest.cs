using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Rest
    {
        public static void RestTime(Player player)
        {

            player.Gold -= 500;
            player.Health = 100;      
        }
    }
}

