using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class MyPing
    {
        public bool IsConnected { get; private set; }
        public float ResponseTime{ get; private set; }
        public byte TTL { get; private set; }
        public string Message { get; private set; }
        public MyPing(IPAddress address)
        {
            const int timeout = 1000;
            const int packetSize = 32;

            byte[] buffer = new byte[packetSize];
            new Random().NextBytes(buffer);

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp))
            {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);

                EndPoint endPoint = new IPEndPoint(address, 0);
                socket.SendTo(CreatePingPacket(buffer), endPoint);
                DateTime sendTime = DateTime.Now;
    
                byte[] responseBuffer = new byte[packetSize + 28];
                try
                {
                    socket.ReceiveFrom(responseBuffer, ref endPoint);
                    DateTime receiveTime = DateTime.Now;
                    ResponseTime = (float)Math.Round(((float)(receiveTime.Ticks - sendTime.Ticks))/10000, 2);
                    TTL = GetTTL(responseBuffer);
                    IsConnected = true;
                    Message = $"Ответ от {address}: число байт={packetSize} время={ResponseTime}мс, TTL={TTL}\r\n";
                }
                catch (SocketException ex)
                {
                    IsConnected = false;
                    Message = $"Потеря эхо-пакета!\r\n";
                }
            }
        }
        private static byte GetTTL(byte[] ipHeader)
        {
            // IP-заголовок имеет фиксированный размер 20 байт (для IPv4)
            // Восьмой байт в заголовке содержит TTL
            const int ttlOffset = 8;
            return ipHeader[ttlOffset];
        }
        private static byte[] CreatePingPacket(byte[] buffer)
        {
            const int icmpHeaderSize = 8;

            byte[] packet = new byte[icmpHeaderSize + buffer.Length];
            packet[0] = 8;  // Эхо-запрос ICMP
            packet[1] = 0;  // Всегда ноль
            Array.Copy(BitConverter.GetBytes(0), 0, packet, 2, 2);  // Всегда ноль
            Array.Copy(BitConverter.GetBytes(0), 0, packet, 4, 2);  // Идентификатор
            Array.Copy(BitConverter.GetBytes(0), 0, packet, 6, 2);  // Номер последовательности
            Array.Copy(buffer, 0, packet, icmpHeaderSize, buffer.Length);
            int checksum = CalculateChecksum(packet);
            Array.Copy(BitConverter.GetBytes(checksum), 0, packet, 2, 2);
            return packet;
        }
        private static int CalculateChecksum(byte[] buffer)
        {
            int sum = 0;
            for (int i = 0; i < buffer.Length; i += 2)
            {
                sum += (int)BitConverter.ToUInt16(buffer, i);
            }
            sum = (sum >> 16) + (sum & 0xffff);
            sum += sum >> 16;
            return ~sum;
        }
    }
}
