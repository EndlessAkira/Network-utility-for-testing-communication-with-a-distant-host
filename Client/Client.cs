using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Client.MyClasses;

namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private const int _port = 8888;
        private const int _size = 1024;

        List<Server> _servers = new List<Server>();
        // 
        private int _numberOfEchoRequests = 2;
        private bool _tracertCheck;

        private readonly int _numberOfEchoRequestsMaxValue = 200;
        private readonly int _numberOfEchoRequestsMinValue = 2;
        //

        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void updateListServersButton_Click(object sender, EventArgs e)
        {
            _servers.Clear();
            serversListBox.Items.Clear();
            Loading loading = new Loading();
            loading.Show();
            this.Hide();
            loading.Update();
            IPEndPoint[] allIPEndPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            List<IPEndPoint> iPEndPoints = new List<IPEndPoint>();
            foreach (IPEndPoint iPEndPoint in allIPEndPoints)
            {
                if (iPEndPoint.Address.ToString() != IPAddress.None.ToString() && iPEndPoint.Address.ToString() != IPAddress.Any.ToString() && iPEndPoint.AddressFamily == AddressFamily.InterNetwork)
                {
                    iPEndPoints.Add(iPEndPoint);   
                }
            }
            for (int i = 0; i < iPEndPoints.Count; i++)  
            {
                _servers.Add(new Server(iPEndPoints[i], Dns.GetHostByAddress(iPEndPoints[i].Address).HostName));
                
            }
            try
            {
                IPEndPoint iPEnd = new IPEndPoint(Dns.GetHostByName("www.google.com").AddressList[0], _port);
                _servers.Add(new Server(iPEnd, "www.google.com"));
                iPEnd = new IPEndPoint(Dns.GetHostByName("www.microsoft.com").AddressList[0], _port);
                _servers.Add(new Server(iPEnd, "www.microsoft.com"));
            }
            catch
            {
                MessageBox.Show("Отсутствует соединение с Интернетом! Невозможно добавить хост www.google.com");
            }
            this.Show();
            loading.Close();
            for (int i = 0; i < _servers.Count; i++)
            {
                serversListBox.Items.Add($"{_servers[i].IPEndPoint.Address}   {_servers[i].HostName}");
            }
            
        }

        private void numberOfEchoRequestsTrackBar_Scroll(object sender, EventArgs e)
        {
            _numberOfEchoRequests = numberOfEchoRequestsTrackBar.Value;
            numberOfEchoRequestsTextBox.Text = _numberOfEchoRequests.ToString();
        }

        private void numberOfEchoRequestsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int _value = Convert.ToInt32(numberOfEchoRequestsTextBox.Text);
                if (_value < _numberOfEchoRequestsMinValue)
                {
                    numberOfEchoRequestsTextBox.Text = _numberOfEchoRequestsMinValue.ToString();
                    _value = _numberOfEchoRequestsMinValue;

                }
                else if(_value > _numberOfEchoRequestsMaxValue)
                {
                    numberOfEchoRequestsTextBox.Text = _numberOfEchoRequestsMaxValue.ToString();
                    _value = _numberOfEchoRequestsMaxValue;
                }
                _numberOfEchoRequests = _value;
                numberOfEchoRequestsTrackBar.Value = _value;

            }
            catch
            {
                MessageBox.Show("Ошибка ввода!");
                int _value = _numberOfEchoRequestsMinValue;
                numberOfEchoRequestsTrackBar.Value = _value;
                numberOfEchoRequestsTextBox.Text = _value.ToString();

            }
        }

        private void contactServerButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                if (contactByIPRadioButton.Checked)
                {
                    _servers[serversListBox.SelectedIndex].LinkingByIPAdress(messageTextBox, _numberOfEchoRequests);
                }
                else if (contactByNameRadioButton.Checked)
                {
                    _servers[serversListBox.SelectedIndex].LinkingByName(messageTextBox, _numberOfEchoRequests);
                }
                MessageBox.Show("Выполнение утилиты Ping завершено!");
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }

        private void serversListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {
            updateListServersButton_Click(sender, e);   
        }

        private void clearMessageTextBoxButton_Click(object sender, EventArgs e)
        {
            messageTextBox.Text = "";
        }

        private bool ServerSelected()
        {
            return serversListBox.SelectedIndex > -1;
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            if(ServerSelected())
            {
                messageTextBox.Text = MyNslookup.GetInfo(_servers[serversListBox.SelectedIndex].HostName);
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }

        private void findOtherButton_Click(object sender, EventArgs e)
        {
            AddServer addServerForm = new AddServer(this);
            addServerForm.Show();
            this.Hide();

        }

        public void AddServer(IPAddress iP)
        {
            //_servers.Add(server);
        }
        public void AddServer(string hostname)
        {
            //_servers.Add(server);
        }

        private void tracertButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                MyTracert.Trace(_servers[serversListBox.SelectedIndex].HostName, messageTextBox);
                MessageBox.Show("Выполнение утилиты Tracert завершено!");
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }
    }

    public class Server
    {
        public IPEndPoint IPEndPoint { get; private set; }
        public string HostName { get; private set; }

        public Server(IPEndPoint ipEndPoint, string hostName)
        {
            IPEndPoint = ipEndPoint;
            HostName = hostName;
        }
        public void LinkingByIPAdress(RichTextBox textBox, int numberOfEchoRequests)
        {
            textBox.Text = "Выполнение проверки соединения с сервером по IPv4\r\n\r\n";
            try
            {
                List<float> time = new List<float>();
                List<bool> connectedEchoPackage = new List<bool>();
                for (int i = 0; i < numberOfEchoRequests; i++)
                {
                    Thread.Sleep(50);
                    MyPing myPing = new MyPing(IPEndPoint.Address);
                    
                    connectedEchoPackage.Add(myPing.IsConnected);
                    string message = $"{i + 1}) " + myPing.Message;
                    textBox.AppendText(message);
                    if (myPing.IsConnected)
                    {
                        time.Add(myPing.ResponseTime);
                    }
                    textBox.Update();
                }
                textBox.AppendText($"\r\nСтатистика Ping для {IPEndPoint.Address}:");
                textBox.AppendText($"Пакетов:\r\n     отправлено = {numberOfEchoRequests},\r\n     получено = {NumberPacketsReceived(connectedEchoPackage)},\r\n     потеряно = {NumberLostPackets(connectedEchoPackage, numberOfEchoRequests)} ( {GetProcent(NumberLostPackets(connectedEchoPackage, numberOfEchoRequests), numberOfEchoRequests)}% )");
                if (time.Count > 0)
                {
                    textBox.AppendText($"\r\nПриблизительное время приёма-передачи в мс:\r\n     Минимальное={FindMin(time)}мс,\r\n     Максимальное={FindMax(time)}мс,\r\n     Среднее={FindAverage(time)}мс\r\n");
                }
            }
            catch (Exception ex)
            {
                textBox.Text += $"АБОБА?\r\n";
                textBox.Update();
            }
        }
        public void LinkingByName(RichTextBox textBox, int numberOfEchoRequests)
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
                        MyPing myPing = new MyPing(IPEndPoint.Address);
                        textBox.Text += $"{i+1}) " + myPing.Message;
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
            return (float)Math.Round(sum / list.Count,2);
        }

        private int NumberPacketsReceived(List<bool> list)
        {
            int count = 0;
            foreach(bool connect in list)
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