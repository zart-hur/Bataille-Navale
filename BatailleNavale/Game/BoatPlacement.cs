using BatailleNavale.View;

namespace BatailleNavale
{
    public static class BoatPlacement
    {
        

        public static void PutBoats(Cell[][] grid, List<Boat> boats)
        {

            GridView.PrintEmptyGrid(4, 4);
            GridView.PrintEmptyGrid(54, 4);
            foreach (var boat in boats)
            {

                PutOneBoat(grid, boat);
                BoatView.PrintBoat(boat, ConsoleColor.Green);
            }
        }

        private static void PutOneBoat(Cell[][] grid, Boat boat)
        {
            //Displays.Display(grid, 1);

            Console.WriteLine("\nEntrez Les coordonnées du bateau X : ");
            int x =-1;
            int numberX;
            if (int.TryParse(Console.ReadLine(), out numberX))
            {
                x = numberX;
            }
            else
            {
                UtilView.ResetCursorAfterAddBoat();
                Console.WriteLine("\nCoords INVALIDES !! (pas des chiffres)");
                
                PutOneBoat(grid, boat);
                return;
            }
            Console.WriteLine("Entrez Les coordonnées du bateau Y : ");
            int y= -1;
            int numberY;
            if (int.TryParse(Console.ReadLine(), out numberY))
            {
                y = numberY;
            }
            else
            {
                UtilView.ResetCursorAfterAddBoat();
                Console.WriteLine("\nCoords INVALIDES !! (pas des chiffres)");
                
                PutOneBoat(grid, boat);
                return;
            }
            if (x < 1 || y < 1 || x > 10 || y > 10)
            {
                UtilView.ResetCursorAfterAddBoat();
                Console.WriteLine("\nCoords INVALIDES !! (hors de la grille)");
                PutOneBoat(grid, boat);
            }
            else
            {
                Cell myCell = grid[x - 1][y - 1];
                if (myCell.NumBoat != -1)
                {
                    UtilView.ResetCursorAfterAddBoat();
                    Console.WriteLine("\nCoords INVALIDES (déja un bateau)!!");
                    PutOneBoat(grid, boat);
                }
                else
                {
                    DirectionChoice(grid, myCell, boat);
                }
            }
        }
        private static void DirectionChoice(Cell[][] grid, Cell myCell, Boat boat)
        {
            Console.WriteLine("Entrez une direction (N,S,E,W) :");
            string direction = Console.ReadLine()!.ToUpper();
            switch (direction)
            {
                case "N":
                    if ((myCell.CoordY - (boat.Size - 1)) < 0)
                    {
                        UtilView.ResetCursorAfterAddBoat();
                        Console.WriteLine("\nca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        { //verif
                            Cell setCell = grid[myCell.CoordX][myCell.CoordY - i];
                            if (myCell.NumBoat != -1)
                            {
                                UtilView.ResetCursorAfterAddBoat();
                                Console.WriteLine("\nplace déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX + 1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY + 1;
                        Char orientation = char.Parse(direction);
                        boat.Orientation = orientation;
                        foreach (Cell cell in listOfCells)
                        { //affectation cellule
                            cell.NumBoat = (int)boat.Id;
                        }
                    }
                    break;
                case "S":
                    if ((myCell.CoordY + boat.Size - 1) > 9)
                    {
                        UtilView.ResetCursorAfterAddBoat();
                        Console.WriteLine("\nca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        {
                            Cell setCell = grid[myCell.CoordX][myCell.CoordY + i];
                            if (myCell.NumBoat != -1)
                            {
                                UtilView.ResetCursorAfterAddBoat();
                                Console.WriteLine("\nplace déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX + 1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY + 1;
                        Char orientation = char.Parse(direction);
                        boat.Orientation = orientation;
                        foreach (Cell cell in listOfCells)
                        {
                            cell.NumBoat = (int)boat.Id;
                        }
                    }
                    break;
                case "E":
                    if ((myCell.CoordX + boat.Size - 1) > 9)
                    {
                        UtilView.ResetCursorAfterAddBoat();
                        Console.WriteLine("\nca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        {
                            Cell setCell = grid[myCell.CoordX + i][myCell.CoordY];
                            if (myCell.NumBoat != -1)
                            {
                                UtilView.ResetCursorAfterAddBoat();
                                Console.WriteLine("\nplace déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX + 1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY + 1;
                        Char orientation = char.Parse(direction);
                        boat.Orientation = orientation;
                        foreach (Cell cell in listOfCells)
                        {
                            cell.NumBoat = (int)boat.Id;
                        }
                    }
                    break;
                case "W":
                    if ((myCell.CoordX - (boat.Size - 1)) < 0)
                    {
                        UtilView.ResetCursorAfterAddBoat();
                        Console.WriteLine("\nca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        {
                            Cell setCell = grid[myCell.CoordX - i][myCell.CoordY];
                            if (myCell.NumBoat != -1)
                            {
                                UtilView.ResetCursorAfterAddBoat();
                                Console.WriteLine("\nplace déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX + 1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY + 1;
                        Char orientation = char.Parse(direction);
                        boat.Orientation = orientation;
                        foreach (Cell cell in listOfCells)
                        {
                            cell.NumBoat = (int)boat.Id;
                        }
                    }
                    break;
                default:
                    UtilView.ResetCursorAfterAddBoat();
                    Console.WriteLine("\nceci n'est pas une direction valide !");
                    PutOneBoat(grid, boat);
                    break;
            }
        }



    }
}
