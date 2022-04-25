using BatailleNavale.Network;
using BatailleNavale.View;

namespace BatailleNavale
{
    public class Program
    {

        public static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);



            //reseau-process

            MenuView menuvue = new();
           // GetAddrIP.GetIp();

            //affichage -prosecc


            //Process.PutBoats(playerMy.Grid, playerMy.ListOfBoats);//placement de bateaux


            UtilView.WriteAt("connexion terminée!", 0, 50, ConsoleColor.Red);
            //Console.WriteLine();


        }

    }
}