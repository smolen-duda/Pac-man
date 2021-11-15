using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console=Colorful.Console;

namespace Game1
{
    public abstract class Ghost
    {
        char[][] Map { get; }
        int Speed;
        public int Column { get; set; }
        public int Row { get; set; }
        public int Previousx { get; set; }
        public int Previousy { get; set; }
        public int Targetx { get; set; }
        public int Targety { get; set; }
        public int Step { get; set; }

        public string Mode { get; set; }
        public Ghost(int row1, int column, int speed, char[][] originalmap)
        {
            Row = row1;
            Column = column;
            Previousx = row1;
            Previousy = column;
            Targetx = row1;
            Targety = column;
            Speed = speed;
            Map = originalmap;
            Mode = "Scatter";
            Step = 2;
        }


        

        public void FirstMove(Player player, char c)
        {
            Previousx = Row;
            if (player.Column > Column)
            {
                Previousy = Column - 1;
            }
            else
            {
                Previousy = Column + 1;
            }
            Console.SetCursorPosition(Column, Row);
            Console.Write(' ');
            if (Column == 23)
            {
                Column++;
            }
            else
            {
                Row--;
            }
            Draw(c);

        }

        public void SecondMove(char c)
        {
            Previousx = Row;
            Previousy = Column;
            if (Column == 21)
            {
                Console.SetCursorPosition(Column, Row);
                Console.Write(' ');
                Column++;
                Draw(c);
            }
            else if (Column == 25)
            {
                Console.SetCursorPosition(Column, Row);
                Console.Write(' ');
                Column--;
                Draw(c);
            }
            else
            {
                Console.SetCursorPosition(Column, Row);
                Console.Write(' ');
                Row--;
                Draw(c);
            }

        }

        public void ThirdMove(Player player,char c)
        {
            Previousx = Row;
            Previousy = Column;
            Console.SetCursorPosition(Column, Row);
            Console.Write(' ');
            if (Column == 23 && Row != 11)
            {
                Column++;
            }
            else if (Column == 23 && Row == 11)
            {
                Column = player.Column;
            }
            else
            {
                Row--;
            }
            Draw(c);

        }

        public void FourthMove(char c)
        {
            Previousx = Row;
            Console.SetCursorPosition(Column, Row);
            Console.Write('-');
            Row--;
            Draw(c);

        }


