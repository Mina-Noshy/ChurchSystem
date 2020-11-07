using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class SqlForm : Form
    {
        public SqlForm()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();
        DataTable tbl = new DataTable();
        SqlCommand cmd;
        SqlDataAdapter dt;

        private void SqlForm_Load(object sender, EventArgs e)
        {
            MainForm frm = new MainForm();
            this.Icon = frm.Icon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSelect.Text.Length >= 10)
                {
                    using (AppDbContext context = new AppDbContext())
                    {
                        string databaseName = context.Database.Connection.Database;

                        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        conn.ConnectionString = connectionString;
                        cmd = new SqlCommand(txtSelect.Text, conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        dt = new SqlDataAdapter(cmd);
                        tbl.Clear();
                        dt.Fill(tbl);
                        dataGridView1.DataSource = tbl;
                        conn.Close();

                       // MessageBox.Show("Doooooooone");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExcute.Text.Length >= 10)
                {
                    using (AppDbContext context = new AppDbContext())
                    {
                        string databaseName = context.Database.Connection.Database;


                        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        conn.ConnectionString = connectionString;
                        cmd = new SqlCommand(txtExcute.Text, conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Doooooooone");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
