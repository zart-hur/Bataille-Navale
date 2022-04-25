using BatailleNavale.View;

namespace BatailleNavale
{
    public static class Process
    {

        public static int[] convertion(string input)
        {
            string alphabet = "ABCDEFGHIJKLMNOP";
            int i = 0;
            char lettre = input.ElementAt(0);
            char str = input.ElementAt(1);
            while (lettre != alphabet[i])
                i++;
            int[] nb = { 0, 1 };
            nb[0] = i;
            nb[1] = str - '0';
            return nb;
        }



        public static bool CorrectCoords(Cell[][] grid, string coords)
        {
            if (coords.Count() != 2)
            {
                Console.WriteLine("Coords INVALIDES (mauvaise taille)!!");
                return false;
            }
            char letter = (char)coords[0];
            char num = (char)coords[1];
            if (!char.IsLetter(letter) || !char.IsDigit(num))
            {
                Console.WriteLine("Coords INVALIDES (mauvais format ou sort de la grille)!!");
                return false;
            }
            else
            { // bon format exemple : C4 ou c4

                int[] coordsInInt = convertion(coords);
                int x = coordsInInt[0];
                int y = coordsInInt[1];

                if (y > 9)
                {
                    Console.WriteLine("Coords INVALIDES (sort de la grille)!!");
                    return false;
                }
                else //dans la grille
                {
                    Cell myCell = grid[y][x];
                    if (myCell.AlreadyPlayed)
                    {
                        Console.WriteLine("déja torpillé, réentrez des coordonnées");
                        return false;
                    }
                    return true;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"> la grille de mon joueur</param>
        /// <param name="Allboats"> le tableau de bateaux de mon joueur</param>
        /// <param name="coords"> les coordonnées données par l'input</param>
        /// <returns></returns>
        public static int BeenTarget(Cell[][] grid, List<Boat> Allboats, int[] coords)
        {

            Cell myCell = grid[coords[0]][coords[1]];

            if (myCell.NumBoat == -1)
            {
                Console.WriteLine("loupé :(");
                myCell.AlreadyPlayed = true;
                return 0;
            }
            else
            {
                Boat theBoat = new(0, 0); //0 et 0 ne sont que temporaires
                foreach (Boat boat in Allboats)
                {
                    if (boat.Id == myCell.NumBoat)
                        theBoat = boat;
                }
                theBoat.Health--;
                if (theBoat.Health == 0)
                {
                    myCell.AlreadyPlayed = true;
                    return 2; //coulé !
                }
                myCell.AlreadyPlayed = true;
                return 1; // touché !

            }

        }

        public static void AnswerTarget(int answer, Cell[][] gridOpponent, int[] coords)
        {
            Cell myCell = gridOpponent[coords[0]][coords[1]];
            switch (answer)
            {
                case 0:
                    Console.WriteLine("loupé");
                    break;
                case 1:
                    Console.WriteLine("touché");
                    break;
                case 2:
                    Console.WriteLine("coulé");
                    break;
            }
        }



        /////////////////////////////// PLACEMENT BATEAUX ////////////////////////////////////////////
        public static void PutBoats(Cell[][] grid, List<Boat> boats)
        {

            GridView.PrintEmptyGrid(0, 0);
            GridView.PrintEmptyGrid(50, 0);
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
            int x = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Entrez Les coordonnées du bateau Y : ");
            int y = int.Parse(Console.ReadLine()!);

            if (x < 1 || y < 1 || x > 10 || y > 10)
            {
                Console.Clear();
                Console.WriteLine("Coords INVALIDES !!");
                PutOneBoat(grid, boat);
            }
            else
            {
                Cell myCell = grid[x - 1][y - 1];
                if (myCell.NumBoat != -1)
                {
                    Console.Clear();
                    Console.WriteLine("Coords INVALIDES !!");
                    PutOneBoat(grid, boat);
                }
                else
                {
                    DirectionChoice(grid, myCell, boat);
                    //Console.Clear();

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
                        Console.WriteLine("ca sort de la grille !");
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
                                Console.WriteLine("place déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX+1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY+1;
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
                        Console.WriteLine("ca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        {
                            Cell setCell = grid[myCell.CoordX ][myCell.CoordY + i];
                            if (myCell.NumBoat != -1)
                            {
                                Console.WriteLine("place déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX+1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY+1;
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
                        Console.WriteLine("ca sort de la grille !");
                        PutOneBoat(grid, boat);
                    }
                    else
                    {
                        List<Cell> listOfCells = new();
                        for (int i = 0; i < boat.Size; i++)
                        {
                            Cell setCell = grid[myCell.CoordX + i][myCell.CoordY ];
                            if (myCell.NumBoat != -1)
                            {
                                Console.WriteLine("place déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX+1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY+1;
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
                        Console.WriteLine("ca sort de la grille !");
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
                                Console.WriteLine("place déja occupée");
                                PutOneBoat(grid, boat);
                            }
                            else
                                listOfCells.Add(setCell);
                        }
                        boat.x0 = listOfCells.ElementAt(0).CoordX+1;
                        boat.y0 = listOfCells.ElementAt(0).CoordY+1;
                        Char orientation = char.Parse(direction);
                        boat.Orientation = orientation;
                        foreach (Cell cell in listOfCells)
                        {
                            cell.NumBoat = (int)boat.Id;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("ceci n'est pas une direction valide !");
                    DirectionChoice(grid, myCell, boat);
                    break;
            }
        }



    }
}
