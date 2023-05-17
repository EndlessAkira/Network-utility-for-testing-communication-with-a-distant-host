using System.Net;
namespace Client.MyClasses
{
    using System.Net;
    using System.Net.NetworkInformation;

    public abstract class MyTracert
    {
        public static void Trace(MyServer server, RichTextBox textBox)
        {
            textBox.Clear();
            IPAddress address = server.IP;
            textBox.AppendText($"Трассировка маршрута к {server.HostName} [{address}]\r\n\r\n");
            int ttl = 1;     // Определение переменной TTL единицей
            float timer = 0; // Определение переменной таймера нулём
            while (true)
            {
                PingReply reply = new Ping().Send(address, 1000, new byte[] { 0 }, new PingOptions(ttl, true)); // Пинг заданным значением TTL
                if (reply.Status == IPStatus.Success)
                {
                    // Если произошло подключение к конечному IP
                    timer = (float)Math.Round(timer, 2);
                    textBox.AppendText($"\r\nТрассировка {address} успешно завершена!\r\n      Кол-во прыжков={ttl}\r\n      Время={timer}мс");
                    break;
                }
                else
                {   
                    // Если маршрутизатор промежуточный
                    MyPing myPing = new MyPing(reply.Address);
                    textBox.AppendText($"[{ttl}] - {myPing.Message}"); // Вывод хопа и сообщения от MyPing
                    timer += myPing.ResponseTime;                      // Прибавление времени к таймеру
                }
                if (ttl++ > 30)
                {
                    // Если кол-во хопов больше 30 а подключиться не удалось
                    textBox.AppendText("Трассировка прервана.\r\n");
                    break;
                }
            }
        }
    }
}
