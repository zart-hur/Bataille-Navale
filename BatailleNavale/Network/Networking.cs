

using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace BatailleNavale.Network
{
    public class Networking
    {
        public static string input { get; set; } = "";
        //string serverIp = "0.0.0.0";
        //int serverPort = 12345;

        ////string SServerIp = "0.0.0.0";
        int SServerPort = 9000;
        public static int recv { get; set; }

        bool numvalide { get; set; } = false;
        char letter { get; set; } = ' ';
        char num { get; set; } = ' ';
        Player PlayerMy;


        public Networking(Player player)
        {
            this.PlayerMy = player;
        }

        public void Server(string ServerIp, int ServerPort)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] data = new byte[1024];

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[3];

            IPEndPoint sEndPoint = new IPEndPoint(IPAddress.Parse("" + ServerIp + ""), ServerPort);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(sEndPoint);
            serverSocket.Listen(10);
            Console.WriteLine("\nSERVEUR DEMARRE : EN ATTENTE D'UNE CONNEXION CLIENTE");


            Socket client = serverSocket.Accept();
            Player playerMyServer = new Player();
            try
            {
                //client = serverSocket.Accept();
                Console.WriteLine("\nUn client vient de se connecter");
                Console.Clear();

                InitGame initGame = new InitGame();

                Console.WriteLine("\nCommencer la Partie : L'adversaire Joue en Premier");

                //Send Welcome message to the Client
                string welcome = "\nBienvenue sur mon serveur du jeu Bataille Navale";
                data = Encoding.UTF8.GetBytes(welcome);
                client.Send(data, data.Length, SocketFlags.None);

                playerMyServer = initGame.getPlayerMy(); //recupere le playerMy
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur survenue");
            }

            while (true)
            {
                data = new byte[1024];

                //Read data From the client
                try
                {
                    recv = client.Receive(data);
                }
                catch (Exception)
                {
                    Console.WriteLine(" Erreur : Client Fermé.");
                }


                string AttackReceived = Encoding.UTF8.GetString(data, 0, recv);
                //VERIF LES COORDONNEES et affiche l'attaque puis send au client le resultat de l'attaque.
                int[] coordsInInt = Game.Attack.convertion(AttackReceived); //convertie en int depuis string
                string AttackResultCode = Game.Attack.BeenTarget(playerMyServer.Grid, playerMyServer.ListOfBoats, coordsInInt); //Met dans res le code du resultat de l'attaque 
                View.GridView.UpdateGridAfterAttack(playerMyServer.Grid, playerMyServer.ListOfBoats);//affiche les dommages sur ma grille

                client.Send(Encoding.UTF8.GetBytes(AttackResultCode));

                View.UtilView.ResetCursorAfterAttack();
                Console.WriteLine("\nVotre adversaire a attaque sur les coordonnées :  " + Encoding.UTF8.GetString(data, 0, recv));
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("\nC'est a vous de jouer.");
                Console.WriteLine("\nEntrez des coordonnées d'attaque !");
                Console.ResetColor();

                input = Console.ReadLine();

                //Check if the input string is null ou empty
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("String is either null or empty");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEntrez des coordonnées d'attaque !");
                    input = Console.ReadLine();
                    Console.ResetColor();
                }


                letter = (char)input[0];
                if (input.Length == 2)
                {
                    num = (char)input[1];
                }


                if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
                {
                    numvalide = true;
                }

                bool onlyAlphas = input[0].ToString().All(c => c >= 'a' && c <= 'j' || c >= 'A' && c <= 'J');


                while (input.Length != 2 || !onlyAlphas || !numvalide)
                {

                    if (input.Length != 2)
                    {
                        Console.WriteLine("Erreur sur le Nombre de Carateres : Vous devez saisir 2 caracteres. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }
                    else if (!onlyAlphas)
                    {
                        Console.WriteLine("Erreur sur le 1er caratere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }
                    else if (!numvalide)
                    {
                        Console.WriteLine("Erreur sur le 2e caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }

                    if (input == "exit")
                    {
                        break;
                    }

                    input = Console.ReadLine();
                    letter = (char)input[0];
                    num = (char)input[1];

                    if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
                    {
                        numvalide = true;
                    }

                }

                if (input == "exit")
                    break;

                Console.ForegroundColor = ConsoleColor.Blue;

                client.Send(Encoding.UTF8.GetBytes(input));
                //recoit le code du resutat de son attaque
                //Read data From the client
                data = new byte[1024];
                try
                {
                    recv = client.Receive(data);
                }
                catch (Exception)
                {
                    Console.WriteLine(" Erreur : Client Fermé.");
                }
                string codeRetour = Encoding.UTF8.GetString(data, 0, recv);
                Game.Attack.AnswerTarget(codeRetour, playerMyServer.GridOpponent, Game.Attack.convertion(input));
                
                //client.Send(Encoding.UTF8.GetBytes("redonne la main au client"));
            }
            Console.WriteLine("\nVous êtes deconnectés.");
            client.Close();
            serverSocket.Close();
            Console.ReadLine();
        }

        public void Client(string ServerIp, int ServerPort)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] data = new byte[1024];
            string stringData;

            IPEndPoint sEndPointForClient = new IPEndPoint(IPAddress.Parse("" + ServerIp + ""), ServerPort);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Player playerMyClient;
            try
            {
                serverSocket.Connect(sEndPointForClient);
                Console.WriteLine("\nVous êtes connecté");
                Console.Clear();
                InitGame initGameClient = new InitGame();
                playerMyClient = initGameClient.getPlayerMy(); //recupere le playerMy
                Console.WriteLine("\nDebut de la Partie : Je joue en Premier ");

                //Get Data From the Server
                //int recv = serverSocket.Receive(data);
            }
            catch
            {
                Console.WriteLine("\nImpossible de connecter au serveur ");
                return;
            }


            while (true)
            {
                int recv = serverSocket.Receive(data);
                //Console.WriteLine("code recu :" + recv);
                Console.WriteLine("\nEntrez vos coordonnées d'attaque");
                input = Console.ReadLine();


                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Erreur : Vous n'avez rien saisi. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    input = Console.ReadLine();
                }

                if (input == "exit")
                {
                    break;
                }


                letter = (char)input[0];
                if (input.Length >= 2)
                {
                    num = (char)input[1];
                }


                if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
                {
                    numvalide = true;
                }

                bool onlyAlphas = input[0].ToString().All(c => c >= 'a' && c <= 'j' || c >= 'A' && c <= 'J');
                //int value;


                while (input.Length != 2 || !onlyAlphas || !numvalide)
                {

                    if (input.Length != 2)
                    {
                        Console.WriteLine("Erreur sur le Nombre de Carateres : Vous devez saisir 2 caracteres. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }
                    else if (!onlyAlphas)
                    {
                        Console.WriteLine("Erreur sur le 1er caratere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }
                    else if (!numvalide)
                    {
                        Console.WriteLine("Erreur sur le 2e caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
                    }



                    letter = (char)input[0];
                    num = (char)input[1];
                    if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
                    {
                        numvalide = true;
                    }

                    if (input == "exit")
                    {
                        break;
                    }
                }
                //Attack the Server Gamer // Send to Server
                serverSocket.Send(Encoding.UTF8.GetBytes(input));

                data = new byte[1024];
                try
                {
                    recv = serverSocket.Receive(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client deconecté");
                }
                //recoit le code du resutat de son attaque

                Game.Attack.AnswerTarget(Encoding.UTF8.GetString(data, 0, recv), playerMyClient.GridOpponent, Game.Attack.convertion(input));

                //input = Console.ReadLine();
                //serverSocket.Send(Encoding.UTF8.GetBytes("redonne la main au server apres avoir recu le code answer de l'attaque"));

                data = new byte[1024];
                try
                {
                    recv = serverSocket.Receive(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client deconnecté");
                }
                //Attaque Result Management
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                Console.ForegroundColor = ConsoleColor.Red;

                string AttackReceived = stringData;
                
                //VERIF LES COORDONNEES et affiche l'attaque puis send au client le resultat de l'attaque.
                Console.WriteLine("dans le receve du client : " + AttackReceived);
                int[] coordsInInt = Game.Attack.convertion(AttackReceived); //convertie en int depuis string
                string AttackResultCode = Game.Attack.BeenTarget(playerMyClient.Grid, playerMyClient.ListOfBoats, coordsInInt); //Met dans res le code du resultat de l'attaque 

                View.GridView.UpdateGridAfterAttack(playerMyClient.Grid, playerMyClient.ListOfBoats);//affiche les dommages sur ma grille

                serverSocket.Send(Encoding.UTF8.GetBytes(AttackResultCode));

                View.UtilView.ResetCursorAfterAttack();
                Console.WriteLine("\nVotre adversaire a attaque sur les coordonnées: " + stringData);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nA Vous de Jouer");
                Console.WriteLine("\nEntrez vos coordonnées d'attaque");
                input = Console.ReadLine();

                Console.ResetColor();
            }
            Console.WriteLine("\nDeconnection du Serveur...");
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
            Console.WriteLine("\nVous êtes deconnecté.!");
            Console.ReadLine();
        }
    }
}