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
            } catch
            {
                MessageBox.Show("Enter a Valid ID");
                return;
            }

            try
            {
                Student std = new Student(id, f_name, l_name, city, departement);

                db.Cmd.CommandType = CommandType.StoredProcedure;
                db.Cmd.CommandText = "ADD_P";

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@id", std.Id);
                parameters[1] = new SqlParameter("@f_name", std.First_name);
                parameters[2] = new SqlParameter("@l_name", std.Last_name);
                parameters[3] = new SqlParameter("@city", std.City);
                parameters[4] = new SqlParameter("@dep", std.Department);

                db.Cmd.Parameters.Clear();
                foreach(SqlParameter p in parameters)
                {
                    p.Direction = ParameterDirection.Input;
                    db.Cmd.Parameters.Add(p);
                }

                db.Cmd.ExecuteNonQuery();
                MessageBox.Show("The Student Was Added Successfuly");

            } catch
            {
                MessageBox.Show("Please Enter A valid Informations");
                return;
            }
        }

        // Exiting The Application Event
        private void exit_Handler(object sender, EventArgs e)
        {
            this.db.Disconnect();
            Application.Exit();
        }

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
    }
}
