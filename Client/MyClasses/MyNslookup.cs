using System.Net;
using System.Net.Sockets;

namespace Client.MyClasses
{
    internal abstract class MyNslookup
    {
        public static string GetInfo(MyServer server)
        {
            string message = $"Информация о сервере {server.HostName}:\r\n\r\n";
            try
            {
                IPHostEntry host = Dns.GetHostByName(server.HostName); // Получение хоста по имени сервера
                IPAddress[] ips = host.AddressList;                    // Определение массива IP-адресов хоста
                List<IPAddress> ipV4 = new List<IPAddress>();          // Обьявление листа IPv4-адресов
                List<IPAddress> ipV6 = new List<IPAddress>();          // Обьявление листа IPv6-адресов
                foreach (IPAddress ip in ips)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)// Если адрес относится к IPv4
                    {
                        ipV4.Add(ip);
                    }
                    else if (ip.AddressFamily == AddressFamily.InterNetworkV6)// Если адрес относится к IPv6
                    {
                        ipV6.Add(ip);
                    }
                }
                if (ipV4.Count > 0)
                {
                    // Вывод IPv4 адресов если их кол-во больше нуля
                    message += "Адрес(-а) IPv4:\r\n";
                    foreach (var ip in ipV4)
                    {
                        message += "     " + ip.ToString() + "\r\n";
                    }
                }
                if (ipV6.Count > 0)
                {
                    // Вывод IPv6 адресов если их кол-во больше нуля
                    message += "\r\nАдрес(-а) IPv6:\r\n";
                    foreach (var ip in ipV6)
                    {
                        message += "     " + ip.ToString() + "\r\n";
                    }
                }
                string[] aliesNames = host.Aliases;
                if (aliesNames.Length > 0)
                {
                    // Вывод Alies-имён если их кол-во больше нуля
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
