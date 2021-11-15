using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    
    public class Red : Ghost
    {
        public int redCounter { get; set; }
        ConsoleKeyInfo up = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
        ConsoleKeyInfo down = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
        ConsoleKeyInfo left = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
        ConsoleKeyInfo right = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
        public Red(int row, int column, int speed, char[][] map) : base(row, column, speed, map)
        {
            redCounter = 0;
        }
        public ConsoleKeyInfo Chase(Player player)
        {
            (Targetx, Targety) = (player.Row, player.Column);
            List<(int,int)> possibleMoves = FindTheBestWay(Targetx, Targety);

            ConsoleKeyInfo key = ChooseDirection(possibleMoves);


            return key;
        }

        public ConsoleKeyInfo Scatter()
        {
            (Targetx, Targety) =(0,38);
            List<(int, int)> possibleMoves = FindTheBestWay(Targetx, Targety);

            ConsoleKeyInfo key = ChooseDirection(possibleMoves);

            return key;
        }

        public ConsoleKeyInfo Frightened()
        {
            ConsoleKeyInfo key;
            if (redCounter==0)
            {
                (Targetx, Targety) = (Previousx,Previousy);
                List<(int, int)> possibleMoves = new List<(int, int)> { (Previousx, Previousy) };

                 key= ChooseDirection(possibleMoves);
                redCounter++;
            }
            else
            {
                key = ChooseRandomDirection();
            }

            return key;
        }
    }
}
