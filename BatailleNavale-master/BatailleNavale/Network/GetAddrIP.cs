﻿
namespace BatailleNavale.Network;

class GetAddrIP
{
    static void GetIp(string[] args)
    {
        string IPAddress = "";
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
        }

        Console.WriteLine(IPAddress);
    }
}
