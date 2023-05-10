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
        public AddServer()
        {
            InitializeComponent();
        }
        public AddServer(Client client)
        {
            InitializeComponent();
        }
        Client client;

        private void AddServer_Load(object sender, EventArgs e)
        {

        }

        private void AddServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server server = null;
            client.Show();
            client.AddServer(server);
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress iP = new IPAddress(Convert.ToInt64(otherServerDataTextBox.Text));
            }
            catch
            {

            }
        }
    }
}
