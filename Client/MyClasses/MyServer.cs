using System.Net.Sockets;
using System.Net;

namespace Client.MyClasses
{
    public class MyServer
    {
        public const int Port = 8888;
        public IPAddress IP { get; private set; }
        public string HostName { get; private set; }

        public MyServer(string hostName)
        {
            HostName = hostName;
            IPAddress[] iPs = Dns.GetHostAddresses(HostName);
            bool findIPv4 = false;
            for (int i = 0; i < iPs.Length; i++)
            {
                if (iPs[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    IP = iPs[i];
                    findIPv4 = true;
                    break;
                }
            }
            if (findIPv4 == false)
            {
                IP = iPs[0];
            }
        }
        public MyServer(IPAddress address)
        {
            IP = address;
            HostName = Dns.GetHostByAddress(IP).HostName;
        }
        public void PingByIPAdress(RichTextBox textBox, int numberOfEchoRequests)
        {
            textBox.Text = "Выполнение проверки соединения с сервером по IPv4\r\n\r\n";
            try
            {
                List<float> time = new List<float>();
                List<bool> connectedEchoPackage = new List<bool>();
                for (int i = 0; i < numberOfEchoRequests; i++)
                {
                    Thread.Sleep(50);
                    MyPing myPing = new MyPing(IP);

                    connectedEchoPackage.Add(myPing.IsConnected);
                    string message = $"{i + 1}) " + myPing.Message;
                    textBox.AppendText(message);
                    if (myPing.IsConnected)
                    {
                        time.Add(myPing.ResponseTime);
                    }
                    textBox.Update();
                }
                textBox.AppendText($"\r\nСтатистика Ping для {IP}:");
                textBox.AppendText($"\r\n     Пакетов:  отправлено = {numberOfEchoRequests}, получено = {NumberPacketsReceived(connectedEchoPackage)}, потеряно = {NumberLostPackets(connectedEchoPackage, numberOfEchoRequests)} ( {GetProcent(NumberLostPackets(connectedEchoPackage, numberOfEchoRequests), numberOfEchoRequests)}% )");
                if (time.Count > 0)
                {
                    textBox.AppendText($"\r\nПриблизительное время приёма-передачи в мс:\r\n     Минимальное={FindMin(time)}мс,\r\n     Максимальное={FindMax(time)}мс,\r\n     Среднее={FindAverage(time)}мс\r\n");
                }
            }
            catch (Exception ex)
            {
                textBox.Text += $"При проверке связи не удалось подключиться к выбранному узлу\r\n";
                textBox.Update();
            }
        }
        public void PingByName(RichTextBox textBox, int numberOfEchoRequests)
        {
            textBox.Text = "Выполнение проверки соединения с сервером по имени хоста\r\n\r\n";
            try
            {
                IPAddress[] addresses = Dns.GetHostByName(HostName).AddressList;
                IPAddress addressv4 = null;
                foreach (var adress in addresses)
                {
                    if (adress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        addressv4 = adress;
                    }
                }

                if (addressv4 != null)
                {
                    List<float> time = new List<float>();
                    List<bool> connectedEchoPackage = new List<bool>();
                    for (int i = 0; i < numberOfEchoRequests; i++)
                    {
                        Thread.Sleep(50);
                        MyPing myPing = new MyPing(IP);
                        textBox.Text += $"{i + 1}) " + myPing.Message;
                        textBox.Update();
                        connectedEchoPackage.Add(myPing.IsConnected);
                        if (myPing.IsConnected)
                        {
                            time.Add(myPing.ResponseTime);
                        }
                    }
                    textBox.Text += $"\r\nСтатистика Ping для {HostName}:";
                    textBox.Text += $"\r\n     Пакетов: отправлено = {numberOfEchoRequests}, получено = {NumberPacketsReceived(connectedEchoPackage)}, потеряно = {NumberLostPackets(connectedEchoPackage, numberOfEchoRequests)} ( {GetProcent(NumberLostPackets(connectedEchoPackage, numberOfEchoRequests), numberOfEchoRequests)}% )";
                    if (time.Count > 0)
                    {
                        textBox.Text += $"\r\nПриблизительное время приёма-передачи в мс:\r\n     Минимальное={FindMin(time)}мс,\r\n     Максимальное={FindMax(time)}мс,\r\n     Среднее={FindAverage(time)}мс\r\n";
                    }

                }
                else
                {
                    textBox.Text += "Не найден IPv4 для подключения\r\n";
                    textBox.Update();
                }

            }
            catch (Exception ex)
            {
                textBox.Text += "При проверке связи не удалось подключиться к выбранному узлу\r\n";
                textBox.Update();
            }
        }
        public void Tracert(RichTextBox textBox)
        {
            MyTracert.Trace(this, textBox);
        }
        public void Nslookup(RichTextBox textBox)
        {
            textBox.Text = MyNslookup.GetInfo(this);
        }
        private float FindMin(List<float> list)
        {
            float min = list[0];
            foreach (var item in list)
            {
                if (item < min)
                {
                    min = item;
                }
            }
            return min;
        }
        private float FindMax(List<float> list)
        {
            float max = list[0];
            foreach (var item in list)
            {
                if (item > max)
                {
                    max = item;
                }
            }
            return max;
        }
        private float FindAverage(List<float> list)
        {
            float sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return (float)Math.Round(sum / list.Count, 2);
        }

        private int NumberPacketsReceived(List<bool> list)
        {
            int count = 0;
            foreach (bool connect in list)
            {
                if (connect == true)
                {
                    count++;
                }
            }
            return count;
        }
        private int NumberLostPackets(List<bool> list, int numberOfEchoRequests)
        {
            return numberOfEchoRequests - NumberPacketsReceived(list);
        }

        private float GetProcent(int a, int b)
        {
            return (float)Math.Round((float)a / b * 100, 2);
        }
    }
}
