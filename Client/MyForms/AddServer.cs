using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AddServer : Form
    {
        public AddServer(Client cl)
        {
            InitializeComponent();
            client = cl;
        }
        Client client;
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string data = textBox.Text;
                if (data == "")
                {
                    MessageBox.Show("Поле ввода осталось незаполненным!");
                }
                else
                {
                    IPAddress iP;
                    try
                    {
                        iP = IPAddress.Parse(data);
                        IPHostEntry host = Dns.GetHostEntry(iP);
                        if (host != null)
                        {
                            client.AddServer(iP);
                            client.UpdateServerListBox();
                            textBox.Text = "";
                            MessageBox.Show("Сервер успешно добавлен!");
                        }
                        else
                            MessageBox.Show("Не удалось найти сервер по заданному IP!");
                    }
                    catch
                    {
                        IPHostEntry host = Dns.GetHostEntry(data);
                        if (host != null)
                        {
                            client.AddServer(data);
                            client.UpdateServerListBox();
                            textBox.Text = "";
                            MessageBox.Show("Сервер успешно добавлен!");
                        }
                        else
                            MessageBox.Show("Не удалось найти сервер по заданному имени хоста!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex}");
                textBox.Text = "Добавить сервер не удалось..";
            }
        }
    }
}
