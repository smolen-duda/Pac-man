using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;

namespace Game1
{
    public class Player
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public System.Diagnostics.Stopwatch frightwatch { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int Previousx { get; set; }
        public int Previousy { get; set; }
        public int Score { get; set; }
        public int lifes { get; set; }
        public Player(int row1, int column, string name)
        {
            Name = name;
            Row = row1;
            Column = column;
            Score = 0;
            Previousx = Row;
            Previousy = Column;
            Mode = "Normal";
            frightwatch = new System.Diagnostics.Stopwatch();
            lifes = 2;
        }
        public char[][] FirstMove(ConsoleKeyInfo key,char[][] mapGame)
        {
            
            Console.SetCursorPosition(Column, Row);
            Console.Write(" ");
            if (key.Key == ConsoleKey.RightArrow)
            {
                Column++;
                Score += 10;
                mapGame[Row][Column] = ' ';
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                Column--;
                Score += 10;
                mapGame[Row][Column] = ' ';
            }
            Console.SetCursorPosition(Column, Row);
            Console.ForegroundColor = GlobalVariables.DarkYellow;
            Console.Write('C');
            Console.ForegroundColor = Color.White;

            return mapGame;
        }


        public bool CheckTarget(int xt, int yt)
        {
            if (xt == Row && yt == Column)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public char[][] MovePlayer(ConsoleKeyInfo key, char[][] mapGame)
        {
            if (Previousx != Row && Previousy != Column)
            {
                Previousx = Row;
                Previousy = Column;
            }
            Console.SetCursorPosition(Column, Row);
            
            Console.Write(" ");
           
            if (key.Key == ConsoleKey.RightArrow)
            {
                if ((Column, Row) == (42, 14))
                {
                    Column = 4;
                }
                else
                {
                    Column++;
                    Column++;
                    if (mapGame[Row][Column] == '#' || mapGame[Row][Column] == '-')
                    {
                        Column--;
                        Column--;
                    }
                }

            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                if ((Column,Row)== (4, 14))
                {
                    Column = 42;
                }
                else
                {
                    Column--;
                    Column--;
                    if (mapGame[Row][Column] == '#' || mapGame[Row][Column] == '-')
                    {
                        Column++;
                        Column++;
                    }
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                Row--;
                if (mapGame[Row][Column] == '#' || mapGame[Row][Column] == '-')
                {
                    Row++;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                Row++;
                if (mapGame[Row][Column] == '#' || mapGame[Row][Column] == '-')
                {
                    Row--;
                }
            }
            return CheckChar(Column, Row, mapGame);
        }

        public void Draw()
        {
            Console.SetCursorPosition(Column, Row);
            Console.ForegroundColor = GlobalVariables.DarkYellow;
            Console.Write('C');
            Console.ForegroundColor = Color.White;
        }



        private char[][] CheckChar(int coordCol, int coordRow, char[][] mapGame)
        {

            if (mapGame[coordRow][coordCol] == 'o')
            {
                Mode = "Hunting";
                frightwatch.Restart();
            }
            else
            {
                Mode = "Normal";
            }
            if (mapGame[coordRow][coordCol] == '.' || mapGame[coordRow][coordCol] == 'o')
            {
                mapGame[coordRow][coordCol] = ' ';
                Score += 10;
            }

                return mapGame;
        }

        public bool IsItEnd(char[][] mapGame)
        {

            foreach (char[] row in mapGame)
            {
                if (row.ToList().Exists(x => x == '.'))
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
