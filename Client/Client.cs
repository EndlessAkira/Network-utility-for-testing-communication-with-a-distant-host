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

        List<MyServer> _servers = new List<MyServer>();               // Лист подключенных серверов

        private const int _port = 8888;                               // Порт подключения
        private int _numberOfEchoRequests = 2;                        // Кол-во эхо-запросов
        private readonly int _numberOfEchoRequestsMaxValue = 200;     // Макс. кол-во эхо-запросов
        private readonly int _numberOfEchoRequestsMinValue = 2;       // Мин. кол-во эхо-запросов

        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Сокет связи
        
        // Метод срабатывающий при загрузке формы
        private void Client_Load(object sender, EventArgs e)
        {
            updateListServersButton_Click(sender, e); // Вызов обновления списк серверов
        }

        // Регулировка кол-ва эхо-запросов
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

        // Методы кнопок панели управления
        private void updateListServersButton_Click(object sender, EventArgs e)
        {
            _servers.Clear();
            Loading loading = new Loading(); // Создаем загрузочный экран
            loading.Show();                  // Запуск загрузочного экрана
            this.Hide();                     // Сокрытие основной формы
            loading.Update();                   
            IPEndPoint[] allIPEndPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners(); // Поиск хостов внутри локальной сети
            foreach (IPEndPoint iPEndPoint in allIPEndPoints)
            {
                if (iPEndPoint.Address.ToString() != IPAddress.None.ToString() && iPEndPoint.Address.ToString() != IPAddress.Any.ToString() && iPEndPoint.AddressFamily == AddressFamily.InterNetwork) // Выбор IPv4 хостов
                {
                    _servers.Add(new MyServer(iPEndPoint.Address));
                }
            }
            try
            {
                // Добавление вручную нескольких хостов
                IPEndPoint iPEnd = new IPEndPoint(Dns.GetHostByName("www.google.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.google.com"));
                iPEnd = new IPEndPoint(Dns.GetHostByName("www.microsoft.com").AddressList[0], _port);
                _servers.Add(new MyServer("www.microsoft.com"));
            }
            catch
            {
                MessageBox.Show("Отсутствует соединение с Интернетом! Невозможно добавить хост www.google.com");
            }
            this.Show();                    // Показ главной формы
            loading.Close();                // Закрытие загрузочного экрана
            UpdateServerListBox();          // Вывод серверов в ListBox

        }
        private void infoButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                GetSelectedServer().Nslookup(messageTextBox);
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }
        private void pingServerButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                if (contactByIPRadioButton.Checked)         // Если выбран Ping по IP-адресу сервера
                {
                    GetSelectedServer().PingByIPAdress(messageTextBox, _numberOfEchoRequests);
                }
                else if (contactByNameRadioButton.Checked)  // Если выбран Ping по имени сервера
                {
                    GetSelectedServer().PingByName(messageTextBox, _numberOfEchoRequests);
                }
                MessageBox.Show("Выполнение утилиты Ping завершено!");
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }
        private void tracertButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                GetSelectedServer().Tracert(messageTextBox);
                MessageBox.Show("Выполнение утилиты Tracert завершено!");
            }
            else
            {
                MessageBox.Show("Выберите сервер из списка для взаимодействия!");
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected())
            {
                string serverName = GetSelectedServer().HostName;                    // Записываем имя сервера
                IPAddress serverIP = GetSelectedServer().IP;                         // Записываем IP сервера
                _servers[serversListBox.SelectedIndex] = null;                       // Обнуляем данным о выбранном сервере
                List<MyServer> _updateServers = new List<MyServer>();                // Создаем новый лист серверов
                for (int i = 0; i < _servers.Count; i++)                             // Заполняем лист всеми кроме удаленного
                {
                    if (_servers[i] != null)
                    {
                        _updateServers.Add(_servers[i]);
                    }
                }
                _servers = _updateServers;                                           // Присваиваем основному листу вспомогательный 
                UpdateServerListBox();                                               // Обновляем ListBox, заново выводя сервера
                MessageBox.Show($"Выбранный сервер был успешно удален!\r\n Name: {serverName}\r\n IP: {serverIP}");
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

        // Метод очистки окна вывода сообщений
        private void clearMessageTextBoxButton_Click(object sender, EventArgs e)
        {
            messageTextBox.Text = "";
        }
        
        // Вспомогательные методы
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
