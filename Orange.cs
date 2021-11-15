using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Orange : Ghost
    {
        public int orangeCounter { get; set; }


        public Orange(int row, int column, int speed, char[][] map) : base(row, column, speed, map)
        {
            orangeCounter = 0;
        }

        public ConsoleKeyInfo Chase(Player player)
        {
            Target(player);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }

        public ConsoleKeyInfo Scatter()
        {
            (Targetx, Targety) = (27, 6);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }


        public ConsoleKeyInfo Frightened()
        {
            ConsoleKeyInfo key;
            if (orangeCounter == 0)
            {
                (Targetx, Targety) = (Previousx, Previousy);
                List<(int, int)> possibleMoves = new List<(int, int)> { (Previousx, Previousy) };

                key = ChooseDirection(possibleMoves);
                orangeCounter++;
            }
            else
            {
                key = ChooseRandomDirection();
            }

            return key;
        }

        public void Target(Player player)
        {
            int distance = 4*(Row - player.Row) * (Row - player.Row) + (Column - player.Column) * (Column - player.Column);
            if (distance>16)
            {
                (Targetx, Targety) = (player.Row, player.Column);
            }
            else
            {
                (Targetx, Targety) = (27, 6);
            }
        }

    }
    
}
