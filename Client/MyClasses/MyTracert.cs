using System.Net;
using System.Text;

namespace Client.MyClasses
{
    using System.Net;
    using System.Net.NetworkInformation;

    public abstract class MyTracert
    {
        public static void Trace(string domainName, RichTextBox textBox)
        {
            textBox.Clear();
            IPAddress address = Dns.GetHostAddresses(domainName)[0];
            textBox.AppendText($"Трассировка маршрута к {domainName} [{address}]\r\n\r\n");
            int ttl = 1;
            while (true)
            {
                PingReply reply = new Ping().Send(address, 5000, new byte[] { 0 }, new PingOptions(ttl, true));
                if (reply.Status == IPStatus.Success)
                {
                    textBox.AppendText($"\r\nТрассировка успешно завершена! Адрес={reply.Address}, время={reply.RoundtripTime}ms\r\n");
                    break;
                }
                else
                {
                    MyPing myPing = new MyPing(reply.Address);
                    textBox.AppendText($"[{ttl}] - {myPing.Message}");
                }
                if (ttl++ > 30)
                {
                    textBox.AppendText("Трассировка прервана.\r\n");
                    break;
                }
            }
        }
    }
}
