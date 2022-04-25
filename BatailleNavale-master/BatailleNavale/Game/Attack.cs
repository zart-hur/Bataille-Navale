

using BatailleNavale.View;

namespace BatailleNavale.Game
{
     static class Attack
    {
        /// <summary>
        /// Converti un string de coordonnées (exemple : A1) 
        /// en un tableau d'entier (exemple : tab{1,1})
        /// </summary>
        /// <param name="input">un string de taille 2 sous la forme lettre+chiffre</param>
        /// <returns>un tableau d'entier de case 2 avec x = tab[0] et y = tab[1]</returns>
        public static int[] convertion(string input)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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


        /// <summary>
        /// Verifie si les coordonnées sont au bon format, dans la grille et non utilisées.
        /// </summary>
        /// <param name="grid">la grille du joueur</param>
        /// <param name="coords">l'input entré par l'adversaire</param>
        /// <returns>true si les coordonnées sont correctes, false sinon</returns>
        public static bool CorrectCoords(Cell[][] grid, string coords)
        {
            if (coords.Count() != 2)
            {
                UtilView.ResetCursorAfterAttack();
                Console.WriteLine("Coords INVALIDES (mauvaise taille)!!");
                return false;
            }
            char letter = (char)coords[0];
            char num = (char)coords[1];
            if (!char.IsLetter(letter) || !char.IsDigit(num))
            {
                UtilView.ResetCursorAfterAttack();
                Console.WriteLine("Coords INVALIDES (mauvais format ou sort de la grille)!!");
                return false;
            }
            else
            { // bon format exemple : C4 ou c4

                int[] coordsInInt = convertion(coords);
                int x = coordsInInt[0];
                int y = coordsInInt[1];

                if (x > 9 || y > 9 )
                {
                    UtilView.ResetCursorAfterAttack();
                    Console.WriteLine("Coords INVALIDES (sort de la grille)!!");
                    return false;
                }
                else //dans la grille
                {
                    Cell myCell = grid[y][x];
                    if (myCell.AlreadyPlayed)
                    {
                        UtilView.ResetCursorAfterAttack();
                        Console.WriteLine("déja torpillé, réentrez des coordonnées");
                        return false;
                    }
                    return true;
                }
            }
        }
        /// <summary>
        /// Verifie si l'adversaire à touché, coulé ou raté mes bateaux.
        /// </summary>
        /// <param name="grid"> la grille de mon joueur</param>
        /// <param name="Allboats"> le tableau de bateaux de mon joueur</param>
        /// <param name="coords"> les coordonnées données par l'input (adversaire)</param>
        /// <returns>un entier : 0 pour raté, 1 pour touché, 2 pour coulé.</returns>
        public static int BeenTarget(Cell[][] grid, List<Boat> Allboats, int[] coords)
        {
            Cell myCell = grid[coords[0]][coords[1]];
            if (myCell.NumBoat == -1)
            {
                UtilView.ResetCursorAfterAttack();
                Console.WriteLine("loupé :(");
                myCell.AlreadyPlayed = true;
                return 0;
            }
            else
            {
                UtilView.ResetCursorAfterAttack();
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
        /// <summary>
        /// Indique le résultat de l'attaque à l'attaquant.
        /// </summary>
        /// <param name="answer">code resultat: 0=raté, 1=touché,2=coulé</param>
        /// <param name="gridOpponent">grille de l'adversaire</param>
        /// <param name="coords">les coords de l'attaque</param>
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




    }
}