        public void Move(ConsoleKeyInfo key, char[][] mapGame)
        {
            if (Mode != "Eaten")
            {
                Previousx = Row;
                Previousy = Column;
            }
            
            Console.SetCursorPosition(Column, Row);

            Console.Write(mapGame[Row][Column]);

            if (key.Key == ConsoleKey.RightArrow)
            {
                if ((Column,Row)== (43, 14))
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
                if ((Column,Row) == (5, 14))
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
                if (mapGame[Row][Column] == '#' || (mapGame[Row][Column] == '-'&& Mode!="Eaten"))
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

        }

        public void Draw(char c)
        {
            Console.SetCursorPosition(Column, Row);
            if(Mode=="Frightened")
            {
                Console.BackgroundColor = Color.Black;
                Console.ForegroundColor = Color.Blue;
                Console.Write('F');
            }
            else
            {
                if (c == 'R')
                {
                    Console.BackgroundColor = Color.Black;
                    Console.ForegroundColor = Color.Red;
                    Console.Write(c);

                }
                else if (c == 'P')
                {
                    Console.BackgroundColor = Color.Black;
                    Console.ForegroundColor = Color.Pink;
                    Console.Write(c);
                }
                else if (c == 'B')
                {
                    Console.BackgroundColor = Color.Black;
                    Console.ForegroundColor = Color.LightBlue;
                    Console.Write(c);

                }
                else if (c == 'O')
                {
                    Console.BackgroundColor = Color.Black;
                    Console.ForegroundColor = Color.Orange;
                    Console.Write(c);
                }
            }
            Console.ForegroundColor = Color.White;
        }


        public ConsoleKeyInfo ChooseRandomDirection()
        {

            ConsoleKeyInfo up = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
            ConsoleKeyInfo down = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo left = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
            var random = new Random();
            List<(int,int)> neighbors=FindNeighbors(Row,Column,Map);
            List<ConsoleKeyInfo> possibleMove = new List<ConsoleKeyInfo>();
            foreach ((int,int) n in neighbors)
            {
                if (n.Item2 == Column && n.Item1 < Row)
                {
                    possibleMove.Add(up);
                }
                else if (n.Item2 < Column && n.Item1 == Row)
                {
                    possibleMove.Add(left);
                }
                else if (n.Item2 == Column && n.Item1 > Row)
                {
                    possibleMove.Add(down);
                }
                else
                {
                    possibleMove.Add(right);
                }
            }

            ConsoleKeyInfo key = possibleMove.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            return key;

        }

        protected ConsoleKeyInfo ChooseDirection(List<(int, int)> possibleMoves)
        {
            ConsoleKeyInfo up = new ConsoleKeyInfo('g', ConsoleKey.UpArrow, false, false, false);
            ConsoleKeyInfo down = new ConsoleKeyInfo('d', ConsoleKey.DownArrow, false, false, false);
            ConsoleKeyInfo left = new ConsoleKeyInfo('l', ConsoleKey.LeftArrow, false, false, false);
            ConsoleKeyInfo right = new ConsoleKeyInfo('p', ConsoleKey.RightArrow, false, false, false);
            ConsoleKeyInfo nothing = new ConsoleKeyInfo('n', ConsoleKey.Enter, false, false, false);

            ConsoleKeyInfo key = nothing;




            if (possibleMoves.Exists(x => x.Item2 == Column && x.Item1 < Row))
            {
                key = up;
            }
            else if (possibleMoves.Exists(x => x.Item2 < Column && x.Item1 == Row))
            {
                key = left;
            }
            else if (possibleMoves.Exists(x => x.Item2 == Column && x.Item1 > Row))
            {
                key = down;
            }
            else
            {
                key = right;
            }


            return key;

        }


        protected List<(int, int)> FindTheBestWay(int xt, int yt)
        {
            List<int> answer = new List<int>();
            int x = Row;
            int y = Column;
            int time;
            List<(int, int)> neighbors = FindNeighbors(x, y, Map);
            


            List<((int, int), int)> times = new List<((int, int), int)>();
            

            foreach ((int,int) target in neighbors)
            {
                time = (2*target.Item1 - 2*xt)* (2*target.Item1 - 2*xt) +  (target.Item2 - yt)* (target.Item2 - yt);
                times.Add((target, time));
                answer.Add(time);
            }
            neighbors.Clear();
            int theBest=answer.Min();
            foreach (((int, int), int) n in times.Where(w => w.Item2 == theBest).ToList())
            {
                neighbors.Add(n.Item1);
            }

            return neighbors;
            
        }
        


        private List<(int,int)> FindNeighbors(int x, int y, char[][] mapGame)
        {
            List<(int, int)> neighbors= new List<(int,int)>();
            if(mapGame[x+1][y]=='.'|| mapGame[x + 1][y] == 'o')
            {
                neighbors.Add((x + 1, y));
            }
            if(mapGame[x][y+2] == '.' || mapGame[x][y+2] == 'o')
            {
                neighbors.Add((x , y+2));
            }
            if (mapGame[x - 1][y] == '.' || mapGame[x - 1][y] == 'o')
            {
                neighbors.Add((x - 1, y));
            }
            if (mapGame[x][y-2] == '.' || mapGame[x][y-2] == 'o')
            {
                neighbors.Add((x , y-2));
            }
            if (Map[Previousx][Previousy] != '-')
            {
                List<(int, int)> toRemove = neighbors.Where(q => q == (Previousx, Previousy)).ToList();

                neighbors.Remove(toRemove[0]);
            }


            if((Row, Column)==(11,20) && neighbors.Exists(q => q == (10,20)))
            {
                neighbors.Remove(neighbors.Where(q => q == (10,20)).ToList()[0]);
            }
            else if ((Row, Column) == (11, 26) && neighbors.Exists(q => q == (10, 26)))
            {
                neighbors.Remove(neighbors.Where(q => q == (10, 26)).ToList()[0]);
            }
            else if ((Row, Column) == (21, 26) && neighbors.Exists(q => q == (20, 26)))
            {
                neighbors.Remove(neighbors.Where(q => q == (20, 26)).ToList()[0]);
            }
            else if ((Row, Column) == (21, 20) && neighbors.Exists(q => q == (20, 20)))
            {
                neighbors.Remove(neighbors.Where(q => q == (20, 20)).ToList()[0]);
            }
            return neighbors;
        }

    }
}
