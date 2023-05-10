using System.Net;
using System.Net.Sockets;

namespace Client.MyClasses
{
    internal abstract class MyNslookup
    {
        public static string GetInfo(string hostname)
        {
            string message = $"Информация о сервере {hostname}:\r\n\r\n";
            try
            {
                IPHostEntry iPHostEntry = Dns.GetHostByName(hostname);
                Console.WriteLine(iPHostEntry.HostName);
                IPAddress[] ips = iPHostEntry.AddressList;
                List<IPAddress> ipV4 = new List<IPAddress>();
                List<IPAddress> ipV6 = new List<IPAddress>();
                foreach (IPAddress ip in ips)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipV4.Add(ip);
                    }
                    else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        ipV6.Add(ip);
                    }
                }
                if (ipV4.Count > 0)
                {
                    message += "Адрес(-а) IPv4:\r\n";
                    foreach (var ip in ipV4)
                    {
                        message += "     " + ip.ToString() + "\r\n";
                    }
                }
                if (ipV6.Count > 0)
                {
                    message += "\r\nАдрес(-а) IPv6:\r\n";
                    foreach (var ip in ipV6)
                    {
                        message += "     " + ip.ToString() + "\r\n";
                    }
                }
                string[] aliesNames = iPHostEntry.Aliases;
                MessageBox.Show($"{aliesNames.Length > 0}");
                if (aliesNames.Length > 0)
                {
                    message += "\r\nAlias-имена домена:\r\n";
                    foreach (string aliesName in aliesNames)
                    {
                        message += "     " + aliesName + "\r\n";
                    }
                }
                return message;

            }
            catch (Exception ex)
            {
                message += "Произошла ошибка получения информации о сервере!";
            }
            return message;
        }
    }
}
