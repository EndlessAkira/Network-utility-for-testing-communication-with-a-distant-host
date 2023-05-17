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

        List<MyServer> _servers = new List<MyServer>();               // ���� ������������ ��������

        private const int _port = 8888;                               // ���� �����������
        private int _numberOfEchoRequests = 2;                        // ���-�� ���-��������
        private readonly int _numberOfEchoRequestsMaxValue = 200;     // ����. ���-�� ���-��������
        private readonly int _numberOfEchoRequestsMinValue = 2;       // ���. ���-�� ���-��������

        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // ����� �����
        
        // ����� ������������� ��� �������� �����
        private void Client_Load(object sender, EventArgs e)
        {
            updateListServersButton_Click(sender, e); // ����� ���������� ����� ��������
        }

        // ����������� ���-�� ���-��������
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
                MessageBox.Show("������ �����!");
                int _value = _numberOfEchoRequestsMinValue;
                numberOfEchoRequestsTrackBar.Value = _value;
                numberOfEchoRequestsTextBox.Text = _value.ToString();

            }
        }

        // ������ ������ ������ ����������
        private void updateListServersButton_Click(object sender, EventArgs e)
        {
            _servers.Clear();
            Loading loading = new Loading(); // ������� ����������� �����
            loading.Show();                  // ������ ������������ ������
            this.Hide();                     // �������� �������� �����
            loading.Update();                   
            IPEndPoint[] allIPEndPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners(); // ����� ������ ������ ��������� ����
            foreach (IPEndPoint iPEndPoint in allIPEndPoints)
            {
                if (iPEndPoint.Address.ToString() != IPAddress.None.ToString() && iPEndPoint.Address.ToString() != IPAddress.Any.ToString() && iPEndPoint.AddressFamily == AddressFamily.InterNetwork) // ����� IPv4 ������
                {
                    _servers.Add(new MyServer(iPEndPoint.Address));
                }
            }
            try
            {
                // ���������� ������� ���������� ������
                IPEndPoint iPEnd = new IPEndPoint(Dns.GetHostByName("www.google.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.google.com"));
                iPEnd = new IPEndPoint(Dns.GetHostByName("www.microsoft.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.microsoft.com"));
            }
            catch
            {
                MessageBox.Show("����������� ���������� � ����������! ���������� �������� ���� www.google.com");
            }
            this.Show();                    // ����� ������� �����
            loading.Close();                // �������� ������������ ������
            UpdateServerListBox();          // ����� �������� � ListBox

        }
        private void infoButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                GetSelectedServer().Nslookup(messageTextBox);
            }
            else
            {
                MessageBox.Show("�������� ������ �� ������ ��� ��������������!");
            }
        }
        private void pingServerButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                if (contactByIPRadioButton.Checked)         // ���� ������ Ping �� IP-������ �������
                {
                    GetSelectedServer().PingByIPAdress(messageTextBox, _numberOfEchoRequests);
                }
                else if (contactByNameRadioButton.Checked)  // ���� ������ Ping �� ����� �������
                {
                    GetSelectedServer().PingByName(messageTextBox, _numberOfEchoRequests);
                }
                MessageBox.Show("���������� ������� Ping ���������!");
            }
            else
            {
                MessageBox.Show("�������� ������ �� ������ ��� ��������������!");
            }
        }
        private void tracertButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                GetSelectedServer().Tracert(messageTextBox);
                MessageBox.Show("���������� ������� Tracert ���������!");
            }
            else
            {
                MessageBox.Show("�������� ������ �� ������ ��� ��������������!");
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                string serverName = GetSelectedServer().HostName;                    // ���������� ��� �������
                IPAddress serverIP = GetSelectedServer().IP;                         // ���������� IP �������
                _servers[serversListBox.SelectedIndex] = null;                       // �������� ������ � ��������� �������
                List<MyServer> _updateServers = new List<MyServer>();                // ������� ����� ���� ��������
                for (int i = 0; i < _servers.Count; i++)                             // ��������� ���� ����� ����� ����������
                {
                    if (_servers[i] != null)
                    {
                        _updateServers.Add(_servers[i]);
                    }
                }
                _servers = _updateServers;                                           // ����������� ��������� ����� ��������������� 
                UpdateServerListBox();                                               // ��������� ListBox, ������ ������ �������
                MessageBox.Show($"��������� ������ ��� ������� ������!\r\n Name: {serverName}\r\n IP: {serverIP}");
            }
            else
            {
                MessageBox.Show("�������� ������ �� ������ ��� ��������������!");
            }
        }
        private void findOtherButton_Click(object sender, EventArgs e)
        {
            AddServer addServerForm = new AddServer(this);
            addServerForm.Show();
        }

        // ����� ������� ���� ������ ���������
        private void clearMessageTextBoxButton_Click(object sender, EventArgs e)
        {
            messageTextBox.Text = "";
        }
        
        // ��������������� ������
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
        private bool ServerSelected()
        {
            return serversListBox.SelectedIndex > -1;
        }
        private MyServer GetSelectedServer()
        {
            return _servers[serversListBox.SelectedIndex];
        }
    }
}
