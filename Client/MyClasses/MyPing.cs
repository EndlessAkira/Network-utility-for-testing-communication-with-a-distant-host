using System.Net;
using System.Net.Sockets;

namespace Client.MyClasses
{
    public class MyPing
    {
        public IPAddress Address { get; private set; }
        public bool IsConnected { get; private set; }
        public float ResponseTime { get; private set; }
        public byte TTL { get; private set; }
        public string Message { get; private set; }

        private byte[] _buffer;
        private EndPoint _endPoint;
        private Socket _socket;

        const int timeout = 1000;
        const int packetSize = 32;

        public MyPing(IPAddress address)
        {
            Address = address;
            _buffer = new byte[packetSize];
            new Random().NextBytes(_buffer);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);

            _endPoint = new IPEndPoint(address, 0);

            DateTime sendTime = DateTime.Now;
            _socket.SendTo(CreatePingPacket(_buffer), _endPoint);

            byte[] responseBuffer = new byte[_buffer.Length + 28];
            try
            {
                _socket.ReceiveFrom(responseBuffer, ref _endPoint);
                DateTime receiveTime = DateTime.Now;
                ResponseTime = (float)Math.Round(((float)(receiveTime.Ticks - sendTime.Ticks)) / 10000, 2);
                TTL = GetTTL(responseBuffer);
                IsConnected = true;
                Message = $"Ответ от {Address}: число байт={_buffer.Length} время={ResponseTime}мс, TTL={TTL}\r\n";
            }
            catch (SocketException ex)
            {
                IsConnected = false;
                Message = $"Ответ от {Address}: потеря эхо-пакета!\r\n";
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
