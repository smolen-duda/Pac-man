using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Pink : Ghost
    {
        public int pinkCounter { get; set; }
        public Pink(int row, int column, int speed, char[][] map) : base(row, column, speed, map)
        {
            pinkCounter = 0;
        }
        public ConsoleKeyInfo Chase(Player player, ConsoleKeyInfo direction)
        {
            Target(player, direction);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }

        public ConsoleKeyInfo Scatter()
        {
            (Targetx, Targety) = (0, 8);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);
            return ChooseDirection(possibleMoves);
        }

        public ConsoleKeyInfo Frightened()
        {
            ConsoleKeyInfo key;
            if (pinkCounter == 0)
            {
                (Targetx, Targety) = (Previousx, Previousy);
                List<(int, int)> possibleMoves = new List<(int, int)> { (Previousx, Previousy) };

                key = ChooseDirection(possibleMoves);
                pinkCounter++;
            }
            else
            {
                key = ChooseRandomDirection();
            }

            return key;
        }

        private void Target(Player player, ConsoleKeyInfo direction)
        {
            ConsoleKeyInfo up = new ConsoleKeyInfo('g', ConsoleKey.UpArrow, false, false, false);
            ConsoleKeyInfo down = new ConsoleKeyInfo('d', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo left = new ConsoleKeyInfo('l', ConsoleKey.LeftArrow, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo('p', ConsoleKey.RightArrow, false, false, false);
            ConsoleKeyInfo nothing = new ConsoleKeyInfo('n', ConsoleKey.Enter, false, false, false);

            int newCol = player.Column;
            int newRow = player.Row;
            if (direction.Key == left.Key)
            {
                if (player.Column == 42)
                {
                    newCol = player.Column - 8 ;
                }
                else
                {
                    newCol = player.Column - 8;
                }
            }
            else if (direction.Key == right.Key)
            {
                if (player.Column == 42)
                {
                    newCol = player.Column + 8;
                }
                else
                {
                    newCol = player.Column + 8;
                }
            }
            else if (direction.Key == up.Key)
            {
                newRow = player.Row - 4;
                newCol = player.Column - 8;
            }
            else if (direction.Key == down.Key)
            {
                newRow = player.Row + 4;
            }
            (Targetx,Targety)= (newRow, newCol);
        }
    }
}
