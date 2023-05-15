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

        List<MyServer> _servers = new List<MyServer>();
        // 
        private int _numberOfEchoRequests = 2;

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
                _servers.Add(new MyServer(iPEndPoints[i].Address));
                
            }
            try
            {
                IPEndPoint iPEnd = new IPEndPoint(Dns.GetHostByName("www.google.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.google.com"));
                iPEnd = new IPEndPoint(Dns.GetHostByName("www.microsoft.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.microsoft.com"));
            }
            catch
            {
                MessageBox.Show("Отсутствует соединение с Интернетом! Невозможно добавить хост www.google.com");
            }
            this.Show();
            loading.Close();
            for (int i = 0; i < _servers.Count; i++)
            {
                serversListBox.Items.Add($"{_servers[i].IP}   {_servers[i].HostName}");
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
                messageTextBox.Text = MyNslookup.GetInfo(_servers[serversListBox.SelectedIndex]);
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
        }

        public void AddServer(IPAddress iP)
        {
            _servers.Add(new MyServer(iP));
        }
        public void AddServer(string hostname)
        {
            _servers.Add(new MyServer(hostname));
        }

        public void UpdateServerListBox()
        {
            serversListBox.Items.Clear();
            foreach(var server in _servers)
            {
                serversListBox.Items.Add($"{server.IP}    {server.HostName}");   
            }
        }

        private void tracertButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                MyTracert.Trace(_servers[serversListBox.SelectedIndex], messageTextBox);
                MessageBox.Show("Выполнение утилиты Tracert завершено!");
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }
    }

    
}