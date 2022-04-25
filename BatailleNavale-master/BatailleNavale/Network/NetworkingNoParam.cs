using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace BatailleNavale.Network
{
	public class NetworkingNoParam
	{
		public static string input { get; set; } = "";
		string serverIp = "0.0.0.0";
		int serverPort = 12345;
		//int recv;

		string SServerIp = "0.0.0.0";
		int SServerPort = 12345;
		public static int recv { get; set; }

		bool numvalide { get; set; } = false;
		char letter { get; set; } = ' ';
		char num { get; set; } = ' ';

		public void Server()
		{
			Console.OutputEncoding = Encoding.UTF8;
			//int recv;
			byte[] data = new byte[1024];

			/*      IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.51"), 49221);
                  Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                  IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                  IPAddress ipAddress = ipHostInfo.AddressList[3];

                  IPEndPoint serverEP = new IPEndPoint(ipAddress, 12345);*/


			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[3];

			//IPEndPoint serverEP = new IPEndPoint(ipAddress, 12345);
			IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 12345);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			serverSocket.Bind(serverEP);
			serverSocket.Listen(10);
			Console.WriteLine("\nSERVEUR DEMARRE : EN ATTENTE D'UNE CONNEXION CLIENTE");

			Socket client = serverSocket.Accept();
			Console.WriteLine("\nUn client vient de se conecter");
			Console.WriteLine("\nCommencer la Partie : L'adversaire Joue en Premier");

			string welcome = "\nBienvenue sur mon serveur du jeu Bataille Navale";
			data = Encoding.UTF8.GetBytes(welcome);
			client.Send(data, data.Length, SocketFlags.None);

			//string input;
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
				

				Console.ForegroundColor = ConsoleColor.Red;
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


				//First ans Second Letter of input
				//char inputFirdtLetter = input.ToArray()[0];
				//char inputSecondtLetter = input.ToArray()[0];

				//check input lenght
				/*while (input.Length != 2)
				{
					Console.WriteLine("Erreur sur la Longeur : Vous devez saisir deux carateres en tout. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
					input = Console.ReadLine();
				}

				
				//while (!Regex.IsMatch(inputFirdtLetter.ToString(), "^[a-z]+$", RegexOptions.IgnoreCase))
				while (!onlyAlphas)
				{
					Console.WriteLine("Erreur sur le 1er caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
					input = Console.ReadLine();
				}*/

				//Check if input first Letter is Alphbetic / True if Alpha
				//bool onlyAlphas = input[0].ToString().All(c => c >= 'a' && c <= 'j' || c >= 'A' && c <= 'J');

				//Check if input first Letter is a number . True if number
				//bool onlyNumber = input[1].ToString().All(c => c >= '0' && c <= '9');


/*  while (input.Length != 2 || !onlyAlphas || !onlyNumber)
  {
      if (input.Length != 2)
      {
          Console.WriteLine("Erreur sur le Nombre de Carateres : Vous devez saisir 2 caracteres. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
          input = Console.ReadLine();
      }
      else if (!onlyAlphas)
      {
          Console.WriteLine("Erreur sur le 1er caratere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
          input = Console.ReadLine();
      }
      else
      {
          Console.WriteLine("Erreur sur le 2e caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
          input = Console.ReadLine();
      }
  }

  //check id Second the input char is numerical
  //while (!Regex.IsMatch(inputFirdtLetter.ToString(), "^[a-]+$", RegexOptions.IgnoreCase))
  while (!onlyNumber)
  {
      Console.WriteLine("Erreur sur le 2e caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
      input = Console.ReadLine();
  }
*/
				letter = (char)input[0];
				num = (char)input[1];

				if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
				{
					numvalide = true;
				}

				//int testnumbers = 1;

				//Console.WriteLine(" 1 en ascii" + Convert.ToChar(testnumbers));

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

					input = Console.ReadLine();
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


				if (input == "exit")
					break;


				//else if (verif == false)
				//    input = "c'est encore à toi";
				//int[] nb = Process.Converting.convertion();
				//Console.WriteLine("You: " + nb[0] + "" + nb[1]);

				client.Send(Encoding.UTF8.GetBytes(input));
			}
			Console.WriteLine("\nVous êtes deconnectés.");
			client.Close();
			serverSocket.Close();
			Console.ReadLine();
		}


		/*public static void Server(string ServerIp, int ServerPort)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("" + ServerIp + ""), ServerPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ip);
            socket.Listen(10);
            Console.WriteLine("Waiting for a client...");
            Socket client = socket.Accept();
            Console.WriteLine("Connected");

            string welcome = "Welcome to my test server";
            data = Encoding.UTF8.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            //string input;
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                Console.WriteLine("Entrez des coordonnées au format x");
                Console.WriteLine("Client: " + Encoding.UTF8.GetString(data, 0, recv));
                input = Console.ReadLine()!;
                if (input == "exit")
                    break;
                //else if (verif == false)
                //    input = "c'est encore à toi";
                //int[] nb = Process.Converting.convertion();
                //Console.WriteLine("You: " + nb[0] + "" + nb[1]);
                client.Send(Encoding.UTF8.GetBytes(input));
            }
            Console.WriteLine("Disconnected");
            client.Close();
            socket.Close();
            Console.ReadLine();
        }
*/
		public void Client()
		{
			Console.OutputEncoding = Encoding.UTF8;
			byte[] data = new byte[1024];
			string stringData;

			/*  IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.51"), 49221);
			  Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
  */

			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[3];

			IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("192.168.0.13"), 12345);
			//IPEndPoint serverEP = new IPEndPoint(ipAddress, 12345);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


			try
			{
				serverSocket.Connect(serverEP);
				Console.WriteLine("\nVous êtes connecté");
				Console.WriteLine("\nDebut de la Partie : Je joue en Premier ");
				//Get Data From the Server
				int recv = serverSocket.Receive(data);
			}
			catch
			{
				Console.WriteLine("\nImpossible de connecter au serveur ");
				return;
			}		

			
			while (true)
			{
				Console.WriteLine("\nEntrez vos coorodnnées d'attaque");
				//Console.WriteLine("\nEntrez vos coorodnnées d'attaque\n."); 
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



				//Check if input first Letter is Alphbetic . True if alpha
				//bool onlyAlphas = input[0].ToString().All(c => c >= 'a' && c <= 'j' || c >= 'A' && c <= 'J');
				//bool onlyNumber = false;
				//int value;
				//bool boolvalvalue = int.TryParse(input[1].ToString(), out value);


				/*if (int.TryParse(input[1].ToString(), out value) && value >= 0 && value <= 9)
				{
					onlyNumber = true;
				}*/
				//Console.WriteLine("valeur  de value :  " + value);
				//bool value = int.TryParse(input[1].ToString(), out value);
				//int inpuTovalue = Convert.ToInt32(input[1]);
				//Check if input first Letter is a number . True if number
				//input[1] = 

				//bool onlyNumber = input[1].ToString().All(c => c >= '0' && c <= '9');
				//value == char.IsLetterOrDigit(value)

				//(int.TryParse(c, out value) && value >= 1 && value <= 3)

				/*while (input.Length != 2 || !onlyAlphas || boolvalvalue == false)
				{

					//Console.WriteLine("onlyNumber = " + onlyNumber);

					if (input.Length != 2)
					{
						Console.WriteLine("Erreur sur le Nombre de Carateres : Vous devez saisir 2 caracteres. Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
						//input = Console.ReadLine();
					}
					if (!onlyAlphas)
					{
						Console.WriteLine("Erreur sur le 1er caratere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
						//input = Console.ReadLine();
					}
					if (boolvalvalue == false)
					{
						Console.WriteLine("Erreur sur le 2e caractere: Vous devez saisir d'abord une Lettre en premier, puis un chiffre . Veuillez entrer vos coordonnées d'attaque : De A0 a J10");
						//input = Console.ReadLine();
					}

					input = Console.ReadLine();
				}

*/

				//int[] nb = Process.Converting.convertion();
				//Console.WriteLine("You: " + nb[0] + "" + nb[1]);


				letter = (char)input[0];
				num = (char)input[1];

				if (num >= 48 && num <= 57) //Valeur ascii de 0 = 48 et de 9 = 57
				{
					numvalide = true;
				}

				//int testnumbers = 1;

				//Console.WriteLine(" 1 en ascii" + Convert.ToChar(testnumbers));

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

					input = Console.ReadLine();
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

				serverSocket.Send(Encoding.UTF8.GetBytes(input));

				data = new byte[1024];
				try
				{
					recv = serverSocket.Receive(data);
				}
				catch (Exception e)
				{
					Console.WriteLine("Client deconecté");
					//throw;
				}


				stringData = Encoding.UTF8.GetString(data, 0, recv);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\nVotre adversaire a attaque sur les coordonnées: " + stringData);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\nA Vous de Jouer");
				Console.WriteLine("\nEntrez vos coorodnnées d'attaque");
				Console.ResetColor();
			}
			Console.WriteLine("\nDeconnection du Serveur...");
			serverSocket.Shutdown(SocketShutdown.Both);
			serverSocket.Close();
			Console.WriteLine("\nVous êtes deconnecté.!");
			Console.ReadLine();
		}

		/*public static void Client(string ServerIp, int ServerPort)
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] data = new byte[1024];
            string stringData;
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("" + ServerIp + ""), ServerPort);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ip);
                Console.WriteLine("Connected");
            }
            catch
            {
                Console.WriteLine("Unable to connect to server.");
                return;
            }
            int recv = server.Receive(data);
            while (true)
            {
                input = Console.ReadLine()!;
                if (input == "exit")
                    break;
                //int[] nb = Process.Converting.convertion();
                //Console.WriteLine("You: " + nb[0] + "" + nb[1]);

                server.Send(Encoding.UTF8.GetBytes(input));

                data = new byte[1024];
                recv = server.Receive(data);
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                Console.WriteLine("Server: " + stringData);
            }
            Console.WriteLine("Disconnecting from server...");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Console.WriteLine("Disconnected!");
            Console.ReadLine();
        }
*/

	}
}