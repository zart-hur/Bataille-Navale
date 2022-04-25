using BatailleNavale.Network;

using System.Text.Json;

namespace BatailleNavale.View;

 class MainMenu
{
    //public static  MenuDisplays Display = new ();
    static Player playerMy = new Player();
    static Player Opponent = new Player();


    static string? ipServer = "0.0.0.0";
    static string portServer = "12345";
    static string? stPortServer = "12345";
    static string? stPortServerFromClient = "12345";

    public  MainMenu()
	{
        playerMy.InitGrid();
        playerMy.InitBoat();
        Opponent.InitGrid();
    }
    public  void ChooseMenuItems()
    {
        ContentStyle.TitleText("Bienvenue dans la Bataiile Navale");
        ContentStyle.LineText("Veuillez choisir un des choix suivants : ");
        ContentStyle.UnordoredList("1 - Jouer à la Bataille Navale");
        ContentStyle.UnordoredList("2 - Quitter le jeu");

        // Console.WriteLine("Erreur la valeur ne correspond a aucun choix");
        string? choixmenu = Console.ReadLine();

        while (choixmenu != "1" && choixmenu != "2")
        {
            Console.WriteLine("Vous devez entrer 1 ou 2");
            choixmenu = Console.ReadLine();
        }
        int menuNumber = Convert.ToInt32(choixmenu);

        switch (menuNumber)
        {
            case 1:
                GameMenu();
                break;
            case 2:
                QuitBattleship();
                break;
            default:
                //  Display.Clear(" Erreur la valeur ne correspond a aucun choix");
                break;
        }
    }


    public  void GameMenu()
    {
        ContentStyle.TitleText("Menu du Jeux", ConsoleColor.Green);
        ContentStyle.LineText("Veuillez choisir un des choix suivants : ");
        ContentStyle.UnordoredList("1 - demmarrer comme serveur");
        ContentStyle.UnordoredList("2 - demarrer comme Client");
        ContentStyle.UnordoredList("3- Revenir au menu principal");
        Console.ResetColor();

        string? choixmenu = Console.ReadLine();
        while (choixmenu != "1" && choixmenu != "2" && choixmenu != "3")
        {
            Console.WriteLine("Vous devez entrer 1 ou 2 ou 3");
            choixmenu = Console.ReadLine();
        }


        int menuNumber = Convert.ToInt32(choixmenu);

        //Process.PutBoats(playerMy.Grid, playerMy.ListOfBoats)

        switch (menuNumber)
        {
            case 1:
                ContentStyle.TitleText(" Start As Server !");

                //Fournir l'adresse Ip à utiliser pour le serveur
               // Console.WriteLine("Entre l'adresse Ip du Serveur");
               // string? ipServer = Console.ReadLine();

                //Fournir l'adresse Ip à utiliser pour le serveur
               //// Console.WriteLine("Entre le port à utiliser pour le Serveur");
                //string? stPortServer = Console.ReadLine();
                int portServer = Convert.ToInt32(stPortServer);
                Networking networking = new(playerMy);
                networking.Server(ipServer!, portServer!);

                break;

            case 2:
                ContentStyle.TitleText(" Start As Client !");

                //Fournir l'adresse Ip à utiliser pour le serveur
                Console.WriteLine("Entre l'adresse Ip du Serveur");
                string? ipServerFromClient = Console.ReadLine();

                //Fournir l'adresse Ip à utiliser pour le serveur
               // Console.WriteLine("Entre le port à utiliser pour le Serveur");
               // string? stPortServerFromClient = Console.ReadLine();
                
                int portServerFromClient = Convert.ToInt32(stPortServerFromClient);

               // Networking networkingClient = new(playerMy);
                //networkingServer.Client(ipServer!, portServer!);

                Networking networkingClient = new(playerMy);
                networkingClient.Client(ipServerFromClient!, portServerFromClient!);

                break;

            case 3:
                // StartAsClient();
                //ContentStyle.TitleText(" Start As Server !");
                ChooseMenuItems();
                break;

            default:
                // Display.Clear(" Erreur la valeur ne correspond a aucun choix");
                break;
        }
    }
    /* static void GameMenu()
     {
         Display.Clear();

       // je récupère toutes les informations liées à la nouvelle question 
         Console.WriteLine(" Jouer a la Bataille Navale :");
         string? query = Console.ReadLine();
     }*/

    static void QuitBattleship()
    {
        // Display.Clear();
        ContentStyle.TitleText(" Aurevoir. Merci d'avoir jouer !");


    }
}
