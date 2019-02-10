using System;
using System.Data;
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
            if( !this.db.Connect() )
                MessageBox.Show("Please Configure Your Connection");
        }

        // Form Loading Handler
        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.db.Con.State == ConnectionState.Open)
            {
                this.load_students();
                this.fill_TexBoxes(index);
            }
            else
                Application.Exit();
        }

        // Loading Students From the Database
        private void load_students()
        {
            db.Cmd.CommandType = CommandType.Text;
            db.Cmd.CommandText = "Select * from Student";
            db.Dr = db.Cmd.ExecuteReader();
            dt.Clear();
            dt.Load(db.Dr);
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

        // Get Student From TextBoxes
        private Student get_student()
        {
            int id = -1;
            string f_name = firstNameTxt.Text.Trim(),
                l_name = lastNameTxt.Text.Trim(),
                city = cityTxt.Text.Trim(),
                departement = departementTxt.Text.Trim();

            try { id = int.Parse(IDtxt.Text.Trim()); }
            catch { MessageBox.Show("Enter a Valid ID"); }

            return new Student(id, f_name, l_name, city, departement);
        }

        // Adding a Student
        private void add_Handler(object sender, EventArgs e)
        {
            Student std = this.get_student();
            if (std.Id < 0)
                return;

            int added = std.AddStudent(db);
            if (added >= 0)
            {
                if (added == 1)
                {
                    this.load_students();
                    this.index = dt.Rows.Count - 1;
                    this.fill_TexBoxes(index);
                    MessageBox.Show("The Student Was Added Successfuly");
                }
                else
                    MessageBox.Show("This Student already exist");
            }
            else
                MessageBox.Show("Connection Error");
        }

        // Updating a Student
        private void update_student(object sender, EventArgs e)
        {
            Student std = this.get_student();
            if (std.Id < 0)
                return;

            int updated = std.UpdateStudent(db);
            if (updated >= 0)
            {
                if (updated == 1)
                {
                    this.load_students();
                    this.fill_TexBoxes(index);
                    MessageBox.Show("The Student Was Updated Successfuly");
                }
                else
                    MessageBox.Show("This Student Don't Exist");
            }
            else
                MessageBox.Show("Connection Error");
        }

        // Deleting a Student
        private void delete_handler(object sender, EventArgs e)
        {
            Student std = this.get_student();
            if (std.Id < 0)
                return;

            if ( MessageBox.Show("Are you sure", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                int deleted = std.DeleteStudent(db);
                if (deleted >= 0)
                {
                    if (deleted == 1)
                    {
                        this.load_students();
                        this.index--;
                        this.fill_TexBoxes(index);
                        MessageBox.Show("The Student Was Deleted Successfuly");
                    }
                    else
                        MessageBox.Show("This Student Don't Exist");
                }
                else
                    MessageBox.Show("Connection Error");
            }
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

        // Exiting The Application Event
        private void exit_Handler(object sender, EventArgs e)
        {
            this.db.Disconnect();
            Application.Exit();
        }

    }
}
