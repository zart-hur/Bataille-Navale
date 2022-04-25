
using System.Drawing;

namespace BatailleNavale.View
{
    public class View
    {
        public const int DIMENTION_OF_CELL = 3;
    }

    public class MenuView
    {
        public MenuView()
        {
            MainMenu menu = new MainMenu();
            menu.ChooseMenuItems();
        }

    }

    internal class CellView
    {
        public static void PrintCell(int x0, int y0, Char letter, ConsoleColor consoleColor)
        {

            //du haut vers le bas, de gauche a droite
            UtilView.WriteAt("┌─┐", x0, y0, consoleColor);
            UtilView.WriteAt("│" + letter + "│", x0, y0 + 1, consoleColor);
            UtilView.WriteAt("└─┘", x0, y0 + 2, consoleColor);
        }
    }
    internal class GridView
    {
        public static void PrintEmptyGrid(int x0, int y0)
        {
            //Axes
            for (int i = 0; i <= 9; i++)
            {
                CellView.PrintCell(View.DIMENTION_OF_CELL + x0 - 6, i * View.DIMENTION_OF_CELL + y0, char.Parse(i + ""), ConsoleColor.DarkGray);
            }
            for (int i = 0; i <= 9; i++)//char A vaut 65
            {
                CellView.PrintCell(i * View.DIMENTION_OF_CELL + x0, View.DIMENTION_OF_CELL + y0 - 6, Convert.ToChar(i + 65), ConsoleColor.DarkGray);
            }

            //Grille
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    CellView.PrintCell(x * View.DIMENTION_OF_CELL + x0, y * View.DIMENTION_OF_CELL + y0, ' ', ConsoleColor.Blue);
                }
            }
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">la grille à parcourir</param>
        /// <param name="boats"> la liste de bateaux</param>
        public static void UpdateGridAfterAttack(Cell[][] grid, List<Boat> boats)
        {

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Cell cell = grid[i][j];
                    Point pointUserInput = new Point(i + 1, j + 1);
                    Point pointConverted = UtilView.ConvertPoints(pointUserInput);

                    if (cell.AlreadyPlayed)
                    {
                        if (cell.NumBoat == -1)
                        {
                            CellView.PrintCell(pointConverted.X, pointConverted.Y, 'X', ConsoleColor.Blue);

                            //mettre X;
                        }
                        else
                        {
                            foreach (Boat boat in boats)
                            {
                                if (boat.Id == cell.NumBoat)
                                {
                                    if (boat.Health > 0)
                                    {
                                        CellView.PrintCell(pointConverted.X, pointConverted.Y, boat.Name, ConsoleColor.Yellow);
                                        //mettre orange;
                                    }
                                    else
                                    {
                                        CellView.PrintCell(pointConverted.X, pointConverted.Y, boat.Name, ConsoleColor.Red);
                                    }

                                    //mettre rouge;
                                }
                            }
                        }
                    }
                }
            }




        }
        public static void UpdateGridAfterBeAttacked(Cell[][] grid, List<Boat> boats)
        {

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Cell cell = grid[i][j];
                    Point pointUserInput = new Point(i + 1, j + 1);
                    Point pointConverted2 = UtilView.ConvertPointsGrid2(pointUserInput);

                    if (cell.AlreadyPlayed)
                    {
                        if (cell.NumBoat == -1)
                        {
                            CellView.PrintCell(pointConverted2.X, pointConverted2.Y, 'X', ConsoleColor.Blue);

                            //mettre X;
                        }
                        else
                        {
                            foreach (Boat boat in boats)
                            {
                                if (boat.Id == cell.NumBoat)
                                {
                                    if (boat.Health > 0)
                                    {
                                        CellView.PrintCell(pointConverted2.X, pointConverted2.Y, boat.Name, ConsoleColor.Yellow);
                                        //mettre orange;
                                    }
                                    else
                                    {
                                        CellView.PrintCell(pointConverted2.X, pointConverted2.Y, boat.Name, ConsoleColor.Red);
                                    }

                                    //mettre rouge;
                                }
                            }
                        }
                    }
                }
            }




        }
    }
    public class BoatView
    {
        public static void PrintBoat(Boat boat, ConsoleColor consoleColor)
        {
            if (boat.x0 != -1 || boat.y0 != -1)
            {
                Point pointUserInput = new Point(boat.x0, boat.y0);
                Point pointConverted = UtilView.ConvertPoints(pointUserInput);

                CellView.PrintCell(pointConverted.X, pointConverted.Y, boat.Name, consoleColor);

                for (int i = 1; i < boat.Size; i++)
                {
                    switch (boat.Orientation)
                    {
                        case 'N':
                            CellView.PrintCell(pointConverted.X, pointConverted.Y - (View.DIMENTION_OF_CELL * (i)), boat.Name, consoleColor);
                            break;
                        case 'S':
                            CellView.PrintCell(pointConverted.X, pointConverted.Y + (View.DIMENTION_OF_CELL * (i)), boat.Name, consoleColor);
                            break;
                        case 'E':
                            CellView.PrintCell(pointConverted.X + (View.DIMENTION_OF_CELL * (i)), pointConverted.Y, boat.Name, consoleColor);
                            break;
                        case 'W':
                            CellView.PrintCell(pointConverted.X - (View.DIMENTION_OF_CELL * (i)), pointConverted.Y, boat.Name, consoleColor);
                            break;
                        default:
                            Console.WriteLine("pas d'orientation");
                            break;
                    }
                }
                UtilView.ResetCursorAfterAddBoat();
            }
        }
        public static void PrintAllBoats(Player player)
        {
            foreach (var boat in player.ListOfBoats)
            {
                PrintBoat(boat, ConsoleColor.Yellow);
            }
        }


        //    public static void UpdateGrid(Player player)
        //    {
        //    //boat
        //    Name 
        //    Id 
        //    Size 
        //    Health 
        //    x0 
        //    y0 
        //    Char 

        //    //cell
        //    CoordX 
        //    CoordY 
        //    NumBoat 
        //    AlreadyPlayed 

        //    //player
        //    Name 
        //    SizesBoats 
        //    ListOfBoats 
        //    Grid 


    }
    internal class UtilView
    {
        public static int origRow { get; set; }
        public static int origCol { get; set; }

        public static void ResetCursorAfterAddBoat()
        {
            Console.SetCursorPosition(0, 34);
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.SetCursorPosition(0, 33);
        }

        public static void ResetCursorAfterAttack()
        {
            Console.SetCursorPosition(0, 34);
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.WriteLine("                                           ");
            Console.SetCursorPosition(0, 34);
        }
        public static void WriteAt(string s, int x, int y, ConsoleColor backgroundColor)
        {
            try
            {
                Console.BackgroundColor = backgroundColor;
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s, Console.BackgroundColor);
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }
        public static Point ConvertPoints(Point pointIn)
        {
            //in = de Game, out = dans View
            Point pointOut = new Point();

            pointOut.X = pointIn.X * View.DIMENTION_OF_CELL - View.DIMENTION_OF_CELL + 4;
            pointOut.Y = pointIn.Y * View.DIMENTION_OF_CELL - View.DIMENTION_OF_CELL + 4;

            return pointOut;
        }
        public static Point ConvertPointsGrid2(Point pointIn)
        {
            //in = de Game, out = dans View
            Point pointOut = new Point();

            pointOut.X = pointIn.X * View.DIMENTION_OF_CELL - View.DIMENTION_OF_CELL + 54;
            pointOut.Y = pointIn.Y * View.DIMENTION_OF_CELL - View.DIMENTION_OF_CELL + 4;

            return pointOut;
        }
    }
}


