using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game1
{
    class GeneralMovement
    {
        ConsoleKeyInfo up = new ConsoleKeyInfo(' ', ConsoleKey.UpArrow, false, false, false);
        ConsoleKeyInfo down = new ConsoleKeyInfo(' ', ConsoleKey.DownArrow, false, false, false);
        ConsoleKeyInfo left = new ConsoleKeyInfo(' ', ConsoleKey.LeftArrow, false, false, false);
        ConsoleKeyInfo right = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, false, false, false);
        public GeneralMovement()
        {

        }

        public void Runghosts(Player player, Red redGhost, Blue blueGhost, Pink pinkGhost, Orange orangeGhost, char[][] mapGame, ConsoleKeyInfo key)
        {
            Move(player, redGhost, blueGhost, pinkGhost, orangeGhost, mapGame, key);

            if (GlobalVariables.watch.Elapsed.Seconds == 10 && GlobalVariables.switchMode == true && -1 < GlobalVariables.counter && GlobalVariables.counter < 2
            || GlobalVariables.watch.Elapsed.Seconds == 7 && GlobalVariables.switchMode == true && 1 < GlobalVariables.counter && GlobalVariables.counter < 4)
            {
                redGhost.Mode = "Chase";
                blueGhost.Mode = "Chase";
                pinkGhost.Mode = "Chase";
                orangeGhost.Mode = "Chase";
                GlobalVariables.watch.Restart();
                GlobalVariables.switchMode = false;
                GlobalVariables.counter++;

            }
            else if (GlobalVariables.watch.Elapsed.Seconds == 27 && GlobalVariables.switchMode == false && GlobalVariables.counter < 4)
            {
                redGhost.Mode = "Scatter";
                blueGhost.Mode = "Scatter";
                pinkGhost.Mode = "Scatter";
                orangeGhost.Mode = "Scatter";
                GlobalVariables.watch.Restart();
                GlobalVariables.switchMode = true;
            }

        }

        public void Move(Player player, Red ghost, Blue ghost1, Pink ghost2, Orange ghost3, char[][] mapGame, ConsoleKeyInfo key)
        {
            if (ghost1.Step == 2 && (ghost1.Mode == "Scatter" || ghost1.Mode == "Chase"))
            {
                ghost1.SecondMove('B');
                ghost1.Step++;
            }
            else if (ghost1.Step == 3 && (ghost1.Mode == "Scatter" || ghost1.Mode == "Chase"))
            {
                ghost1.ThirdMove(player,'B');
                ghost1.Step++;
            }
            else if (ghost1.Step== 4 && (ghost1.Mode == "Scatter" || ghost1.Mode == "Chase"))
            {
                ghost1.FourthMove('B');
                ghost1.Step++;
            }
            else
            {
                if (ghost1.Mode == "Scatter")
                {
                    ghost1.Move(ghost1.Scatter(), mapGame);
                }
                else if (ghost1.Mode == "Chase")
                {
                    ghost1.Move(ghost1.Chase(player, key, ghost), mapGame);
                }
                else if (ghost1.Mode == "Frightened")
                {

                    ghost1.Move(ghost1.Frightened(), mapGame);
                }
                else if (ghost1.Mode == "Eaten" && ghost1.blueCounter < 3)
                {
                    ghost1.Previousx = 12;
                    ghost1.Previousy = 22;
                    ghost1.Move(up, mapGame);
                    ghost1.blueCounter++;
                }
                else if (ghost1.Mode == "Eaten" && ghost1.blueCounter == 3)
                {
                    ghost1.Mode = "Chase";
                    ghost1.Move(ghost1.Chase(player, key, ghost), mapGame);
                    ghost1.blueCounter = 0;
                }
            }
            if (ghost2.Step == 2 && (ghost2.Mode == "Scatter" || ghost2.Mode == "Chase"))
            {
                ghost2.SecondMove('P');
                ghost2.Step++;
            }
            else if (ghost2.Step == 3 && (ghost2.Mode == "Scatter" || ghost2.Mode == "Chase"))
            {
                ghost2.ThirdMove(player,'P');
                ghost2.Step++;
            }
            else if (ghost2.Step == 4 && (ghost2.Mode == "Scatter" || ghost2.Mode == "Chase"))
            {
                ghost2.FourthMove('P');
                ghost2.Step++;
            }
            else
            {
                if (ghost2.Mode == "Scatter")
                {
                    ghost2.Move(ghost2.Scatter(), mapGame);
                }
                else if (ghost2.Mode == "Chase")
                {
                    ghost2.Move(ghost2.Chase(player, key), mapGame);
                }
                else if (ghost2.Mode == "Frightened")
                {

                    ghost2.Move(ghost2.Frightened(), mapGame);
                }
                else if (ghost2.Mode == "Eaten" && ghost2.pinkCounter < 3)
                {
                    ghost2.Previousx = 12;
                    ghost2.Previousy = 22;
                    ghost2.Move(up, mapGame);
                    ghost2.pinkCounter++;
                }
                else if (ghost2.Mode == "Eaten" && ghost2.pinkCounter == 3)
                {
                    ghost2.Mode = "Chase";
                    ghost2.Move(ghost2.Chase(player, key), mapGame);
                    ghost2.pinkCounter = 0;
                }

            }
            if (ghost3.Step == 2 && (ghost3.Mode == "Scatter" || ghost3.Mode == "Chase"))
            {
                ghost3.SecondMove('O');
                ghost3.Step++;
            }
            else if (ghost3.Step == 3 && (ghost3.Mode == "Scatter" || ghost3.Mode == "Chase"))
            {
                ghost3.ThirdMove(player,'O');
                ghost3.Step++;
            }
            else if (ghost3.Step == 4 && (ghost3.Mode == "Scatter" || ghost3.Mode == "Chase"))
            {
                ghost3.FourthMove('O');
                ghost3.Step++;
            }
            else
            {
                if (ghost3.Mode == "Scatter")
                {
                    ghost3.Move(ghost3.Scatter(), mapGame);
                }
                else if (ghost3.Mode == "Chase")
                {
                    ghost3.Move(ghost3.Chase(player), mapGame);
                }
                else if (ghost3.Mode == "Frightened")
                {

                    ghost3.Move(ghost3.Frightened(), mapGame);
                }
                else if (ghost3.Mode == "Eaten" && ghost3.orangeCounter < 3)
                {
                    ghost3.Previousx = 12;
                    ghost3.Previousy = 22;
                    ghost3.Move(up, mapGame);
                    ghost3.orangeCounter++;
                }
                else if (ghost3.Mode == "Eaten" && ghost3.orangeCounter == 3)
                {
                    ghost3.Mode = "Chase";
                    ghost3.Move(ghost3.Chase(player), mapGame);
                    ghost3.orangeCounter = 0;
                }

            }


            if (ghost.Step == 2 && (ghost.Mode=="Scatter" || ghost.Mode=="Chase") )
            {
                ghost.Move(left, mapGame);
                ghost.Step++;
            }
            else if (ghost.Step == 3 && (ghost.Mode == "Scatter" || ghost.Mode == "Chase"))
            {
                ghost.Move(left, mapGame);
                ghost.Step++;
            }
            else if (ghost.Step == 4 && (ghost.Mode == "Scatter" || ghost.Mode == "Chase"))
            {
                ghost.Move(left, mapGame);
                ghost.Step++;
            }
            else
            {
                if (ghost.Mode == "Scatter")
                {
                    ghost.Move(ghost.Scatter(), mapGame);
                }
                else if (ghost.Mode == "Chase")
                {
                    ghost.Move(ghost.Chase(player), mapGame);
                }
                else if (ghost.Mode == "Frightened")
                {
                    ghost.Move(ghost.Frightened(), mapGame);
                }
                else if (ghost.Mode == "Eaten" && ghost.redCounter < 3)
                {
                    ghost.Previousx = 12;
                    ghost.Previousy = 22;
                    ghost.Move(up, mapGame);
                    ghost.redCounter++;
                }
                else if (ghost.Mode == "Eaten" && ghost.redCounter == 3)
                {
                    ghost.Mode = "Chase";
                    ghost.Move(ghost.Chase(player), mapGame);
                    ghost.redCounter = 0;
                }


            }

                if (player.frightwatch.Elapsed.Seconds == 10)
                {
                    ghost.redCounter = 0;
                    ghost1.blueCounter = 0;
                    ghost2.pinkCounter = 0;
                    ghost3.orangeCounter = 0;
                    ghost.Mode = "Chase";
                    ghost1.Mode = "Chase";
                    ghost2.Mode = "Chase";
                    ghost3.Mode = "Chase";
                    GlobalVariables.interval = 711;
                    GlobalVariables.watch.Start();
                    player.frightwatch.Stop();

                }

        }

        public void IsCatched(Player player, Red redGhost, Pink pinkGhost, Blue blueGhost, Orange orangeGhost)
        {

            if (redGhost.Column == player.Column && redGhost.Row == player.Row
               || blueGhost.Column == player.Column && blueGhost.Row == player.Row
               || pinkGhost.Column == player.Column && pinkGhost.Row == player.Row
               || orangeGhost.Column == player.Column && orangeGhost.Row == player.Row
                ||
                   (player.Previousx == redGhost.Row && player.Previousy == redGhost.Column && redGhost.Previousx == player.Row && redGhost.Previousy == player.Column
                   || player.Previousx == blueGhost.Row && player.Previousy == blueGhost.Column && blueGhost.Previousx == player.Row && blueGhost.Previousy == player.Column)
                   || (player.Previousx == pinkGhost.Row && player.Previousy == pinkGhost.Column && pinkGhost.Previousx == player.Row && pinkGhost.Previousy == player.Column)
                   || (player.Previousx == orangeGhost.Row && player.Previousy == orangeGhost.Column && orangeGhost.Previousx == player.Row && orangeGhost.Previousy == player.Column))
            {

                int temp = 0;


                if ((redGhost.Column == player.Column && redGhost.Row == player.Row
                    || player.Previousx == redGhost.Row && player.Previousy == redGhost.Column && redGhost.Previousx == player.Row && redGhost.Previousy == player.Column)
                    && redGhost.Mode == "Frightened" )
                {

                    temp += 200;
                    player.Score += temp;
                    redGhost.Row = 14;
                    redGhost.Column = 22;
                    redGhost.Mode = "Eaten";
                }
                else if ((pinkGhost.Column == player.Column && pinkGhost.Row == player.Row
                   || player.Previousx == pinkGhost.Row && player.Previousy == pinkGhost.Column && pinkGhost.Previousx== player.Row && pinkGhost.Previousy == player.Column)
                   && pinkGhost.Mode == "Frightened")
                {
                    temp += 200;
                    player.Score += temp;
                    pinkGhost.Row = 14;
                    pinkGhost.Column = 22;
                    pinkGhost.Mode = "Eaten";
                }
                else if ((blueGhost.Column == player.Column && blueGhost.Row == player.Row
                  || player.Previousx == blueGhost.Row && player.Previousy == blueGhost.Column && blueGhost.Previousx == player.Row && blueGhost.Previousy == player.Column)
                  && blueGhost.Mode == "Frightened")
                {
                    temp += 200;
                    player.Score += temp;
                    blueGhost.Row = 14;
                    blueGhost.Column = 22;
                    blueGhost.Mode = "Eaten";
                }
                else if ((orangeGhost.Column == player.Column && orangeGhost.Row == player.Row
                  || player.Previousx == orangeGhost.Row && player.Previousy == orangeGhost.Column && orangeGhost.Previousx == player.Row && orangeGhost.Previousy == player.Column)
                  && orangeGhost.Mode == "Frightened")
                {
                    temp += 200;
                    player.Score += temp;
                    orangeGhost.Row = 14;
                    orangeGhost.Column = 22;
                    orangeGhost.Mode = "Eaten";
                }

                else
                {
                    player.lifes--;
                    GlobalVariables.watch.Restart();
                    player.Row = 17;
                    player.Column = 22;
                }


            }
        }


        
    }
}
