using System;
using System.Data.SqlClient;
using System.Data;

namespace CRUD
{
    class ADO
    {
        private const string serverName = "PRO-PC\\SQLSERVER";
        private const string dataBaseName = "school";
        private const bool integratedSecurity = true;

        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader dr;

        #region Encapsulation
        public SqlConnection Con { get => con; set => con = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }
        public SqlDataReader Dr { get => dr; set => dr = value; }
        #endregion Encapsulation

        // Constructor
        public ADO()
        {
            this.con = new SqlConnection("Data Source = " + serverName + ";"
                            + "Integrated Security = " + integratedSecurity + ";" +
                            "Initial Catalog = "+ dataBaseName +""
                        );
            this.cmd = new SqlCommand();
            this.cmd.Connection = this.con;
        }


        // Method to Connect to the database
        public bool Connect()
        {
            if(this.con.State == ConnectionState.Closed || this.con.State == ConnectionState.Broken )
            {
                try { this.con.Open(); }
                catch { return false; }
            }
            return true;
        }

        // Method to close the connection to the database
        public void Disconnect()
        {
            if(this.con.State == ConnectionState.Open)
            {
                this.con.Close();
            }
        }
    }
}
