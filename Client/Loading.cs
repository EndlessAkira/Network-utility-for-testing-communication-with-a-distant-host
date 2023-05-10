using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            label1.Update();
            pictureBox1.Update();
            Thread.Sleep(1000);
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            
        }
    }
}
