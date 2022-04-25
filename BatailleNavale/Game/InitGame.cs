using BatailleNavale.View;

namespace BatailleNavale;

public class InitGame
{
    Player playerMy = new Player();
    
    public InitGame()
    {
       // Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

       



        playerMy.InitGrids();
        playerMy.InitBoats();
       


        BoatPlacement.PutBoats(playerMy.Grid, playerMy.ListOfBoats);//placement de bateaux

        Console.WriteLine();//aller a la ligne
        /*while (true)
        {

            string? coords = Console.ReadLine();



            if (Game.Attack.CorrectCoords(playerMy.Grid, coords!))
            {
                int[] coordsInInt = Game.Attack.convertion(coords!);

                int res = Game.Attack.BeenTarget(playerMy.Grid, playerMy.ListOfBoats, coordsInInt);

                //Game.Attack.AnswerTarget(res, Opponent.Grid, coordsInInt);
            }
            GridView.UpdateGridAfterAttack(playerMy.Grid, playerMy.ListOfBoats);
            UtilView.ResetCursorAfterAttack();

        }*/


      //  UtilView.WriteAt("All done!", 0, 50, ConsoleColor.Green);
    }

    public Player getPlayerMy()
    {
        return playerMy;
    }
}



