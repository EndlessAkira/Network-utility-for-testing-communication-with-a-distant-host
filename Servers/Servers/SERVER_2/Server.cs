using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servers
{
    internal abstract class Server
    {
        
        // Длинна очереди
        protected const int _lengthQueue = 10;
        // Размер сообщения
        protected const int SIZE = 1024;

        public static int PORT { get; protected set; }
        public static string IPAdress { get; protected set; }

        protected static void Start()
        {
            Console.WriteLine($"Запуск сервера IP: {IPAdress}, порт: {PORT}");


            // Создание конечной точки по IP и порту
            IPEndPoint _iPEndPoint = new IPEndPoint(IPAddress.Parse(IPAdress), PORT);
            // Создание сокета(v4, потоковый, TCP)
            Socket _socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Связь с конечной локальной точкой для ожидания вход. запросов
            _socket1.Bind(_iPEndPoint);
            // Включение прослушивания
            _socket1.Listen(_lengthQueue);
            // Вывод инфо о сервере
            Console.WriteLine("Дата и время: " + DateTime.Now + "\r\n");
            Console.WriteLine($"Прослушивающий сокет:\r\n      Дескриптор: {_socket1.Handle}\r\n      IPv4: {_iPEndPoint.Address}\r\n      Порт: {_iPEndPoint.Port}\r\n");
            // Ожидание подключения
            Console.WriteLine("Сервер в режиме ожидания\r\n");
            // Инициализация клиентского сокета в случае подключения клиента к серверу
            Socket _socket2 = _socket1.Accept();
            // Инициализация переменной для сообщения от сервера
            String _dataRec = "";
            while (true)
            {
                Console.WriteLine("Дата и время: " + DateTime.Now + "\r\n");
                Console.WriteLine($"Получение запроса от клиента:\n      Дескриптор: {_socket2.Handle}\n      IPv4: {((IPEndPoint)_socket2.RemoteEndPoint).Address}\n      Порт: {((IPEndPoint)_socket2.RemoteEndPoint).Port}\r\n");
                byte[] _byteRec = new byte[SIZE];
                // Приём сообщения от клиента, запись сообщения и его длинны
                int _lenBytesReciver = _socket2.Receive(_byteRec);
                // Декодировка
                _dataRec += Encoding.ASCII.GetString(_byteRec, 0, _lenBytesReciver);
                // Есть ли в сообщении еще символы
                if (_dataRec.IndexOf('.') > -1)
                {
                    break;
                }
            }
            Console.WriteLine($"Получено сообщение от клиента: {_dataRec}\r\n");
            // Инициализация сообщения для клиента
            string dataSend = $"\n {Dns.GetHostName()}";
            // Кодировка сообщения
            byte[] byteSend = Encoding.ASCII.GetBytes(dataSend);
            // Отправка сообщения клиенту
            int lenBytesSend = _socket2.Send(byteSend);
            Console.WriteLine($"Отправка клиенту {lenBytesSend} bytes");
            // Инициирование закрытия сокета клиента
            _socket2.Shutdown(SocketShutdown.Both);
            // Закрытие сокета клиента
            _socket2.Close();
            Console.WriteLine("Общение с клиентом остановлено");
        }
    }
}
