using BatailleNavale.View;

namespace BatailleNavale
{
    public class Program
    {

        public static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            /*
                        Player playerMy = new Player();
                        Player Opponent = new Player();



                        playerMy.InitGrid();
                        playerMy.InitBoat();
                        Opponent.InitGrid();





                        *//*GridView.PrintEmptyGrid(0, 0);
                        GridView.PrintEmptyGrid(50, 0);
                        for (int i = 0; i < playerMy.ListOfBoats.Count; i++)
                        {
                            playerMy.Grid[i][i].NumBoat = i;
                            Boat boat = playerMy.ListOfBoats.ElementAt(i);
                            boat.x0 = i + 1;
                            boat.y0 = i + 1;
                            boat.Orientation = 'S';
                            BoatView.PrintBoat(boat, ConsoleColor.Green);

                        }*//*

                        BoatPlacement.PutBoats(playerMy.Grid, playerMy.ListOfBoats);//placement de bateaux

                        Console.WriteLine();//aller a la ligne
                        while (true)
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

                        }
                        */
            MenuView menuvue = new();
            UtilView.WriteAt("All done!", 0, 50, ConsoleColor.Green);

        }

    }
}