using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        private ADO db = new ADO();
        public Form1()
        {
            InitializeComponent();

            // Connect to the Databse
            this.db.Connect();
            MessageBox.Show(this.db.Con.State.ToString());
        }

        // Exiting The Application Event
        private void exit_Handler(object sender, EventArgs e)
        {
            this.db.Disconnect();
            Application.Exit();
        }
    }
}
