/*namespace BatailleNavale
{
    public class Networking
    {
        Player PlayerMy;

        public Networking(Player player)
		{
            this.PlayerMy = player;     
		}
		public static string input { get; set; }

        public static void Server()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.51"), 49221);
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
                input = Console.ReadLine();
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


        public  void Server(string ServerIp, int ServerPort)
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
            BoatPlacement.PutBoats(PlayerMy.Grid, PlayerMy.ListOfBoats);//placement de bateaux
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
                input = Console.ReadLine();
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

        public void Client()
        {
            Console.OutputEncoding = Encoding.UTF8;
            byte[] data = new byte[1024];
            string stringData;
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.1.51"), 49221);
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
            BoatPlacement.PutBoats(PlayerMy.Grid, PlayerMy.ListOfBoats);//placement de bateaux
            int recv = server.Receive(data);
            while (true)
            {
                input = Console.ReadLine();
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

        public static void Client(string ServerIp, int ServerPort)
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
                input = Console.ReadLine();
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


    }
}*/