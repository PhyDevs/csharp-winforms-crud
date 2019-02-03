using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        private int index = 0; 
        private ADO db = new ADO();
        private DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();

            // Connect to the Databse
            this.db.Connect();
        }

        // Form Loading Handler
        private void Form1_Load(object sender, EventArgs e)
        {
            db.Cmd.CommandType = CommandType.Text;
            db.Cmd.CommandText = "Select * from Student";
            db.Dr = db.Cmd.ExecuteReader();
            dt.Load(db.Dr);

            fill_TexBoxes(0);
        }

        // Filling Text Boxes With Students Info
        private void fill_TexBoxes(int i)
        {
            IDtxt.Text = dt.Rows[i][0].ToString();
            firstNameTxt.Text = dt.Rows[i][1].ToString();
            lastNameTxt.Text = dt.Rows[i][2].ToString();
            cityTxt.Text = dt.Rows[i][3].ToString();
            departementTxt.Text = dt.Rows[i][4].ToString();

        }

        // Adding a Student
        private void add_Handler(object sender, EventArgs e)
        {
            int id = -1;
            string f_name = firstNameTxt.Text.Trim(),
                l_name = lastNameTxt.Text.Trim(),
                city = cityTxt.Text.Trim(),
                departement = departementTxt.Text.Trim();

            try
            {
                id = int.Parse(IDtxt.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Enter a Valid ID");
                return;
            }

            Student std = new Student(id, f_name, l_name, city, departement);
            bool added = std.AddStudent(db);

            if (added) {
                MessageBox.Show("The Student Was Added Successfuly");
            }
            else
            {
                MessageBox.Show("Please Enter A valid Informations");
            }
        }

        // Deleting a Student
        private void delete_handler(object sender, EventArgs e)
        {
            int id = -1;
            try
            {
                id = int.Parse(IDtxt.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Enter a Valid ID");
                return;
            }
            Student std = new Student();
            std.Id = id;

            if (MessageBox.Show("Are you sure", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                bool deleted = std.DeleteStudent(db);
                if (deleted)
                {
                    MessageBox.Show("The Student Was Deleted Successfuly");
                }
                else
                {
                    MessageBox.Show("The Student Does not exist");
                }
            }
        }

        // Exiting The Application Event
        private void exit_Handler(object sender, EventArgs e)
        {
            this.db.Disconnect();
            Application.Exit();
        }

        #region Navgation Buttons
        // Navigate to Next Student
        private void next_student(object sender, EventArgs e)
        {
            index = index + 1 > dt.Rows.Count - 1 ? 0 : index + 1;
            fill_TexBoxes(index);
        }
        // Navigate to Previeus Student
        private void prev_student(object sender, EventArgs e)
        {
            index = index - 1 < 0 ? dt.Rows.Count - 1 : index - 1;
            fill_TexBoxes(index);
        }
        // Navigate to the first Student
        private void navigateToFirst(object sender, EventArgs e)
        {
            index = 0;
            fill_TexBoxes(index);
        }
        // Navigate to the Last Student
        private void navigateToLast(object sender, EventArgs e)
        {
            index = dt.Rows.Count - 1;
            fill_TexBoxes(index);
        }
        #endregion Navigation Buttons
    }
}
