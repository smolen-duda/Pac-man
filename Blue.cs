using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Blue : Ghost
    {
        public int blueCounter { get; set; }
        public Blue(int row, int column, int speed, char[][] map) : base(row, column, speed, map)
        {
            blueCounter = 0;
        }

        public ConsoleKeyInfo Chase(Player player, ConsoleKeyInfo direction, Red redGhost)
        {
            Target(player, direction,redGhost); 
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }

        public ConsoleKeyInfo Scatter()
        {
            (Targetx, Targety) = (27, 40);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }


        public ConsoleKeyInfo Frightened()
        {
            ConsoleKeyInfo key;
            if (blueCounter == 0)
            {
                (Targetx, Targety) = (Previousx, Previousy);
                List<(int, int)> possibleMoves = new List<(int, int)> { (Previousx, Previousy) };

                key = ChooseDirection(possibleMoves);
                blueCounter++;
            }
            else
            {
                key = ChooseRandomDirection();
            }

            return key;
        }

        private void Target(Player player, ConsoleKeyInfo direction, Red redGhost)
        {
            ConsoleKeyInfo up = new ConsoleKeyInfo('g', ConsoleKey.UpArrow, false, false, false);
            ConsoleKeyInfo down = new ConsoleKeyInfo('d', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo left = new ConsoleKeyInfo('l', ConsoleKey.LeftArrow, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo('p', ConsoleKey.RightArrow, false, false, false);

            int newCol = player.Column;
            int newRow = player.Row;
            if (direction.Key == left.Key)
            {
                if (player.Column == 4)
                {
                    newCol = player.Column - 4;
                }
                else
                {
                    newCol = player.Column - 4;
                }
            }
            else if (direction.Key == right.Key)
            {
                if (player.Column == 42)
                {
                    newCol = player.Column + 4;
                }
                else
                {
                    newCol = player.Column + 4;
                }
            }
            else if (direction.Key == up.Key)
            {
                newRow = player.Row - 2;
                newCol = player.Column - 4;
            }
            else if (direction.Key == down.Key)
            {
                newRow = player.Row + 2;
            }

            (int vx, int vy) = (redGhost.Row - redGhost.Previousx, redGhost.Column - redGhost.Previousy);
            (int xt, int yt) = (newRow-redGhost.Row, newCol-redGhost.Column);
            newRow += xt;
            newCol += yt;

            (Targetx, Targety) = (newRow, newCol);
        }
    }
}
