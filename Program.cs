using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;
using System.Drawing;
using Console = Colorful.Console;

namespace Game1
{

    public static class GlobalVariables
    {
        public static System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        public static int interval = 711;

        public static int counter = 0;
        public static bool switchMode = true;

        public static Color DarkYellow = Color.FromArgb(255, 255, 54);

    }
    class Program
    {

        static void Main(string[] args)
        {



            List<string> map = new List<string>();
            StreamReader sr = new StreamReader("map.txt");
            string row = sr.ReadLine();
            while (!(string.IsNullOrEmpty(row)))
            {
                map.Add(row);
                row = sr.ReadLine();
            }
            sr.Close();
            char[][] mapGame = new char[map.Count][];
            int i = 0;
            foreach (string row1 in map)
            {
                mapGame[i] = row1.ToCharArray();
                i++;
            }
            char[][] originalMap = new char[mapGame.Length][];
            for (int y = 0; y < mapGame.Length; y++)
            {
                originalMap[y] = (char[])mapGame[y].Clone();
            }

            Console.SetWindowSize(25, 30);

            Console.CursorVisible = false;
            Player player = new Player(17, 23, "Stranger");

            Red redGhost = new Red(11, 23,1, originalMap);

            Blue blueGhost = new Blue(14, 21,1, originalMap);

            Pink pinkGhost = new Pink(14, 23, 1,originalMap);

            Orange orangeGhost = new Orange(14, 25,1, originalMap);


            foreach (char[] row1 in mapGame)
            {
                foreach (char c in row1)
                {
                    if (c=='#')
                    {
                        Console.BackgroundColor = Color.DarkBlue;
                        Console.ForegroundColor = Color.DarkBlue;
                        Console.Write(c);
                    }
                    else
                    {
                        if (c == 'C')
                        {
                            Console.BackgroundColor = Color.Black;
                            Console.ForegroundColor = GlobalVariables.DarkYellow;
                            Console.Write(c);
                        }
                        else if (c == 'R')
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
                        else
                        {
                            Console.BackgroundColor = Color.Black;
                            Console.ForegroundColor = Color.White;
                            Console.Write(c);
                        }
                        Console.ForegroundColor = Color.White;
                    }
                }
                Console.WriteLine();
            }

            Console.SetCursorPosition(45, 14);
            Console.Write("Score: {0}", player.Score);
            Console.SetCursorPosition(45, 15);
            Console.Write("Lifes: {0}", player.lifes);

            ConsoleKeyInfo firstKey = Console.ReadKey(true);
            while (!(firstKey.Key == ConsoleKey.RightArrow || firstKey.Key == ConsoleKey.LeftArrow))
            {
                firstKey = Console.ReadKey(true);
            }



            mapGame = player.FirstMove(firstKey, mapGame);
            Console.SetCursorPosition(52, 14);
            Console.Write(player.Score);
            Console.SetCursorPosition(52, 15);
            Console.Write(player.lifes);

            redGhost.FirstMove(player,'R');
            blueGhost.FirstMove(player,'B');
            pinkGhost.FirstMove(player,'P');
            orangeGhost.FirstMove(player,'O');


            ConsoleKeyInfo key = Console.ReadKey(true);


            GeneralMovement manager = new GeneralMovement();
            Timer aTimer = new Timer();
            Timer bTimer = new Timer();

            GlobalVariables.watch.Start();
            



            aTimer.Elapsed += (s, e) =>
            {
                manager.Runghosts(player, redGhost, blueGhost, pinkGhost, orangeGhost, mapGame, key);


                redGhost.Draw('R');
                blueGhost.Draw('B');
                pinkGhost.Draw('P');
                orangeGhost.Draw('O');



                aTimer.Interval = GlobalVariables.interval;

            };
            aTimer.Interval = GlobalVariables.interval;
            aTimer.Enabled = true;


            bTimer.Elapsed += (s, e) =>
            {


                Console.SetCursorPosition(52, 14);
                Console.Write(player.Score);
                Console.SetCursorPosition(52, 15);
                Console.Write(player.lifes);


                player.MovePlayer(key, mapGame);


                manager.IsCatched(player, redGhost, pinkGhost, blueGhost, orangeGhost);
                if (player.lifes == -1)
                {
                    Console.SetCursorPosition(21, 14);
                    Console.WriteLine("GAME OVER");
                    aTimer.Stop();
                    bTimer.Stop();
                }

                if (player.Mode == "Hunting")
                {
                    redGhost.Mode = "Frightened";
                    blueGhost.Mode = "Frightened";
                    pinkGhost.Mode = "Frightened";
                    orangeGhost.Mode = "Frightened";
                    GlobalVariables.interval = 1000;
                    GlobalVariables.watch.Stop();
                }

                player.Draw();
            };
            bTimer.Interval = 479;
            bTimer.Enabled = true;

            while (player.IsItEnd(mapGame))
            {

                key = Console.ReadKey(true);

            }




            GlobalVariables.watch.Stop();
            aTimer.Stop();
            bTimer.Stop();
            Console.SetCursorPosition(21, 14);

            Console.WriteLine("YOU WIN");


            Console.ReadKey();

        }

    }
    
}
